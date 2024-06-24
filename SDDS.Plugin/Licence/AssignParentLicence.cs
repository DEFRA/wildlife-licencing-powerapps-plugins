using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SDDS.Plugin.EBG;
using SDDS.Plugin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.Licence
{
    public class AssignParentLicence: IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                if(context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity target){

                    sdds_license licence = target.ToEntity<sdds_license>();
                    if (licence.Contains(Model.Application.PrimaryKey))
                    {
                        sdds_application parentApp = (sdds_application)service.Retrieve(sdds_application.EntityLogicalName, licence.sdds_applicationid.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_parentapplicationid" }));
                        if (parentApp.sdds_parentapplicationid != null)
                        {
                            int activityCount = GetModActivityCount( service, parentApp.sdds_parentapplicationid.Id);
                            sdds_license retLicence = GetParentLicence(service, parentApp.sdds_parentapplicationid.Id);

                            if (retLicence.Id != Guid.Empty)
                            {
                                licence.sdds_activitycountfrommodification = activityCount;
                                licence.sdds_ParentLicence = new EntityReference(retLicence.LogicalName, retLicence.Id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, "AssignParentLicence", (int)ErrorPriority.Medium);
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }

        private static int GetModActivityCount(IOrganizationService service, Guid appId)
        {
            int count = 0;

            var query = new QueryExpression(sdds_modificationrequest.EntityLogicalName);
            query.ColumnSet.AddColumns("sdds_applicationid", "sdds_name", "sdds_activitymodificationcount");
            query.Criteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, appId);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.AddOrder("createdon", OrderType.Ascending);

            EntityCollection modColl = service.RetrieveMultiple(query);

            if(modColl.Entities.Count > 0)
            {
               sdds_modificationrequest modification =  (sdds_modificationrequest)modColl.Entities[0];
                count = (int)modification.sdds_activitymodificationcount;
            }

            return count;
        }

        private static sdds_license GetParentLicence(IOrganizationService service, Guid appId)
        {
            sdds_license licence = new sdds_license();

            var query = new QueryExpression(sdds_license.EntityLogicalName);
            query.ColumnSet.AddColumns("sdds_licenseid");
            query.Criteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, appId);
            query.Criteria.AddCondition("statuscode", ConditionOperator.Equal, 1);


            EntityCollection licenceColl = service.RetrieveMultiple(query);

            if (licenceColl.Entities.Count > 0)
            {
                
                licence.Id = licenceColl.Entities[0].Id;
            }

            return licence;
        }
    }
}
