using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using SDDS.Plugin.EBG;
using SDDS.Plugin.Model;
using static System.Net.Mime.MediaTypeNames;

namespace SDDS.Plugin.Common
{
    public class GetAllAboutApplication
    {
        public sdds_application GetAllApplication( EntityReference appllication, IOrganizationService service, sdds_modificationrequest modification)
        {
            sdds_application app = (sdds_application)service.Retrieve(appllication.LogicalName, appllication.Id, new ColumnSet(true));

            QueryExpression parentApp = new QueryExpression(sdds_application.EntityLogicalName);
            parentApp.ColumnSet = new ColumnSet(false);
            parentApp.Criteria.AddCondition("sdds_parentapplicationid", ConditionOperator.Equal, appllication.Id);

            EntityCollection paApps = service.RetrieveMultiple(parentApp);
            int appCount = paApps.Entities.Count + 1;
            sdds_application app2 = new sdds_application() { 
                sdds_parentapplicationid = new EntityReference(appllication.LogicalName, appllication.Id),
                sdds_priority = app.sdds_priority,
                sdds_level = app.sdds_level,
                sdds_sourceremote = false,
                sdds_licenceexempted = app.sdds_licenceexempted,
                sdds_applicationpurpose = app.sdds_applicationpurpose,
                sdds_descriptionofproposal = app.sdds_descriptionofproposal,
                sdds_homeimprovement = app.sdds_homeimprovement,
                sdds_workbeprotectedscheduledmonumentworship = app.sdds_workbeprotectedscheduledmonumentworship,
                sdds_impactondesignatedprotectedsite = app.sdds_impactondesignatedprotectedsite,
                sdds_issitesameasapplicants = app.sdds_issitesameasapplicants,
                sdds_isapplicantonwnerofland = app.sdds_isapplicantonwnerofland,               
                sdds_wildliferelatedconviction = app.sdds_wildliferelatedconviction,
                sdds_detailsofconvictions = app.sdds_detailsofconvictions,
                sdds_whynopermissionrequired = app.sdds_whynopermissionrequired,
                sdds_nopermissionrequiredOther = app.sdds_nopermissionrequiredOther,
                sdds_doestheprojectneedanypermissions = app.sdds_doestheprojectneedanypermissions,
                sdds_projectpermissionsgranted = app.sdds_projectpermissionsgranted,
                sdds_ownerpermissionreceived = app.sdds_ownerpermissionreceived,
                sdds_sitesubjecttoanycommitment = app.sdds_sitesubjecttoanycommitment,
                sdds_badgersitecommitmenthavebeenmet = app.sdds_badgersitecommitmenthavebeenmet,
                sdds_yesbadgersitecommitment = app.sdds_yesbadgersitecommitment,
                sdds_otherprotectedspeciecommitment = app.sdds_otherprotectedspeciecommitment,
                sdds_protectedspciecommitmentmet = app.sdds_protectedspciecommitmentmet,
                sdds_yesotherprotectedspeciecommitment = app.sdds_yesotherprotectedspeciecommitment,
                sdds_conflictsbtwappotherlegalcommitment = app.sdds_conflictsbtwappotherlegalcommitment,
                sdds_describethepotentialconflicts = app.sdds_describethepotentialconflicts,
                sdds_permissionsobtainednotenough = app.sdds_permissionsobtainednotenough,               
                sdds_applicationtypesid = app.sdds_applicationtypesid,
                sdds_onnexttodesignatedsite = app.sdds_onnexttodesignatedsite,
                sdds_licencejustification = app.sdds_licencejustification,
                sdds_Inrelationtolicencepurpose = app.sdds_Inrelationtolicencepurpose,
                sdds_takenactiontopreventproblemsoutlinedabove = app.sdds_takenactiontopreventproblemsoutlinedabove,
                sdds_providedetailsoftheactionstaken = app.sdds_providedetailsoftheactionstaken,
                sdds_explainwhynoactionshavebeentaken = app.sdds_explainwhynoactionshavebeentaken,
                sdds_applicationtype = sdds_ApplicationType.Modification,
                sdds_alternativeapplicantcontactid = app.sdds_alternativeapplicantcontactid,
                sdds_alternativeecologistcontactid = app.sdds_alternativeecologistcontactid
            };

            if(app.sdds_parentapplicationid != null)
            {
                
                string last2Char = string.Empty;

                string appNumber = app.sdds_applicationnumber.Trim();
                string last3Char = appNumber.Substring(Math.Max(0, appNumber.Length - 3));

                if (last3Char.StartsWith("-")) { last2Char = last3Char; } else last2Char = "-" + last3Char;



                int number;
                int.TryParse(last2Char.Substring(1), out number);

                int newLast2Char = number + 1 + paApps.Entities.Count;

                if (newLast2Char.ToString().Length == 1)
                {
                    app2.sdds_Name = appNumber.Substring(0, appNumber.IndexOf(last2Char)).Trim() + "-0" + newLast2Char;
                    app2.sdds_applicationnumber = appNumber.Substring(0, appNumber.IndexOf(last2Char)).Trim() + "-0" + newLast2Char;

                }
                else
                {
                    app2.sdds_Name = appNumber.Substring(0, appNumber.IndexOf(last2Char)).Trim() + "-" + newLast2Char;
                    app2.sdds_applicationnumber = appNumber.Substring(0, appNumber.IndexOf(last2Char)).Trim() + "-" + newLast2Char;
                }

                
            }
            else
            {
                app2.sdds_Name = app.sdds_Name.Trim() + "-0" + appCount.ToString().Trim();
                app2.sdds_applicationnumber = app.sdds_Name.Trim() + "-0" + appCount.ToString().Trim();
            }

            if (modification.sdds_cloneapplicant == sdds_yesno.Yes)
            {
                app2.sdds_applicantid = app.sdds_applicantid;
                app2.sdds_organisationid = app.sdds_organisationid;
            }

            if (modification.sdds_cloneecologist == sdds_yesno.Yes)
            {
                app2.sdds_ecologistid = app.sdds_ecologistid;
                app2.sdds_heldbadgerlicence = app.sdds_heldbadgerlicence;
                app2.sdds_ecologistexperienceofbadgerecology = app.sdds_ecologistexperienceofbadgerecology;
                app2.sdds_badgermitigationclasslicence = app.sdds_badgermitigationclasslicence;
                app2.sdds_ecologistexperienceofmethods = app.sdds_ecologistexperienceofmethods;
                app2.sdds_mitigationclassrefno = app.sdds_mitigationclassrefno;
                app2.sdds_evidenceofcompetencyprovided = app.sdds_evidenceofcompetencyprovided;
                app2.sdds_ecologistorganisationid = app.sdds_ecologistorganisationid;
            }

            if(modification.sdds_clonebillingdetails == sdds_yesno.Yes)
            {
                app2.sdds_billingcustomerid = app.sdds_billingcustomerid;
                app2.sdds_billingorganisationid = app.sdds_billingorganisationid;
                app2.sdds_applicantthesameasbillingcustomer = app.sdds_applicantthesameasbillingcustomer;
                app2.sdds_referenceorpurchaseordernumber = app.sdds_referenceorpurchaseordernumber;
            }

            return app2;
        }

        public void GetAllRelatedSites(Guid applicationId, IOrganizationService service, ITracingService tracing, Guid oldAppId)
        {
            EntityReferenceCollection sitesColl = new EntityReferenceCollection();

            QueryExpression query = new QueryExpression(sdds_site.EntityLogicalName);
            query.ColumnSet = new ColumnSet(true);
            query.AddLink("sdds_application_sdds_site", "sdds_siteid", "sdds_siteid").LinkCriteria.AddCondition(Model.Application.PrimaryKey, ConditionOperator.Equal, oldAppId);

            EntityCollection retSites =  service.RetrieveMultiple(query);

            if(retSites.Entities.Count > 0)
            {
                tracing.Trace("Found sites: " +  retSites.Entities.Count);
                foreach (sdds_site item in retSites.Entities)
                {
                    sitesColl.Add(new EntityReference(item.LogicalName, item.Id));
                }

                EntityReference applicationRef = new EntityReference(sdds_application.EntityLogicalName, applicationId);

                AssociateRequest request = new AssociateRequest()
                {
                    Target = applicationRef,
                    Relationship = new Relationship("sdds_application_sdds_site_sdds_site"),
                    RelatedEntities = sitesColl
                };

                service.Execute(request);
            }
                       
        }

        public void GetAuthorisedPersons(Guid applicationId, IOrganizationService service, ITracingService tracing, Guid oldAppId)
        {
            EntityReferenceCollection AuthPersonsColl = new EntityReferenceCollection();

            QueryExpression query = new QueryExpression("contact");
            query.ColumnSet = new ColumnSet(false);
            query.AddLink("sdds_application_authorisedpersons", "contactid", "contactid").LinkCriteria.AddCondition(Model.Application.PrimaryKey, ConditionOperator.Equal, oldAppId);

            EntityCollection authPersons = service.RetrieveMultiple(query);

            if (authPersons.Entities.Count > 0)
            {
                foreach (var item in authPersons.Entities)
                {
                    AuthPersonsColl.Add(item.ToEntityReference());
                }
                EntityReference applicationRef = new EntityReference(sdds_application.EntityLogicalName, applicationId);

                AssociateRequest request = new AssociateRequest()
                {
                    Target = applicationRef,
                    Relationship = new Relationship("sdds_application_Contact_Authorisedpersons"),
                    RelatedEntities = AuthPersonsColl
                };

                service.Execute(request);
            }

        }

        public void GetLicensAbleActions(Guid applicationId, IOrganizationService service, ITracingService tracing, Guid oldAppId)
        {           
            List<LicensableAction> licensableActions = new List<LicensableAction>();

            QueryExpression query = new QueryExpression(sdds_licensableaction.EntityLogicalName);
            query.ColumnSet = new ColumnSet(true);

            FilterExpression filter = new FilterExpression(LogicalOperator.And);

            filter.Conditions.Add(new ConditionExpression(Model.Application.PrimaryKey, ConditionOperator.Equal, oldAppId));
            filter.Conditions.Add(new ConditionExpression("sdds_useinlicence", ConditionOperator.Equal, 452120000));
           
            query.Criteria.AddFilter(filter);

            EntityCollection retActions = service.RetrieveMultiple(query);
            if (retActions.Entities.Count > 0)
            {
                EntityCollection methods = new EntityCollection();

                tracing.Trace("Found actions: " +  retActions.Entities.Count);
                foreach (sdds_licensableaction action in retActions.Entities)
                {
                    methods = GetMethods(action, service);

                    action.sdds_applicationid = new EntityReference(sdds_application.EntityLogicalName, applicationId);
                    action.Id = Guid.NewGuid();
                    licensableActions.Add(new LicensableAction()
                    {
                        Action = action
                    });
                }

                if (licensableActions.Count > 0)
                {
                    foreach (var action in licensableActions)
                    {
                        if (methods.Entities.Count > 0)
                        {
                            foreach (sdds_licensemethod method in methods.Entities)
                            {
                                action.Method.Add(new EntityReference(method.LogicalName, method.Id));
                            }
                        }
                    }
                }



                foreach (var item in licensableActions)
                {
                    Guid newActionId = service.Create(item.Action);
                    AssociateRequest request = new AssociateRequest()
                    {
                        Target = new EntityReference(sdds_licensableaction.EntityLogicalName, newActionId),
                        Relationship = new Relationship("sdds_licensableaction_sdds_licensemethod_"),
                        RelatedEntities = item.Method
                    };

                    service.Execute(request);
                }
            }
        }

        public EntityCollection GetMethods(sdds_licensableaction sdds_Licensableaction, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression(sdds_licensemethod.EntityLogicalName);
            query.ColumnSet = new ColumnSet(true);

            query.AddLink("sdds_licensableaction_sdds_licensemetho", "sdds_licensemethodid", "sdds_licensemethodid").LinkCriteria.AddCondition(sdds_licensableaction.PrimaryIdAttribute, ConditionOperator.Equal, sdds_Licensableaction.Id);

            EntityCollection collection = service.RetrieveMultiple(query);

            return collection;
        }

        public void GetAllPlanningConsent(Guid appId, IOrganizationService service, ITracingService tracing, Guid oldAppId)
        {
            List<sdds_planningconsent> consents = new List<sdds_planningconsent>();

            QueryExpression query = new QueryExpression(sdds_planningconsent.EntityLogicalName);
            query.ColumnSet = new ColumnSet(true);
            query.Criteria.AddCondition(Model.Application.PrimaryKey, ConditionOperator.Equal, oldAppId);

            EntityCollection permissionsColl = service.RetrieveMultiple(query);

            if (permissionsColl.Entities.Count > 0)
            {
                tracing.Trace("Found sites: " + permissionsColl.Entities.Count);

                tracing.Trace("found permission: "+ permissionsColl.Entities.Count.ToString());
                permissionsColl.Entities.ToList().ForEach(d => { consents.Add((sdds_planningconsent)d); });
                //consents.AddRange((IEnumerable<sdds_planningconsent>)permissionsColl.Entities.ToList());
                consents.ForEach(c=> { c.sdds_applicationid = new EntityReference(sdds_application.EntityLogicalName, appId);c.Id = Guid.NewGuid(); });

                consents.ForEach(c=> service.Create(c));
            }

        }

        public void GetDesignatedSites(Guid appId, IOrganizationService service, ITracingService tracing, Guid oldAppId)
        {
            List<sdds_designatedsites> consents = new List<sdds_designatedsites>();

            QueryExpression query = new QueryExpression(sdds_designatedsites.EntityLogicalName);
            query.ColumnSet = new ColumnSet(true);
            query.Criteria.AddCondition(Model.Application.PrimaryKey, ConditionOperator.Equal, oldAppId);

            EntityCollection deSitesColl = service.RetrieveMultiple(query);

            if (deSitesColl.Entities.Count > 0)
            {
                tracing.Trace("Found sites: " + deSitesColl.Entities.Count);

                tracing.Trace("found permission: " + deSitesColl.Entities.Count.ToString());
                deSitesColl.Entities.ToList().ForEach(d => { consents.Add((sdds_designatedsites)d); });
                consents.ForEach(c => { c.sdds_applicationid = new EntityReference(sdds_application.EntityLogicalName, appId); c.Id = Guid.NewGuid(); });

                consents.ForEach(c => service.Create(c));
            }

        }

        public void GetAssessmentRecords(Guid appId, IOrganizationService service, ITracingService tracing, Guid oldAppId, sdds_modificationrequest modification)
        {
            if (modification.sdds_cloneAssessmentrecords == sdds_yesno.Yes)
            {
                List<EBG.Task> tasks = new List<EBG.Task>();

                QueryExpression query = new QueryExpression(EBG.Task.EntityLogicalName);
                query.ColumnSet = new ColumnSet(true);
                query.Criteria.AddCondition("regardingobjectid", ConditionOperator.Equal, oldAppId);

                EntityCollection assessmentRecsColl = service.RetrieveMultiple(query);

                if (assessmentRecsColl.Entities.Count > 0)
                {
                    assessmentRecsColl.Entities.ToList().ForEach(d => { tasks.Add((EBG.Task)d); });
                    tasks.ForEach(c => { c.RegardingObjectId = new EntityReference(sdds_application.EntityLogicalName, appId); c.Id = Guid.NewGuid(); c.StateCode = TaskState.Open; c.StatusCode = Task_StatusCode.NotStarted; });

                    tasks.ForEach(c => service.Create(c));
                }

            }

        }

        public sdds_modificationrequest GetModificationRequest(IOrganizationService service, Guid oldAppId)
        {
            sdds_modificationrequest request = new sdds_modificationrequest();  

            QueryExpression query = new QueryExpression(sdds_modificationrequest.EntityLogicalName);
            query.ColumnSet = new ColumnSet(true);
            query.Criteria.AddCondition(Model.Application.PrimaryKey, ConditionOperator.Equal, oldAppId);
            query.AddOrder("createdon", OrderType.Descending);

            EntityCollection modColl = service.RetrieveMultiple(query);

            if (modColl.Entities.Count > 0)
            {
                request = (sdds_modificationrequest)modColl.Entities[0];
            }

            return request;

        }

    }
}
