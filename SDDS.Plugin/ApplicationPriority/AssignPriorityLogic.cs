using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.ApplicationPriority
{
    public class AssignPriorityLogic
    {
        //fedb14b6-53a8-ec11-9840-0022481aca85 Badgers
        //95583cbd-53a8-ec11-9840-0022481aca85 Beavers
        //d85612c4-53a8-ec11-9840-0022481aca85 Buzzard
        //e22614ca-53a8-ec11-9840-0022481aca85 Raven
        //3b8d5ad6-53a8-ec11-9840-0022481aca85 RedKite

        private IOrganizationService _service;

        public int SeasonFrom { get; set; }
        public int SeasonTo { get; set; }

        public AssignPriorityLogic(IOrganizationService service)
        {
            _service = service;
        }


        //Guid healthAndSafety = new Guid("571706d8-54a8-ec11-9840-0022481aca85"); //Public Health and Safety purpose
        public bool GetPurpose(Entity entity)
        {
            var appPurposeRef = entity.GetAttributeValue<EntityReference>("sdds_applicationpurpose");
            var purpose = _service.Retrieve(appPurposeRef.LogicalName, appPurposeRef.Id, new ColumnSet("sdds_purposetype"));

            return purpose.GetAttributeValue<OptionSetValue>("sdds_purposetype").Value == (int)ApplicationEnum.ApplicationPurpose.Priority1;
            /*var id = entity.GetAttributeValue<EntityReference>("sdds_applicationpurpose").Id;
            if (id == healthAndSafety) return true;
           else  return false;*/
        }

        public EntityCollection GetLicensableAction(Guid applicationId)
        {
            QueryExpression specie = new QueryExpression("sdds_licensableaction");
            specie.ColumnSet.AddColumns("sdds_applicationid", "sdds_licensableactionid", "sdds_setttype", "sdds_specieid", "sdds_method", "sdds_maximumnoofsubject");
            specie.Criteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, applicationId);
            var actionmethod = specie.AddLink("sdds_licensableaction_sdds_licensemetho", "sdds_licensableactionid", "sdds_licensableactionid");

            var method = actionmethod.AddLink("sdds_licensemethod", "sdds_licensemethodid", "sdds_licensemethodid");
            method.EntityAlias = "method";
            method.Columns.AddColumn("sdds_choicevalue");

            var entity = _service.RetrieveMultiple(specie);

            return entity;
        }
        public string CheckBadgerBeaverSpecie(Guid parentGuid)
        {
            var actions = GetLicensableAction(parentGuid);
            if (actions == null) return "nothing";

            var badger = actions.Entities.Where(x => x.GetAttributeValue<EntityReference>("sdds_specieid").Id == new Guid("fedb14b6-53a8-ec11-9840-0022481aca85"));
            var beaver = actions.Entities.Where(x => x.GetAttributeValue<EntityReference>("sdds_specieid").Id == new Guid("95583cbd-53a8-ec11-9840-0022481aca85"));
            if (badger.Count() > 0)
            {
                return "badger";
            }
            else if (beaver.Count() > 0) { return "beaver"; }
            else return "nothing";
        }

        public bool CheckBuzzardRavenRedKiteSpecie(Guid parentGuid)
        {
            var actions = GetLicensableAction(parentGuid);
            if (actions == null) return false;

            var species = actions.Entities.Where(x => (x.GetAttributeValue<EntityReference>("sdds_specieid").Id == new Guid("d85612c4-53a8-ec11-9840-0022481aca85")) ||
                         (x.GetAttributeValue<EntityReference>("sdds_specieid").Id == new Guid("e22614ca-53a8-ec11-9840-0022481aca85")) ||
                         (x.GetAttributeValue<EntityReference>("sdds_specieid").Id == new Guid("3b8d5ad6-53a8-ec11-9840-0022481aca85")));
            if (species.Count() > 0)
            {
                return true;
            }
            else return false;
        }

        public bool CheckSettTypeAndMethod(Guid applicationId, ITracingService tracing)
        {
            tracing.Trace("Entering CheckSettTypeAndMethod");
            var actions = GetLicensableAction(applicationId);
            tracing.Trace("getting licensable actions:" + actions.Entities.Count().ToString());
            if (actions == null) return false;

            //If licensable action is of Set type ==MAIN &&
            //Licensable action/Method= Obstructing sett entrances by means of one-way badger gates 
            return actions.Entities.Any(x => (x.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_alternative_sett_available ||
                         x.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_no_alternative_sett) &&
                         x.GetAttributeValue<OptionSetValue>("method.sdds_choicevalue").Value == (int)ApplicationEnum.License_Methods.Obstructing_Sett_Entrances);
        }

        public bool MultiPlots(Guid parentGuid, ITracingService tracing)
        {
            tracing.Trace("Entering MultiPlots");
            // Instantiate QueryExpression query
            var query = new QueryExpression("sdds_site");

            // Add columns to query.ColumnSet
            query.ColumnSet.AddColumns("sdds_name", "sdds_siteid");

            // Add link-entity query_sdds_application_sdds_site
            var query_sdds_application_sdds_site = query.AddLink("sdds_application_sdds_site", "sdds_siteid", "sdds_siteid");

            // Define filter query_sdds_application_sdds_site.LinkCriteria
            query_sdds_application_sdds_site.LinkCriteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, parentGuid);
            var sites = _service.RetrieveMultiple(query);

            if (sites == null) return false;
            if (sites.Entities.Count() > 1)
            {
                return true;
            }
            else return false;
        }

        public bool DASSPSS(Entity entity)
        {
            bool das = entity.GetAttributeValue<bool>("sdds_dasdiscretionaryadvice");
            bool pss = entity.GetAttributeValue<bool>("sdds_psspresubmissionscreening");

            if (das && pss)
            {
                return true;
            }
            else return false;
        }

        public bool SeasonalCheck(Entity entity, Guid licenseTypeId, ITracingService tracing)
        {
            tracing.Trace("Entering SeasonalCheck");
            var createdOn = entity.GetAttributeValue<DateTime>("createdon");
            GetSeasonalWindowByApplicationType(licenseTypeId);
            //var startDay = DateTime.DaysInMonth(DateTime.Now.Year, 6);
            //var endDay = DateTime.DaysInMonth(DateTime.Now.Year, 10);
            //  return createdOn >= new DateTime(DateTime.Now.Year, 6, startDay) && createdOn <= new DateTime(DateTime.Now.Year, 10, endDay);
            tracing.Trace("SeasonFrom " + SeasonFrom);
            tracing.Trace("SeasonTo " + SeasonTo);
            return createdOn.Month >= SeasonFrom && createdOn.Month <= SeasonTo;
        }

        public bool ExistingSiteCheck(Guid applicationId, ITracingService tracing)
        {
            tracing.Trace("Entering ExistingSiteCheck");
            // Instantiate QueryExpression query
            var siteQuery = new QueryExpression("sdds_site")
            {
                ColumnSet = new ColumnSet("sdds_siteid"),
                LinkEntities =
                {
                    new LinkEntity("sdds_site","sdds_application_sdds_site","sdds_siteid", "sdds_siteid", JoinOperator.Inner)
                    {
                        LinkCriteria=new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("sdds_applicationid",ConditionOperator.Equal,applicationId)
                            }
                        }
                    }
                }
            };
            var sites = _service.RetrieveMultiple(siteQuery);
            if (!sites.Entities.Any()) return false;

            siteQuery = new QueryExpression("sdds_license")
            {
                ColumnSet = new ColumnSet("sdds_licenseid"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("statecode", ConditionOperator.Equal,0)
                    }
                },
                LinkEntities =
                {
                    new LinkEntity("sdds_license","sdds_application","sdds_applicationid", "sdds_applicationid", JoinOperator.Inner)
                    {
                        LinkCriteria=new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("sdds_applicationid", ConditionOperator.NotEqual,applicationId)
                            }
                        },
                        LinkEntities =
                        {
                            new LinkEntity("sdds_application","sdds_application_sdds_site","sdds_applicationid", "sdds_applicationid", JoinOperator.Inner)
                            {
                                LinkCriteria=new FilterExpression
                                {
                                    Conditions =
                                    {
                                        new ConditionExpression("sdds_siteid",ConditionOperator.In,sites.Entities.Select(x=>x.Id).ToList())
                                    }
                                }
                            }
                        }
                    }
                }
            };
            sites = _service.RetrieveMultiple(siteQuery);
            return sites.Entities?.Any() ?? false;
        }

        public bool DesignatedSiteCheck(Guid parentGuid, ITracingService tracing)
        {
            tracing.Trace("Entering DesignatedSiteCheck");
            var applicationWithDesignatedSite = @"<fetch mapping='logical' distinct='true'>
                            <entity name ='sdds_application'>
                               <attribute name= 'sdds_applicationid'/>
                                <attribute name= 'sdds_name' />  
                                <filter type='and'>
                                  <condition attribute='sdds_applicationid' operator='eq' value= '" + parentGuid + @"' />
                                 </filter>
                                  <link-entity name='sdds_designatedsites' from='sdds_applicationid' to='sdds_applicationid'  link-type='inner'/>
                                
                            </entity >
                          </fetch >";


            var applications = _service.RetrieveMultiple(new FetchExpression(applicationWithDesignatedSite));
            tracing.Trace(applications.Entities.Count().ToString());
            if (applications.Entities.Count() > 1)
            {
                return true;
            }
            else return false;
        }

        public Guid GetSpiceSubjectByApplicationType(Guid applicationTypeId, ITracingService tracing)
        {
            tracing.Trace("Entering GetSpiceSubjectByApplicationType");
            Guid spiceSubjectId = Guid.Empty;
            var applicationType = _service.Retrieve("sdds_applicationtypes", applicationTypeId, new ColumnSet(new string[] { "sdds_applicationtypesid", "sdds_speciesubjectid" }));
            if (applicationType != null)
            {
                if (!applicationType.Attributes.Contains("sdds_speciesubjectid") || applicationType.Attributes["sdds_speciesubjectid"] == null)
                    return spiceSubjectId;
                spiceSubjectId = applicationType.GetAttributeValue<EntityReference>("sdds_speciesubjectid").Id;

            }
            return spiceSubjectId;
        }

        /// <summary>
        /// Sets priority of a related application record based on the business rules.
        /// </summary>
        public void SetPriorityForLicensableActionConditions(Entity licensableAction, string messageName, Entity PostImage = null)
        {
            var isSetPriority = false;
            var applicationId = Guid.Empty;
            if (licensableAction.Attributes.Contains("sdds_applicationid")
                && licensableAction.Attributes["sdds_applicationid"] != null && messageName == "create")
            {
                applicationId = licensableAction.GetAttributeValue<EntityReference>("sdds_applicationid").Id;
                if (licensableAction.Attributes.Contains("sdds_setttype") && (licensableAction.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_alternative_sett_available
                  || licensableAction.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_no_alternative_sett)
                  && licensableAction.Attributes.Contains("sdds_method") && licensableAction.GetAttributeValue<OptionSetValueCollection>("sdds_method").Contains(new OptionSetValue((int)ApplicationEnum.License_Methods.Obstructing_Sett_Entrances)))
                {
                    isSetPriority = true;
                }
            }

            if (PostImage != null)
            {
                if (PostImage.Attributes.Contains("sdds_applicationid") && PostImage.GetAttributeValue<EntityReference>("sdds_applicationid") != null)
                    applicationId = PostImage.GetAttributeValue<EntityReference>("sdds_applicationid").Id;

                if (PostImage.Attributes.Contains("sdds_setttype") && (PostImage.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_alternative_sett_available
                                        || PostImage.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_no_alternative_sett)
                                        && PostImage.Attributes.Contains("sdds_method") && PostImage.GetAttributeValue<OptionSetValueCollection>("sdds_method").Contains(new OptionSetValue((int)ApplicationEnum.License_Methods.Obstructing_Sett_Entrances)))
                {
                    isSetPriority = true;
                }
            }
            if (applicationId != Guid.Empty)
            {
                if (isSetPriority)
                {
                    //Update the Application Priority.
                    _service.Update(new Entity("sdds_application", applicationId)
                    {
                        ["sdds_priority"] = new OptionSetValue((int)ApplicationEnum.Priority.two)
                    });

                }
                else
                {
                    _service.Update(new Entity("sdds_application", applicationId)
                    {
                        ["sdds_priority"] = new OptionSetValue((int)ApplicationEnum.Priority.four)
                    });
                }
            }



        }

        public void SetPriorityForDesignatedSite(Guid applicationId, int priorityvalue)
        {

            //Update the Application Priority.
            _service.Update(new Entity("sdds_application", applicationId)
            {
                ["sdds_priority"] = new OptionSetValue(priorityvalue)
            });

        }


        /// <summary>
        /// Updates the application priority if priority is not set to 1.
        /// </summary>
        /// <param name="applicationId">Application unique id.</param>
        /// <param name="value">Prioty value to set</param>
        /// <param name="service">Organization Service.</param>
        public void SetPriorityForRelatedAssociation(Guid applicationId, int value)
        {
            var application = _service.Retrieve("sdds_application", applicationId, new ColumnSet(new string[] { "sdds_priority" }));
            if (application == null || !application.Attributes.Contains("sdds_priority") ||
                (int)application.GetAttributeValue<OptionSetValue>("sdds_priority").Value != (int)ApplicationEnum.Priority.one)
                _service.Update(new Entity("sdds_application", applicationId)
                {
                    ["sdds_priority"] = new OptionSetValue(value)
                });
        }


        private void GetSeasonalWindowByApplicationType(Guid applicationTypeId)
        {
            var applicationType = _service.Retrieve("sdds_applicationtypes", applicationTypeId, new ColumnSet(new string[] { "sdds_applicationtypesid", "sdds_seasonfrom", "sdds_seasonto" }));
            if (applicationType != null)
            {
                if ((applicationType.Attributes.Contains("sdds_seasonfrom") || applicationType.Attributes["sdds_seasonfrom"] != null)
                    && (applicationType.Attributes.Contains("sdds_seasonto") || applicationType.Attributes["sdds_seasonto"] != null))
                {
                    SeasonFrom = applicationType.GetAttributeValue<OptionSetValue>("sdds_seasonfrom").Value;
                    SeasonTo = applicationType.GetAttributeValue<OptionSetValue>("sdds_seasonto").Value;

                }

            }

        }

        public bool LateCheck(Entity applicationEntity, ITracingService tracing)
        {
            return (DateTime.Now - applicationEntity.GetAttributeValue<DateTime>("sdds_licenceapplicationduedate")).Days < 5;
        }
    }
}
