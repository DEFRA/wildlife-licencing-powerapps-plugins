using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.Application
{
    public class SetApplicationDueDate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                if(context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity application
                 && context.MessageName.ToLower() == "create")
                {
                    DateTime createdOnDate;
                    if (!application.GetAttributeValue<bool>("sdds_sourceremote"))
                        createdOnDate = application.GetAttributeValue<DateTime>("sdds_applicationformreceiveddate");
                    else createdOnDate = DateTime.Now;

                    int businessWorkingdaysToAdd = 30 + GetNumberHolidaydays(service);

                    DateTime applicationDueDate = createdOnDate.AddBusinessDays(businessWorkingdaysToAdd);
                    DateTime centralSLADueTime = createdOnDate.AddBusinessDays(5);

                    service.Update(new Entity(application.LogicalName,application.Id)
                    {
                        ["sdds_licenceapplicationduedate"] = applicationDueDate,
                        ["sdds_centralslatimer"] = centralSLADueTime,
                    });
                }
            }
            catch (Exception ex)
            {

                tracing.Trace(ex.Message);
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }

        private static int GetNumberHolidaydays(IOrganizationService service)
        {
            var query = new QueryExpression("sdds_bankholiday");
            query.ColumnSet.AddColumn("sdds_bankholidaydate");
            query.Criteria.AddCondition("sdds_bankholidaydate", ConditionOperator.OnOrAfter, DateTime.Now);

            EntityCollection holidays = service.RetrieveMultiple(query);

            return holidays.Entities.Count;
        }
    }
}
