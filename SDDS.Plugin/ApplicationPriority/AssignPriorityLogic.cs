using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
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

        public int SeasonFrom { get; set; }
        public int SeasonTo { get; set; }


        Guid healthAndSafety = new Guid("571706d8-54a8-ec11-9840-0022481aca85"); //Public Health and Safety purpose
        public bool GetPurpose(Entity entity)
        {
            var id = entity.GetAttributeValue<EntityReference>("sdds_applicationpurpose").Id;
            if (id == healthAndSafety) return true;
           else  return false;
        }

        public EntityCollection GetLicensableAction(IOrganizationService service, Guid parentGuid)
        {
            QueryExpression specie = new QueryExpression("sdds_licensableaction");
            specie.ColumnSet.AddColumns("sdds_applicationid", "sdds_licensableactionid", "sdds_setttype", "sdds_specieid", "sdds_method", "sdds_maximumnoofsubject");
            specie.Criteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, parentGuid);

            var entity = service.RetrieveMultiple(specie);

            return entity;
        }
        public string CheckBadgerBeaverSpecie(IOrganizationService service, Guid parentGuid)
        {
            var actions = GetLicensableAction(service, parentGuid);
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

        public bool CheckBuzzardRavenRedKiteSpecie(IOrganizationService service, Guid parentGuid)
        {
            var actions = GetLicensableAction(service, parentGuid);
            if (actions == null) return false;

            var species = actions.Entities.Where(x => (x.GetAttributeValue<EntityReference>("sdds_specieid").Id == new Guid("d85612c4-53a8-ec11-9840-0022481aca85")) ||
                         (x.GetAttributeValue<EntityReference>("sdds_specieid").Id == new Guid("e22614ca-53a8-ec11-9840-0022481aca85")) || 
                         (x.GetAttributeValue<EntityReference>("sdds_specieid").Id == new Guid("3b8d5ad6-53a8-ec11-9840-0022481aca85")));
            if (species.Count() > 0)
            {
                return true;
            }
            else return false ;
        }

        public bool CheckSettTypeAndMethod(IOrganizationService service, Guid parentGuid, ITracingService tracing)
        {
            var actions = GetLicensableAction(service, parentGuid);
            tracing.Trace("getting licensable actions:" + actions.Entities.Count().ToString());
            if (actions == null) return false;

            //If licensable action is of Set type ==MAIN &&
            //Licensable action/Method= Obstructing sett entrances by means of one-way badger gates 
            tracing.Trace("getting licensable actions");
            var licenseMethods = actions.Entities.Where(x => (x.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_alternative_sett_available ||
                         x.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_no_alternative_sett) &&
                         x.GetAttributeValue<OptionSetValueCollection>("sdds_method").Contains(new OptionSetValue((int)ApplicationEnum.License_Methods.Obstructing_Sett_Entrances))).FirstOrDefault();

            tracing.Trace("licenseMethods is null");
            if (licenseMethods == null) return false; else return true;
        }

        public bool MultiPlots(IOrganizationService service, Guid parentGuid, ITracingService tracing)
        {
            // Instantiate QueryExpression query
            var query = new QueryExpression("sdds_site");

            // Add columns to query.ColumnSet
            query.ColumnSet.AddColumns("sdds_name", "sdds_siteid");

            // Add link-entity query_sdds_application_sdds_site
            var query_sdds_application_sdds_site = query.AddLink("sdds_application_sdds_site", "sdds_siteid", "sdds_siteid");

            // Define filter query_sdds_application_sdds_site.LinkCriteria
            query_sdds_application_sdds_site.LinkCriteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, parentGuid);
            var sites = service.RetrieveMultiple(query);
            
            if (sites == null) return false;
            if (sites.Entities.Count() > 1)
            {
                return true;
            } else return false;
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

        public bool SeasonalCheck(Entity entity, IOrganizationService service, Guid licenseTypeId, ITracingService tracing)
        {
            var createdOn = entity.GetAttributeValue<DateTime>("createdon");
            GetSeasonalWindowByApplicationType(service,licenseTypeId);
            //var startDay = DateTime.DaysInMonth(DateTime.Now.Year, 6);
            //var endDay = DateTime.DaysInMonth(DateTime.Now.Year, 10);
            //  return createdOn >= new DateTime(DateTime.Now.Year, 6, startDay) && createdOn <= new DateTime(DateTime.Now.Year, 10, endDay);
            tracing.Trace("SeasonFrom "+ SeasonFrom);
            tracing.Trace("SeasonTo " + SeasonTo);
            return createdOn.Month >= SeasonFrom && createdOn.Month <= SeasonTo;
        }

        public bool ExistingSiteCheck(IOrganizationService service, Guid parentGuid, ITracingService tracing)
        {
            // Instantiate QueryExpression query
            var query = new QueryExpression("sdds_site");

            // Add columns to query.ColumnSet
            query.ColumnSet.AddColumns("sdds_name", "sdds_siteid");

            // Add link-entity query_sdds_application_sdds_site
            var query_sdds_application_sdds_site = query.AddLink("sdds_application_sdds_site", "sdds_siteid", "sdds_siteid");

            // Define filter query_sdds_application_sdds_site.LinkCriteria
            query_sdds_application_sdds_site.LinkCriteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, parentGuid);
            var site = service.RetrieveMultiple(query).Entities.FirstOrDefault();
            tracing.Trace("entered ExistingSiteCheck");
            if (site == null) return false;

            var siteId = site.Id;

            var unique = @"<fetch mapping='logical' distinct='true'>
                            <entity name ='sdds_application'>
                               <attribute name= 'sdds_applicationid'/>
                                <attribute name= 'sdds_name' />    
                                 <attribute name = 'createdon' />   
                                  <order attribute = 'createdon' descending ='true' />        
                                   <link-entity name = 'sdds_application_sdds_site' from = 'sdds_applicationid' to = 'sdds_applicationid' visible = 'false' intersect = 'true' >                  
                                    <link-entity name = 'sdds_site' from = 'sdds_siteid' to = 'sdds_siteid' alias = 'ab' >                           
                                     <filter type = 'and' >                             
                                      <condition attribute = 'sdds_siteid' operator= 'eq' uitype = 'sdds_site' value = '" + siteId + @"' />                                   
                                       </filter >                                      
                                    </link-entity >                                     
                                   </link-entity >                                     
                            </entity >
                          </fetch >";

            //var unique = new QueryExpression("sdds_application");
            //query.ColumnSet.AllColumns = false;
            //var linedEnt = query.AddLink("sdds_application_sdds_site", "sdds_applicationid", "sdds_applicationid");
            //linedEnt.LinkCriteria.AddCondition("sdds_siteid", ConditionOperator.Equal, siteId);
            
            var applications = service.RetrieveMultiple(new FetchExpression(unique));

            tracing.Trace(applications.Entities.Count().ToString());
            if (applications.Entities.Count() > 1)
            {
                return true;
            }
            else return false;
        }

        public bool DesignatedSiteCheck(IOrganizationService service, Guid parentGuid, ITracingService tracing)
        {
            
            var applicationWithDesignatedSite = @"<fetch mapping='logical' distinct='true'>
                            <entity name ='sdds_application'>
                               <attribute name= 'sdds_applicationid'/>
                                <attribute name= 'sdds_name' />  
                                <filter type='and'>
                                  <condition attribute='sdds_applicationid' operator='eq' value= '"+parentGuid+ @"' />
                                 </filter>
                                 <link-entity name='sdds_sdds_application_sdds_designatedsites' from='sdds_applicationid' to ='sdds_applicationid' visible='false' intersect='true'>      
                                    <link-entity name='sdds_designatedsites' from='sdds_designatedsitesid' to='sdds_designatedsitesid'/>
                                 </link-entity>
                            </entity >
                          </fetch >";


            var applications = service.RetrieveMultiple(new FetchExpression(applicationWithDesignatedSite));
            tracing.Trace(applications.Entities.Count().ToString());
            if (applications.Entities.Count() > 1)
            {
                return true;
            }
            else return false;
        }

        public Guid GetSpiceSubjectByApplicationType(IOrganizationService service, Guid applicationTypeId, ITracingService tracing)
        {
            tracing.Trace("Entering GetSpiceSubjectByApplicationType");
            Guid spiceSubjectId = Guid.Empty;
            var applicationType = service.Retrieve("sdds_applicationtypes", applicationTypeId, new ColumnSet(new string[] { "sdds_applicationtypesid", "sdds_speciesubjectid" }));
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
        public void SetPriorityForLicensableActionConditions(Entity licensableAction, IOrganizationService service, string messageName, Entity PostImage = null)
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
          
            if (!isSetPriority || applicationId == Guid.Empty)
              return;
            //Update the Application Priority.
            service.Update(new Entity("sdds_application", applicationId)
            {
                ["sdds_priority"] = new OptionSetValue((int)ApplicationEnum.Priority.two)
            });



        }

        /// <summary>
        /// Updates the application priority if priority is not set to 1.
        /// </summary>
        /// <param name="applicatoinId">Application unique id.</param>
        /// <param name="value">Prioty value to set</param>
        /// <param name="service">Organization Service.</param>
        public void SetPriorityForRelatedAssociation(Guid applicatoinId, int value, IOrganizationService service)
        {
            var application = service.Retrieve("sdds_application", applicatoinId, new ColumnSet(new string[] { "sdds_priority" }));
            if (application == null || !application.Attributes.Contains("sdds_priority") ||
                (int)application.GetAttributeValue<OptionSetValue>("sdds_priority").Value != (int)ApplicationEnum.Priority.one)
                service.Update(new Entity("sdds_application", applicatoinId)
                {
                    ["sdds_priority"] = new OptionSetValue(value)
                });
        }


        private void GetSeasonalWindowByApplicationType(IOrganizationService service, Guid applicationTypeId)
        {
            var applicationType = service.Retrieve("sdds_applicationtypes", applicationTypeId, new ColumnSet(new string[] { "sdds_applicationtypesid", "sdds_seasonfrom", "sdds_seasonto" }));
            if (applicationType != null)
            {
                if ((applicationType.Attributes.Contains("sdds_seasonfrom") || applicationType.Attributes["sdds_seasonfrom"] != null)
                    && (applicationType.Attributes.Contains("sdds_seasonto") || applicationType.Attributes["sdds_seasonto"] != null))
                {
                    SeasonFrom =  applicationType.GetAttributeValue<OptionSetValue>("sdds_seasonfrom").Value;
                    SeasonTo = applicationType.GetAttributeValue<OptionSetValue>("sdds_seasonto").Value;

                }

            }
            
        }
    }
}
