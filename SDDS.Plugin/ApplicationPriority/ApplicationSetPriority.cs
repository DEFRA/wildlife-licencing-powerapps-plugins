using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.ApplicationPriority
{
    public class ApplicationSetPriority : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            tracing.Trace("Entering Plugin");
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity entity)
            {
                Entity licenseApp = entity;
                if (licenseApp.LogicalName == "sdds_application" && context.MessageName.ToLower() == "create")
                {
                    tracing.Trace("Entering On Create of Application");
                    SetPriorityOnApplicationCreate(licenseApp, service, tracing);
                }
                else if (licenseApp.LogicalName == "sdds_licensableaction" && context.MessageName.ToLower() == "create")
                {
                    tracing.Trace("Entering On Create of Licensable Action.");
                    SetPriorityForLicensableActionConditions(licenseApp, service, null);
                }
                else if (licenseApp.LogicalName == "sdds_licensableaction" && context.MessageName.ToLower() == "update")
                {
                    tracing.Trace("Entering On Update of Licensable Action.");
                    Entity preImageEntity = null;
                    if (context.PreEntityImages.Contains("PreImage"))
                        preImageEntity = (Entity)context.PreEntityImages["PreImage"];
                    SetPriorityForLicensableActionConditions(licenseApp, service, preImageEntity);
                }
            }
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference reference)
            {
                if (context.MessageName.ToLower() == "associate")
                {
                    tracing.Trace("Entering On Association between Application and Designated Site.");
                    if (reference.LogicalName == "sdds_application")
                    {
                        if (context.InputParameters.Contains("Relationship"))
                        {
                            var relationship = (Relationship)context.InputParameters["Relationship"];
                           if (relationship.SchemaName != "sdds_sdds_application_sdds_designatedsites") { return; }
                            //Set the Application Priority.
                            SetPriorityForAssociatedDesignatedSites(reference.Id, (int)ApplicationEnum.Priority.two, service);
                        }
                    }
                }
            }
            
        }
        public static void UpdateEntity(Entity entity, int value, IOrganizationService service)
        {
            service.Update(new Entity(entity.LogicalName, entity.Id)
            {
                ["sdds_priority"] = new OptionSetValue(value)
            }); 
        }

        /// <summary>
        /// Sets the Application priority based on different Business Rules.
        /// </summary>
        /// <param name="applicationEntity">Application entity</param>
        /// <param name="service">Organization service</param>
        /// <param name="tracing">Tracing service</param>
        /// <exception cref="InvalidPluginExecutionException"></exception>
        private void SetPriorityOnApplicationCreate(Entity applicationEntity, IOrganizationService service, ITracingService tracing)
        {

            Guid a01ApplicationType = new Guid("f99b0a3b-6c58-ec11-8f8f-000d3a0ce11e");// A01 Application Type.
            Guid badgerSpiceSubject = new Guid("60ce79d8-87fb-ec11-82e5-002248c5c45b");
            // Guid eps = new Guid("05e8951c-f452-ec11-8f8e-000d3a0ce458");
            var entityId = applicationEntity.Id;
            Guid licenseTypeId = applicationEntity.GetAttributeValue<EntityReference>("sdds_applicationtypesid").Id;

            try
            {
                var logic = new AssignPriorityLogic();

                if (logic.GetSpiceSubjectByApplicationType(service, licenseTypeId, tracing) == badgerSpiceSubject)
                {
                    if (licenseTypeId == a01ApplicationType && logic.GetPurpose(applicationEntity))
                    {
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.one, service);
                    }
                    //else if (logic.CheckBadgerBeaverSpecie(service, entityId) == "beaver" && licenseTypeId == badger)
                    //{
                    //    UpdateEntity(licenseApp, (int)ApplicationEnum.Priority.one, service);
                    //}else if (logic.CheckBuzzardRavenRedKiteSpecie(service, entityId) && licenseTypeId == badger)
                    //{
                    //    tracing.Trace("Entering CheckBuzzardRavenRedKiteSpecie");
                    //    UpdateEntity(licenseApp, (int)ApplicationEnum.Priority.one, service);
                    //}
                    //else if (logic.CheckBadgerBeaverSpecie(service,entityId) == "badger" && logic.CheckSettTypeAndMethod(service, entityId, tracing))
                    //{
                    //    tracing.Trace("Entering CheckBadgerBeaverSpecie 2");
                    //    UpdateEntity(licenseApp, (int)ApplicationEnum.Priority.one, service);
                    //}
                    else if (logic.CheckSettTypeAndMethod(service, entityId, tracing))
                    {
                        tracing.Trace("Entering CheckSettTypeAndMethod");
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.ExistingSiteCheck(service, entityId, tracing))
                    {
                        tracing.Trace("Entering ExistingSiteCheck");
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.MultiPlots(service, entityId, tracing))
                    {
                        tracing.Trace("Entering MultiPlots");
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.DesignatedSiteCheck(service, entityId, tracing))
                    {
                        tracing.Trace("Entering DesignatedSiteCheck");
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.SeasonalCheck(applicationEntity, service, licenseTypeId, tracing))
                    {
                        tracing.Trace("Entering SeasonalCheck");
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.DASSPSS(applicationEntity))
                    {
                        tracing.Trace("Entering DASSPSS");
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else { UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.four, service); }
                }
                else { UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.four, service); }
            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                throw new InvalidPluginExecutionException(ex.Message);

            }
        }

        /// <summary>
        /// Sets priority of a related application record based on the business rules.
        /// </summary>
        private void SetPriorityForLicensableActionConditions(Entity licensableAction, IOrganizationService service, Entity PreImage)
        {
            var isUpdate = false;
            if (licensableAction.Attributes.Contains("sdds_applicationid") || licensableAction.Attributes["sdds_applicationid"] != null)
            {
                var applicationId = licensableAction.GetAttributeValue<EntityReference>("sdds_applicationid").Id;
                if (PreImage != null)
                {
                    if (PreImage.Attributes.Contains("sdds_setttype") && (PreImage.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_alternative_sett_available
                                           || PreImage.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_no_alternative_sett)
                                           && PreImage.Attributes.Contains("sdds_method") && PreImage.GetAttributeValue<OptionSetValueCollection>("sdds_method").Contains(new OptionSetValue((int)ApplicationEnum.License_Methods.Obstructing_Sett_Entrances)))
                    {
                        isUpdate = true;
                    }
                }
                else
                {
                    if (licensableAction.Attributes.Contains("sdds_setttype") && (licensableAction.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_alternative_sett_available
                       || licensableAction.GetAttributeValue<OptionSetValue>("sdds_setttype").Value == (int)ApplicationEnum.SettType.Main_no_alternative_sett)
                       && licensableAction.Attributes.Contains("sdds_method") && licensableAction.GetAttributeValue<OptionSetValueCollection>("sdds_method").Contains(new OptionSetValue((int)ApplicationEnum.License_Methods.Obstructing_Sett_Entrances)))
                    {
                        isUpdate = true;
                    }
                }
                if(isUpdate)
                {
                    service.Update(new Entity("sdds_application", applicationId)
                    {
                        ["sdds_priority"] = new OptionSetValue((int)ApplicationEnum.Priority.two)
                    });
                }

            }

        }

        /// <summary>
        /// Updates the application priority if priority is not set to 1.
        /// </summary>
        /// <param name="applicatoinId">Application unique id.</param>
        /// <param name="value">Prioty value to set</param>
        /// <param name="service">Organization Service.</param>
        private void SetPriorityForAssociatedDesignatedSites(Guid applicatoinId, int value, IOrganizationService service)
        {
           var application = service.Retrieve("sdds_application", applicatoinId, new ColumnSet(new string[] { "sdds_priority" }));
            if (application != null && application.Attributes.Contains("sdds_priority") &&
                (int)application.GetAttributeValue<OptionSetValue>("sdds_priority").Value == (int)ApplicationEnum.Priority.one)
                return;

            service.Update(new Entity("sdds_application", applicatoinId)
            {
                ["sdds_priority"] = new OptionSetValue(value)
            });
        }

    }
}
