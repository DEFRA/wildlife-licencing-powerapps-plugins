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

                    EntityReference appTypeRef = application.GetAttributeValue<EntityReference>("sdds_applicationtypesid");
                    Entity applicationType = service.Retrieve(appTypeRef.LogicalName, appTypeRef.Id, new ColumnSet(new string[] { "sdds_duedays" }));

                    int appDueDays = applicationType.GetAttributeValue<int?>("sdds_duedays") ?? 30;

                    tracing.Trace("Due days:" + appDueDays.ToString());
                     
                    DateTime thirtyBusinessDays = createdOnDate.AddBusinessDays(appDueDays);

                    int businessWorkingdaysToAdd = appDueDays + GetNumberHolidaydays(service, createdOnDate, thirtyBusinessDays);

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
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, this.GetType().Name);
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }

        private static int GetNumberHolidaydays(IOrganizationService service, DateTime startDate, DateTime endDate)
        {
            var query = new QueryExpression("sdds_bankholiday");
            query.ColumnSet.AddColumn("sdds_bankholidaydate");
            query.Criteria.AddCondition("sdds_bankholidaydate", ConditionOperator.Between, startDate, endDate);

            EntityCollection holidays = service.RetrieveMultiple(query);

            return holidays.Entities.Count;
        }
    }
}
