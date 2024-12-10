using Microsoft.Xrm.Sdk;
using SDDS.Plugin.Common;
using SDDS.Plugin.EBG;
using SDDS.Plugin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SDDS.Plugin.Application
{
    public class ApplicationModification : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                tracing.Trace("Entering modification");
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference application)
                {
                    GetAllAboutApplication modMethods = new GetAllAboutApplication();

                    sdds_modificationrequest modification = modMethods.GetModificationRequest(service, application.Id);

                    sdds_application newApp = modMethods.GetAllApplication(application, service, modification);

                    tracing.Trace("Creating modification Record");
                    Guid NewAppId = service.Create(newApp);

                    tracing.Trace("Created modification Record: " + NewAppId.ToString());
                    modMethods.GetAllRelatedSites(NewAppId, service, tracing, application.Id);
                    modMethods.GetAuthorisedPersons(NewAppId, service, tracing, application.Id);

                    tracing.Trace("Creating modification Record licensable actions: ");
                    modMethods.GetLicensAbleActions(NewAppId, service, tracing, application.Id);
                    modMethods.GetAllPlanningConsent(NewAppId, service, tracing, application.Id);
                    modMethods.GetDesignatedSites(NewAppId, service, tracing, application.Id);
                    modMethods.GetAssessmentRecords(NewAppId, service, tracing, application.Id, modification);

                    sdds_application parentApp = new sdds_application()
                    {
                        Id = application.Id,
                        LogicalName = application.LogicalName,
                        StatusCode = sdds_application_StatusCode.ModificationinProgress
                    };
                    service.Update(parentApp);

                    context.OutputParameters["RecordId"] = NewAppId.ToString();
                }
            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, "ApplicationModification", (int)ErrorPriority.High);
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
