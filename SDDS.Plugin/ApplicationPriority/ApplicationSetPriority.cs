using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

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
            try
            {
                var logic = new AssignPriorityLogic(service);
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity entity)
                {
                    Entity licenseApp = entity;

                    if (licenseApp.LogicalName == "sdds_application" && context.MessageName.ToLower() == "create")
                    {
                        tracing.Trace("Entering On Create of Application");
                        SetPriorityOnApplicationCreate(licenseApp, service, tracing, context);
                    }
                    else if (licenseApp.LogicalName == "sdds_application" && context.MessageName.ToLower() == "update")
                    {
                        tracing.Trace("Entering On Update of Application");
                        SetPriorityOnApplicationUpdate(licenseApp, service, tracing, context);
                    }
                    else if (licenseApp.LogicalName == "sdds_licensableaction" && context.MessageName.ToLower() == "create")
                    {
                        tracing.Trace("Entering On Create of Licensable Action.");
                        logic.SetPriorityForLicensableActionConditions(licenseApp, "create");
                    }
                    else if (licenseApp.LogicalName == "sdds_licensableaction" && context.MessageName.ToLower() == "update")
                    {
                        tracing.Trace("Entering On Update of Licensable Action.");
                        Entity postImageEntity = null;
                        if (context.PostEntityImages.Contains("PostImage"))
                            postImageEntity = (Entity)context.PostEntityImages["PostImage"];
                        logic.SetPriorityForLicensableActionConditions(licenseApp, "update", postImageEntity);
                    }
                    else if (licenseApp.LogicalName == "sdds_designatedsites" &&
                            (context.MessageName.ToLower() == "create" || context.MessageName.ToLower() == "update"))
                    {
                        tracing.Trace("Entering On Create/Update of Designated Site.");
                        if (licenseApp.Attributes.Contains("sdds_applicationid"))
                        {
                            var application = (EntityReference)licenseApp.Attributes["sdds_applicationid"];
                            logic.SetPriorityForDesignatedSite(application.Id, (int)ApplicationEnum.Priority.two);
                        }

                    }
                }
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference reference)
                {
                    if (context.MessageName.ToLower() == "associate")
                    {
                        tracing.Trace("Entering On Association between Application and Site.");
                        if (reference.LogicalName != "sdds_application")
                            return;
                        if (!context.InputParameters.Contains("Relationship"))
                            return;
                        var relationship = (Relationship)context.InputParameters["Relationship"];
                        if (relationship.SchemaName != "sdds_application_sdds_site_sdds_site")
                            return;
                        //Set the Application Priority.
                        logic.SetPriorityForRelatedAssociation(reference.Id, (int)ApplicationEnum.Priority.two);
                    }
                }
            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, this.GetType().Name);
                throw new InvalidPluginExecutionException(ex.Message);

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
        private void SetPriorityOnApplicationCreate(Entity applicationEntity, IOrganizationService service, ITracingService tracing, IPluginExecutionContext context)
        {
            var entityId = applicationEntity.Id;
            var licenseType = applicationEntity.GetAttributeValue<EntityReference>("sdds_applicationtypesid");
            var applicationType = licenseType.ToEntity(service, "sdds_speciesubjectid", "sdds_type");
            var specieSubject = applicationType.GetAttributeValue<EntityReference>("sdds_speciesubjectid")?.ToEntity(service, "sdds_subject");

            try
            {
                var logic = new AssignPriorityLogic(service);
                var application = service.Retrieve("sdds_application", entityId, new ColumnSet("sdds_priority"));
                if (specieSubject.GetAttributeValue<OptionSetValue>("sdds_subject").Value == (int)ApplicationEnum.SpecieSubjects.Badgers)
                {
                    if (applicationType.GetAttributeValue<OptionSetValue>("sdds_type").Value == (int)ApplicationEnum.ApplicationTypes.A24 && logic.GetPurpose(applicationEntity))
                    {
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.one, service);
                    }
                    else if (logic.CheckSettTypeAndMethod(entityId, tracing))
                    {
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.ExistingSiteCheck(entityId, tracing) &&
                        application.GetAttributeValue<OptionSetValue>("sdds_priority")?.Value != (int)ApplicationEnum.Priority.one)
                    {
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.MultiPlots(entityId, tracing) &&
                        application.GetAttributeValue<OptionSetValue>("sdds_priority")?.Value != (int)ApplicationEnum.Priority.one)
                    {
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.DesignatedSiteCheck(entityId, tracing) &&
                        application.GetAttributeValue<OptionSetValue>("sdds_priority")?.Value != (int)ApplicationEnum.Priority.one)
                    {
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.LateCheck(applicationEntity, tracing) &&
                        application.GetAttributeValue<OptionSetValue>("sdds_priority")?.Value != (int)ApplicationEnum.Priority.one)
                    {
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.three, service);
                    }
                    else if (logic.SeasonalCheck(applicationEntity, applicationType.Id, tracing) &&
                        application.GetAttributeValue<OptionSetValue>("sdds_priority")?.Value != (int)ApplicationEnum.Priority.one)
                    {
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else if (logic.DASSPSS(applicationEntity) &&
                        application.GetAttributeValue<OptionSetValue>("sdds_priority")?.Value != (int)ApplicationEnum.Priority.one)
                    {
                        tracing.Trace("Entering update for DASSPSS");
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else { UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.four, service); }
                }
                else { UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.four, service); }
            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                tracing.Trace(ex.StackTrace);
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, this.GetType().Name);
                throw new InvalidPluginExecutionException(ex.Message);

            }
        }

        /// <summary>
        /// Sets the Application Priority on the update of a License application based on the rules.
        /// </summary>
        /// <param name="applicationEntity"></param>
        /// <param name="service"></param>
        /// <param name="tracing"></param>
        /// <exception cref="InvalidPluginExecutionException"></exception>
        private void SetPriorityOnApplicationUpdate(Entity applicationEntity, IOrganizationService service, ITracingService tracing, IPluginExecutionContext context)
        {

            var entityId = applicationEntity.Id;
            var licenseType = applicationEntity.GetAttributeValue<EntityReference>("sdds_applicationtypesid");
            var applicationType = licenseType.ToEntity(service, "sdds_speciesubjectid", "sdds_type");
            var specieSubject = applicationType.GetAttributeValue<EntityReference>("sdds_speciesubjectid")?.ToEntity(service, "sdds_subject");

            try
            {
                var logic = new AssignPriorityLogic(service);
                if (specieSubject.GetAttributeValue<OptionSetValue>("sdds_subject").Value == (int)ApplicationEnum.SpecieSubjects.Badgers)
                {
                    if (applicationType.GetAttributeValue<OptionSetValue>("sdds_type").Value == (int)ApplicationEnum.ApplicationTypes.A24 && logic.GetPurpose(applicationEntity))
                    {
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.one, service);
                    }
                    else if (logic.DASSPSS(applicationEntity))
                    {
                        tracing.Trace("Entering DASSPSS for APplication update");
                        UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.two, service);
                    }
                    else { UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.four, service); }
                }
                else { UpdateEntity(applicationEntity, (int)ApplicationEnum.Priority.four, service); }
            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, "ApplicationSetPriority");
                throw new InvalidPluginExecutionException(ex.Message);

            }
        }


    }
}
