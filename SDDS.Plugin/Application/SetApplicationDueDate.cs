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
                    DateTime applicationDueDate;
                    DateTime centralSLADueTime;

                    if (!application.GetAttributeValue<bool>("sdds_sourceremote"))
                        createdOnDate = application.GetAttributeValue<DateTime>("sdds_applicationformreceiveddate");
                    else createdOnDate = DateTime.Now;

                    EntityReference appTypeRef = application.GetAttributeValue<EntityReference>("sdds_applicationtypesid");
                    Entity applicationType = service.Retrieve(appTypeRef.LogicalName, appTypeRef.Id, new ColumnSet(new string[] { "sdds_duedays", "sdds_defaultdatewhenoutsideseasonalwindow" }));

                    if (ApplicationOutSideWindowSeason(appTypeRef.Id, service, createdOnDate))
                    {
                        DateTime today = DateTime.Now;
                        DateTime defaultDueDate = applicationType.GetAttributeValue<DateTime>("sdds_defaultdatewhenoutsideseasonalwindow");

                        int yearToUse = defaultDueDate.Month < today.Month ? (today.Year + 1) : today.Year;
                        applicationDueDate = new DateTime(yearToUse, defaultDueDate.Month, defaultDueDate.Day);
                        centralSLADueTime = createdOnDate.AddBusinessDays(5);

                        service.Update(new Entity(application.LogicalName, application.Id)
                        {
                            ["sdds_licenceapplicationduedate"] = applicationDueDate,
                            ["sdds_centralslatimer"] = centralSLADueTime,
                        });

                    }
                    else
                    {
                        
                        int appDueDays = applicationType.GetAttributeValue<int?>("sdds_duedays") ?? 30;

                        tracing.Trace("Due days:" + appDueDays.ToString());

                        DateTime thirtyBusinessDays = createdOnDate.AddBusinessDays(appDueDays);

                        int businessWorkingdaysToAdd = appDueDays + GetNumberHolidaydays(service, createdOnDate, thirtyBusinessDays);

                        applicationDueDate = createdOnDate.AddBusinessDays(businessWorkingdaysToAdd);
                        centralSLADueTime = createdOnDate.AddBusinessDays(5);

                        service.Update(new Entity(application.LogicalName, application.Id)
                        {
                            ["sdds_licenceapplicationduedate"] = applicationDueDate,
                            ["sdds_centralslatimer"] = centralSLADueTime,
                        });
                    }                   
                }
            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, "SetApplicationDueDate");
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

        private static bool ApplicationOutSideWindowSeason(Guid applicationTypeId, IOrganizationService _service, DateTime createdOn)
        {
            bool outSeason = false;
            
            var applicationType = _service.Retrieve("sdds_applicationtypes", applicationTypeId, new ColumnSet(new string[] { "sdds_applicationtypesid", "sdds_fromday", "sdds_today", "sdds_seasonfrom", "sdds_seasonto", "sdds_defaultdatewhenoutsideseasonalwindow" }));
            if (applicationType != null)
            {
                if ((applicationType.Attributes.Contains("sdds_seasonfrom") || applicationType.Attributes["sdds_seasonfrom"] != null)
                    && (applicationType.Attributes.Contains("sdds_seasonto") || applicationType.Attributes["sdds_seasonto"] != null))
                {
                    int seasonFrom = applicationType.GetAttributeValue<OptionSetValue>("sdds_seasonfrom").Value;
                    int seasonTo = applicationType.GetAttributeValue<OptionSetValue>("sdds_seasonto").Value;
                    int toDay = applicationType.GetAttributeValue<OptionSetValue>("sdds_today").Value;
                    int fromDay = applicationType.GetAttributeValue<OptionSetValue>("sdds_fromday").Value;

                    DateTime seasonFromDate = new DateTime(DateTime.UtcNow.Year, seasonFrom, fromDay);
                    DateTime seasonToDate = new DateTime(DateTime.UtcNow.Year, seasonTo, toDay);

                    outSeason = createdOn < seasonFromDate || createdOn > seasonToDate;

                }

            }

            return outSeason;

        }
    }
}
