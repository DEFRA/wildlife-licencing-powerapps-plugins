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
    public class ApplicationDeletionJob1 : CodeActivity
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

                DisassociateNtoNRelationships(service, application, "sdds_application_Contact_Authorisedpersons", "sdds_application_authorisedpersons", "contact");
                DisassociateNtoNRelationships(service, application, "sdds_membership_sdds_application_sdds_app", "sdds_membership_sdds_application", "sdds_membership");
                DisassociateNtoNRelationships(service, application, "sdds_qualification_sdds_application_sdds_", "sdds_qualification_sdds_application", "sdds_qualification");

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

                var applicationDB = service.Retrieve(application.LogicalName, application.Id, new ColumnSet("statuscode"));
                if (applicationDB == null) tracingService.Trace("Application is deleted.");
                if (applicationDB?.GetAttributeValue<OptionSetValue>("statuscode")?.Value == 100000005 ||
                    applicationDB?.GetAttributeValue<OptionSetValue>("statuscode")?.Value == 100000006)
                {
                    tracingService.Trace("Withdrawn or Paused...");
                    service.Delete(application.LogicalName, application.Id);
                    tracingService.Trace("Application deleted!!!");
                }
                else
                {
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
                        ["sdds_referenceorpurchaseordernumber"] = null,
                        ["sdds_nopermissionrequiredother"] = null,
                        ["sdds_whynopermissionrequired"] = null,
                        ["sdds_wildliferelatedconviction"] = null,
                        ["sdds_detailsofconvictions"] = null,
                        ["sdds_sitesubjecttoanycommitment"] = null,
                        ["sdds_otherprotectedspeciecommitment"] = null,
                        ["sdds_conflictsbtwappotherlegalcommitment"] = null,
                        ["sdds_badgersitecommitmenthavebeenmet"] = null,
                        ["sdds_protectedspciecommitmentmet"] = null,
                        ["sdds_yesbadgersitecommitment"] = null,
                        ["sdds_describethepotentialconflicts"] = null,
                        ["sdds_permissionsobtainednotenough"] = null,
                        ["sdds_yesotherprotectedspeciecommitment"] = null
                    });
                    tracingService.Trace("Updated Application!!!");
                }
                Result.Set(context, true);
            }
            catch (Exception ex)
            {
                tracingService.Trace($"{ex.Message}: {ex.StackTrace}");
                ExceptionHandler.SaveToTable(service, ex, wfContext.MessageName, this.GetType().Name, (int)ErrorPriority.High);
                throw ex;
            }
        }

        private void DisassociateNtoNRelationships(IOrganizationService service, EntityReference application, string relationshipName, string relationshipTable, string relatedEntity)
        {
            /*var query = new QueryExpression(relatedEntity) { ColumnSet = new ColumnSet($"{relatedEntity}id") };
            var query_link = query.AddLink(relationshipTable, $"{relatedEntity}id", $"{relatedEntity}id");
            query_link.LinkCriteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, application.Id);*/

            var relatedRecords = service.RetrieveMultiple(new QueryExpression(relatedEntity)
            {
                ColumnSet = new ColumnSet($"{relatedEntity}id"),
                LinkEntities =
                {
                    new LinkEntity(relatedEntity, relationshipTable, $"{relatedEntity}id", $"{relatedEntity}id", JoinOperator.Inner)
                    {
                        LinkCriteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("sdds_applicationid", ConditionOperator.Equal, application.Id)
                            }
                        }
                    }
                }
            });
            if (relatedRecords.Entities.Any())
            {
                service.Disassociate(application.LogicalName,
                    application.Id,
                    new Relationship(relationshipName),
                    new EntityReferenceCollection(
                        relatedRecords.Entities.Select(x => x.ToEntityReference()).ToList()));
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
