using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;
using System.Threading;

namespace SDDS.Workflow.Application
{
    public class ApplicationDeletionJob : CodeActivity
    {
        [RequiredArgument]
        [Input("ApplicationRef")]
        [ReferenceTarget("sdds_application")]
        public InArgument<EntityReference> ApplicationRef { get; set; }

        [Output("Result")]
        [Default("False")]
        public OutArgument<bool> Result { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            IWorkflowContext wfContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(wfContext.UserId);
            ITracingService tracingService = (ITracingService)context.GetExtension<ITracingService>();

            tracingService.Trace("Entering Checking task workflow");
            try
            {
                EntityReference application = ApplicationRef.Get<EntityReference>(context);
                if (application == null || application.LogicalName != "sdds_application") return;

                DeleteRelatedRecords(service, application.Id, "sdds_license", "sdds_applicationreport", "sdds_paymentrequest",
                    "sdds_sitevisit", "sdds_planningconsent", "sdds_designatedsites", "sdds_compliancecheck", "sdds_emaillicence", "sdds_assessmentinterview");

                DeleteRelatedRecords(service, "sdds_consultation", "sdds_consultationid", "sdds_application", application.Id);

                DeleteRelatedObjects(service, "annotation", "annotationid", application.Id);
                DeleteRelatedObjects(service, "queueitem", "queueitemid", application.Id);

                DeleteActivities(service, application.Id, "email", "sdds_stageduration", "task", "phonecall", "letter", "fax", "appointment", "chat", "recurringappointmentmaster");

                DeleteRelatedSharePointDocs(service, "sharepointdocument", "sharepointdocumentid", application.Id);
                DeleteRelatedSharePointDocs(service, "sharepointdocumentlocation", "sharepointdocumentlocationid", application.Id);

                service.Update(new Entity(application.LogicalName, application.Id)
                {
                    ["sdds_fiilesdeleted"] = true
                });
            }
            catch (Exception ex)
            {

            }
        }
        private void DeleteRelatedRecords(IOrganizationService service, Guid ApplicationId, params string[] tableNames)
        {
            tableNames.ToList().ForEach(x => DeleteRelatedRecords(service, x, x + "id", "sdds_applicationid", ApplicationId));
        }

        private void DeleteRelatedObjects(IOrganizationService service, string tableName, string tableId, Guid ApplicationId)
        {
            DeleteRelatedRecords(service, tableName, tableId, "objectid", ApplicationId);
        }

        private void DeleteActivities(IOrganizationService service, Guid applicationId, params string[] tables)
        {
            tables.ToList().ForEach(x => DeleteRelatedActivities(service, x, applicationId));
        }

        private void DeleteRelatedSharePointDocs(IOrganizationService service, string tableName, string tableId, Guid ApplicationId)
        {
            DeleteRelatedRecords(service, tableName, tableId, "regardingobjectid", ApplicationId);
        }

        private void DeleteRelatedActivities(IOrganizationService service, string tableName, Guid ApplicationId)
        {
            DeleteRelatedRecords(service, tableName, "activityid", "regardingobjectid", ApplicationId);
        }

        private void DeleteRelatedRecords(IOrganizationService service, string tableName, string tableId, string applicationField, Guid ApplicationId)
        {
            var relatedRecords = service.RetrieveMultiple(new QueryExpression(tableName)
            {
                ColumnSet = new ColumnSet(tableId),
                Criteria = new FilterExpression
                {
                    Conditions =
                        {
                            new ConditionExpression(applicationField, ConditionOperator.Equal, ApplicationId)
                        }
                }
            });
            relatedRecords.Entities.ToList().ForEach(x => service.Delete(x.LogicalName, x.Id));
        }
    }
}
