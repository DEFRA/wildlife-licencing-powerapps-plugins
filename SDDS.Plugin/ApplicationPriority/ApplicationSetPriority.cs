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
                var logic = new AssignPriorityLogic();
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity entity)
                {
                    Entity licenseApp = entity;

                    if (licenseApp.LogicalName == "sdds_application" && context.MessageName.ToLower() == "create")
                    {
                        tracing.Trace("Entering On Create of Application");
                        SetPriorityOnApplicationCreate(licenseApp, service, tracing);
                    }
                    else if (licenseApp.LogicalName == "sdds_application" && context.MessageName.ToLower() == "update")
                    {
                        tracing.Trace("Entering On Update of Application");
                        SetPriorityOnApplicationUpdate(licenseApp, service, tracing);
                    }
                    else if (licenseApp.LogicalName == "sdds_licensableaction" && context.MessageName.ToLower() == "create")
                    {
                        tracing.Trace("Entering On Create of Licensable Action.");
                        logic.SetPriorityForLicensableActionConditions(licenseApp, service, "create");
                    }
                    else if (licenseApp.LogicalName == "sdds_licensableaction" && context.MessageName.ToLower() == "update")
                    {
                        tracing.Trace("Entering On Update of Licensable Action.");
                        Entity postImageEntity = null;
                        if (context.PostEntityImages.Contains("PostImage"))
                            postImageEntity = (Entity)context.PostEntityImages["PostImage"];
                        logic.SetPriorityForLicensableActionConditions(licenseApp, service, "update", postImageEntity);
                    }
                    else if (licenseApp.LogicalName == "sdds_designatedsites" &&
                            (context.MessageName.ToLower() == "create" || context.MessageName.ToLower() == "update"))
                    {
                        tracing.Trace("Entering On Create/Update of Designated Site.");
                       if(licenseApp.Attributes.Contains("sdds_applicationid"))
                        {
                         var application  = (EntityReference)licenseApp.Attributes["sdds_applicationid"];
                            logic.SetPriorityForDesignatedSite(application.Id, service,(int)ApplicationEnum.Priority.two);
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
                        logic.SetPriorityForRelatedAssociation(reference.Id, (int)ApplicationEnum.Priority.two, service);
                    }
                }
            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
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
        /// Sets the Application Priority on the update of a License application based on the rules.
        /// </summary>
        /// <param name="applicationEntity"></param>
        /// <param name="service"></param>
        /// <param name="tracing"></param>
        /// <exception cref="InvalidPluginExecutionException"></exception>
        private void SetPriorityOnApplicationUpdate(Entity applicationEntity, IOrganizationService service, ITracingService tracing)
        {

            Guid a01ApplicationType = new Guid("f99b0a3b-6c58-ec11-8f8f-000d3a0ce11e");// A01 Application Type.
            Guid badgerSpiceSubject = new Guid("60ce79d8-87fb-ec11-82e5-002248c5c45b");
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
                throw new InvalidPluginExecutionException(ex.Message);

            }
        }


    }
}
