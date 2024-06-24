using Microsoft.Xrm.Sdk;
using SDDS.Plugin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.Application
{
    public class ClearRetentionDate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            tracing.Trace("Entering plugin");
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity entity
                && context.MessageName.ToLower() == "update")
            {
                tracing.Trace("Entering plugin1");
                Entity application = entity;
                if (context.PreEntityImages.Contains("preImage") && context.PreEntityImages["preImage"] is Entity prevEntImage)
                {
                    tracing.Trace("Entering plugin2");
                    var prevState = prevEntImage;
                    var oldstatus = prevState.GetAttributeValue<OptionSetValue>("statuscode");

                    if (oldstatus != null && (oldstatus.Value == 100000006 || oldstatus.Value == 452120001))
                    {
                        
                        service.Update(new Entity(application.LogicalName, application.Id)
                        {
                            ["sdds_retaindatauntil"] = null
                        });

                    }
                }
            }

            try
            {

            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, "ClearRetentionDate", (int)ErrorPriority.High);
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
