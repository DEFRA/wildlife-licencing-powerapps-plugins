using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SDDS.Plugin.Common
{
    public abstract class BasePlugin : IPlugin
    {
        static IReadOnlyDictionary<string, string> EnvVariables;
        static readonly object Lock = new object();
        private string _unsecureConfig;
        private string _secureConfig;

        public BasePlugin()
        {
        }

        public BasePlugin(string UnsecureConfig, string SecureConfig)
        {
            this._unsecureConfig = UnsecureConfig;
            this._secureConfig = SecureConfig;
        }
        protected static IReadOnlyDictionary<string, string> GetEnvironmentVariables(LocalPluginContext ctx, params string[] schemaNames)
        {
            ctx.Trace($"Entered GetEnvironmentVariables");

            // Singleton pattern to load environment variables less
            if (EnvVariables == null)
            {
                lock (Lock)
                {
                    if (EnvVariables == null)
                    {
                        ctx.Trace($"Load environment variables");
                        var envVariables = new Dictionary<string, string>();

                        var query = new QueryExpression("environmentvariabledefinition")
                        {
                            ColumnSet = new ColumnSet("statecode", "defaultvalue", "valueschema",
                              "schemaname", "environmentvariabledefinitionid", "type"),
                            LinkEntities =
                        {
                            new LinkEntity
                            {
                                JoinOperator = JoinOperator.LeftOuter,
                                LinkFromEntityName = "environmentvariabledefinition",
                                LinkFromAttributeName = "environmentvariabledefinitionid",
                                LinkToEntityName = "environmentvariablevalue",
                                LinkToAttributeName = "environmentvariabledefinitionid",
                                Columns = new ColumnSet("statecode", "value", "environmentvariablevalueid"),
                                EntityAlias = "v"
                            }
                        }
                        };

                        if (schemaNames != null && schemaNames.Length > 0)
                        {
                            query.Criteria = new FilterExpression
                            {
                                Conditions =
                                {
                                    new ConditionExpression("schemaname", ConditionOperator.In, schemaNames)
                                }
                            };
                        }
                        var results = ctx.SystemUserService.RetrieveMultiple(query);
                        if (results?.Entities.Count > 0)
                        {
                            foreach (var entity in results.Entities)
                            {
                                var schemaName = entity.GetAttributeValue<string>("schemaname");
                                var value = entity.GetAttributeValue<AliasedValue>("v.value")?.Value?.ToString();
                                var defaultValue = entity.GetAttributeValue<string>("defaultvalue");

                                ctx.Trace($"- schemaName:{schemaName}, value:{value}, defaultValue:{defaultValue}");
                                if (schemaName != null && !envVariables.ContainsKey(schemaName))
                                    envVariables.Add(schemaName, string.IsNullOrEmpty(value) ? defaultValue : value);
                            }
                        }

                        EnvVariables = envVariables;
                    }
                }
            }

            ctx.Trace($"Exiting GetEnvironmentVariables");
            return EnvVariables;
        }

        public object GetEnvironmentVariableSecret(LocalPluginContext ctx, string name)
        {
            ctx.Trace("entered retrieving secret");
            var req = new OrganizationRequest("RetrieveEnvironmentVariableSecretValue");
            req.Parameters["EnvironmentVariableName"] = name;

            ctx.Trace("retrieving secret");
            var result = ctx.Service.Execute(req);

            ctx.Trace($"retrieved secret: {result.Results["EnvironmentVariableSecretValue"]}");
            return result.Results["EnvironmentVariableSecretValue"];
        }

        public void Execute(IServiceProvider serviceProvider)
        {

            var context = new LocalPluginContext(serviceProvider, _unsecureConfig, _secureConfig);
            ExecutePluginLogic(context);
        }

        public virtual void ExecutePluginLogic(LocalPluginContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class LocalPluginContext : ITracingService
    {
        private ITracingService _tracingSvc;
        public IOrganizationService SystemUserService { get; private set; }
        public IOrganizationService Service { get; private set; }
        public IPluginExecutionContext PluginContext { get; private set; }
        public ParameterCollection InputParameters { get; private set; }
        public ParameterCollection OutputParameters { get; private set; }
        public LocalPluginContext(IServiceProvider serviceProvider, string unsecureConfig, string secureConfig)
        {
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            _tracingSvc = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            PluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            SystemUserService = serviceFactory.CreateOrganizationService(null);
            Service = serviceFactory.CreateOrganizationService(PluginContext.UserId);
            InputParameters = PluginContext.InputParameters;
            OutputParameters = PluginContext.OutputParameters;
        }
        public void Trace(string format, params object[] args)
        {
            _tracingSvc.Trace(format, args);
        }
    }
}
