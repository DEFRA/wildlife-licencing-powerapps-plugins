using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Workflow.Application
{
    public class ApplicationDeletionJob1: CodeActivity
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

                tracingService.Trace($"Application id:{application.Id}");

                DeleteRelatedRecords(service, application.Id, "sdds_license", "sdds_paymentrequest", "sdds_ecologistexperience",
                    "sdds_sitevisit", "sdds_planningconsent", "sdds_designatedsites", "sdds_compliancecheck", "sdds_emaillicence", "sdds_assessmentinterview");
                tracingService.Trace("Completed 1");

                DeleteRelatedRecords(service, "sdds_consultation", "sdds_consultationid", "sdds_application", application.Id);
                DeleteRelatedRecords(service, "sdds_applicationreport", "sdds_applicationreportid", "sdds_application", application.Id);
                tracingService.Trace("Completed 2");

                DeleteRelatedObjects(service, "annotation", "annotationid", application.Id);
                DeleteRelatedObjects(service, "queueitem", "queueitemid", application.Id);
                tracingService.Trace("Completed 3");

                DeleteActivities(service, application.Id, "email", "sdds_stageduration", "phonecall", "letter", "fax", "appointment", "chat", "recurringappointmentmaster");
                tracingService.Trace("Completed 4");

                DeleteRelatedSites(service, application.Id);
                tracingService.Trace("Completed 4.1");
                DeleteRelatedSharePointDocs(service, "sharepointdocumentlocation", "sharepointdocumentlocationid", application.Id);
                tracingService.Trace("Completed end");


                service.Update(new Entity(application.LogicalName, application.Id)
                {
                    ["sdds_ecologistid"] = null,
                    ["sdds_ecologistorganisationid"] = null,
                    ["sdds_ecologistcontactno"] = null,
                    ["sdds_applicantid"] = null,
                    ["sdds_applicantcontactno"] = null,
                    ["sdds_organisationid"] = null,
                    ["sdds_alternativeapplicantcontactid"] = null,
                    ["sdds_alternativeecologistcontactid"] = null,
                    ["sdds_billingcustomerid"] = null,
                    ["sdds_billingorganisationid"] = null,
                    ["sdds_heldbadgerlicence"] = null,
                    ["sdds_badgermitigationclasslicence"] = null,
                    ["sdds_ecologistexperienceofbadgerecology"] = null,
                    ["sdds_ecologistexperienceofmethods"] = null,
                    ["sdds_mitigationclassrefno"] = null,
                    ["sdds_referenceorpurchaseordernumber"] = null
                });

                tracingService.Trace("Updated Application!");
            }
            catch (Exception ex)
            {
                tracingService.Trace($"{ex.Message}: {ex.StackTrace}");
            }
        }

        private void DeleteRelatedSites(IOrganizationService service, Guid applicationId)
        {
            var sites = service.RetrieveMultiple(new QueryExpression("sdds_site")
            {
                ColumnSet = new ColumnSet("sdds_siteid"),
                LinkEntities =
                {
                    new LinkEntity ("sdds_site","sdds_application_sdds_site","sdds_siteid","sdds_siteid", JoinOperator.Inner)
                    {
                        EntityAlias="App",
                        LinkCriteria=new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("sdds_applicationid", ConditionOperator.Equal, applicationId)
                            }
                        }
                    }
                }
            });
            service.Execute(new DisassociateRequest
            {
                Target = new EntityReference("sdds_application", applicationId),
                Relationship = new Relationship("sdds_application_sdds_site_sdds_site"),
                RelatedEntities = new EntityReferenceCollection(sites.Entities.Select(x => new EntityReference(x.LogicalName, x.Id)).ToList()),
            });
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
