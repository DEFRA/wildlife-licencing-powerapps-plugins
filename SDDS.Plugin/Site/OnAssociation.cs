using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.Site
{
    public class OnAssociation : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            if (context.MessageName.ToLower() != "associate" && context.MessageName.ToLower() != "disassociate") return;
            tracing.Trace("1.......");
            if (!context.InputParameters.Contains("Target") || !(context.InputParameters["Target"] is EntityReference)) return;
            if (!context.InputParameters.Contains("Relationship") || ((Relationship)context.InputParameters["Relationship"]).SchemaName != "sdds_application_sdds_site_sdds_site") return;

            EntityReference target = (EntityReference)context.InputParameters["Target"];
            tracing.Trace("EntityName: "+ target.LogicalName);
            //Entity application = service.Retrieve(target.LogicalName, target.Id, new ColumnSet("sdds_applicationid"));

            string siteName = GetSiteName(service, target.Id);
            service.Update(new Entity(target.LogicalName, target.Id)
            {
                ["sdds_sites"] = siteName
            });
        }

        private static string GetSiteName(IOrganizationService service, Guid applicationId)
        {
            string siteName = null;
            // Instantiate QueryExpression query
            var query = new QueryExpression("sdds_site");

            // Add columns to query.ColumnSet
            query.ColumnSet.AddColumn("sdds_name");

            // Add link-entity query_sdds_application_sdds_site
            var query_sdds_application_sdds_site = query.AddLink("sdds_application_sdds_site", "sdds_siteid", "sdds_siteid");
            query_sdds_application_sdds_site.LinkCriteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, applicationId);

            var siteColl = service.RetrieveMultiple(query);
            if(siteColl != null && siteColl.Entities.Count > 0)
            {
                for (int i = 0; i < siteColl.Entities.Count; i++)
                {
                    if (i == 0) siteName = siteColl.Entities[i].GetAttributeValue<string>("sdds_name");
                    else siteName += ", " + siteColl.Entities[i].GetAttributeValue<string>("sdds_name");
                }
            }
            return siteName;
        }
    }
}
