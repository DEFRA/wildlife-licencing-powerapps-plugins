using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SDDS.Plugin.Assessment_Logic
{
    public class AssociateAssessmentLogicRecord : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {

                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity entity
                 && context.MessageName.ToLower() == "create")
                {
                    if(entity.LogicalName == "sdds_siteregistrationsepcie" ||  entity.LogicalName == "sdds_planningconsent" || 
                        entity.LogicalName == "sdds_survey" || entity.LogicalName == "sdds_application")
                    {
                        EntityReference assessLogic = GetAssessmentCategoryLogic(service, entity.LogicalName);

                        if(assessLogic != null)
                        {
                            entity.Attributes["sdds_assessmentcategorylogicid"] = assessLogic;
                            //service.Update(new Entity(entity.LogicalName, entity.Id)
                            //{
                            //    ["sdds_assessmentcategorylogicid"] = assessLogic
                            //});
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tracing.Trace("AssociateAssessmentLogicRecord Error: " + ex.Message);
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }

        private static EntityReference GetAssessmentCategoryLogic(IOrganizationService service, string entityLogicalName)
        {
            EntityReference assessmentCat = null;
            int value = GetOptionSetValue(entityLogicalName);

            var query = new QueryExpression("sdds_assessmentcategorylogic");
            query.ColumnSet.AddColumns("sdds_assessmentcategorylogicid", "sdds_name");
            query.Criteria.AddCondition("sdds_regardingentity", ConditionOperator.Equal, value);

            EntityCollection assessmentCatColl = service.RetrieveMultiple(query);
            var assesmentCatLogic = assessmentCatColl.Entities[0];
            if(assessmentCatColl.Entities.Count > 0)
            {
                assessmentCat = new EntityReference(assesmentCatLogic.LogicalName, assesmentCatLogic.Id);
            }

            return assessmentCat;
        }

        private static int GetOptionSetValue(string entityNme)
        {
            //Application - 452120000
            //Accreditation Level, Specie & Roost - 452120001
            //Survey - 452120002
            //Planning Consent - 452120003

             int value;
            switch (entityNme)
            {
                case "sdds_siteregistrationsepcie":
                    value = 452120001;
                    break;
                case "sdds_planningconsent":
                    value = 452120003;
                    break;
                case "sdds_survey":
                    value = 452120002;
                    break;
                default:
                    value = 452120000;
                    break;
            }

            return value;
        }
    }
}
