using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.Survey
{
    public class SurveyAssessmentCategoryLogic : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity regardingRecord
                 && regardingRecord.Attributes.ContainsKey("sdds_assessmentcategorylogicid") && regardingRecord["sdds_assessmentcategorylogicid"] != null)
                {
                    var query = new QueryExpression("sdds_asessmentitem")
                    {
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("sdds_assessmentcategorylogicid", ConditionOperator.Equal, (regardingRecord["sdds_assessmentcategorylogicid"] as EntityReference).Id),
                                new ConditionExpression("sdds_assessmenttype", ConditionOperator.NotEqual,1000000)
                            }
                        }
                    };
                    var result = service.RetrieveMultiple(query);
                    if (result?.Entities != null)
                    {
                        var logicItems = result.Entities.Select(x => ConvertEntityToLogicItem(x)); //452120002
                        var dbRegardingRecord = service.Retrieve(regardingRecord.LogicalName,
                            regardingRecord.Id,
                            new ColumnSet(logicItems.Select(x => x.fieldname).Distinct().ToArray()));
                        var sortedLogicItems = logicItems.OrderByDescending(x => x.assessmentCategoryType);

                        var assessmentCategory = sortedLogicItems.FirstOrDefault(x => (dbRegardingRecord.Contains(x.fieldname) && dbRegardingRecord[x.fieldname] == x.fieldvalue) ||
                            (dbRegardingRecord.FormattedValues.ContainsKey(x.fieldname) && dbRegardingRecord.FormattedValues[x.fieldname] == x.fieldvalue))?.assessmentCategoryType;

                        service.Update(new Entity(regardingRecord.LogicalName, regardingRecord.Id)
                        {
                            ["sdds_assessmentcategory"] = new OptionSetValue(assessmentCategory ?? 452120000)
                        });
                    }
                }
            }
            catch (Exception ex)
            {

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
                fieldvalue = entity.GetAttributeValue<string>("sdds_value")
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
