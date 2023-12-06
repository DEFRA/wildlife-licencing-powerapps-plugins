using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.Assessment_Logic
{
    public class GeneralAssessmentCategoryLogic : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                tracing.Trace("Entered Plugin...");
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity regardingRecord
                 && regardingRecord.Attributes.ContainsKey("sdds_assessmentcategorylogicid") && regardingRecord["sdds_assessmentcategorylogicid"] != null)
                {
                    tracing.Trace("Entered plugin... Actions...");
                    var query = new QueryExpression("sdds_asessmentitem")
                    {
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("sdds_assessmentcategorylogicid", ConditionOperator.Equal, (regardingRecord["sdds_assessmentcategorylogicid"] as EntityReference).Id)
                            }
                        }
                    };
                    var result = service.RetrieveMultiple(query);
                    if (result?.Entities != null)
                    {
                        tracing.Trace("retrieved logic, entitycount:" + result.Entities.Count);
                        var logicItems = result.Entities.Select(x => ConvertEntityToLogicItem(x)).ToList();
                        tracing.Trace("converted logic items");

                        var dbRegardingRecord = service.Retrieve(regardingRecord.LogicalName,
                            regardingRecord.Id,
                            new ColumnSet(true));

                        //tracing.Trace("retrieved regarding record: " + dbRegardingRecord[logicItems.First().fieldname]);
                        //tracing.Trace("value: " + logicItems.First().fieldvalue);
                        var sortedLogicItems = logicItems.OrderByDescending(x => x.assessmentCategoryType);

                        var assessmentCategory = sortedLogicItems.FirstOrDefault(x => (dbRegardingRecord.Attributes.Contains(x.fieldname) && dbRegardingRecord[x.fieldname].ToString().ToLower().Equals(x.fieldvalue)) ||
                            (dbRegardingRecord.FormattedValues.ContainsKey(x.fieldname) && dbRegardingRecord.FormattedValues[x.fieldname].ToLower() == x.fieldvalue))?.assessmentCategoryType;
                        tracing.Trace("implemented logic: " + assessmentCategory);
                        service.Update(new Entity(regardingRecord.LogicalName, regardingRecord.Id)
                        {
                            ["sdds_assessmentcategory"] = new OptionSetValue(assessmentCategory ?? 452120000)
                        });
                        tracing.Trace("end of plugin");
                    }
                }
                tracing.Trace("End of plugin no action");
            }
            catch (Exception ex)
            {
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, this.GetType().Name);
                throw ex;
            }
        }

        private AssessmentLogicItem ConvertEntityToLogicItem(Entity entity)
        {
            return new AssessmentLogicItem
            {
                id = entity.Id,
                assessmentCategoryType = entity.GetAttributeValue<OptionSetValue>("sdds_assessmenttype").Value,
                fieldname = entity.GetAttributeValue<string>("sdds_name"),
                fieldtype = entity.FormattedValues["sdds_datatype"],
                fieldvalue = entity.GetAttributeValue<string>("sdds_value").ToLower()
            };
        }
    }

    public class AssessmentLogicItem
    {
        public Guid id { get; set; }
        public string fieldname { get; set; }
        public string fieldtype { get; set; }
        public string fieldvalue { get; set; }
        public int assessmentCategoryType { get; set; }
    }
}
