namespace SDDS.UnitTests.ApplicationPriority
{
    using FakeXrmEasy.Abstractions;
    using FakeXrmEasy.Plugins;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using SDDS.Plugin.ApplicationPriority;
    using System;
    using System.Collections.Generic;
    using Xunit;
    public class ApplicationSetPriorityTests : FakeXrmEasyTestsBase
    {
        readonly IXrmFakedContext fakecontext;
        readonly Entity application;
        Entity applicationType;
        ParameterCollection inputParameter;
        public ApplicationSetPriorityTests()
        {
            fakecontext = this.xrmFakedContext;
            application = new Entity("sdds_application")
            {
                Id = Guid.NewGuid(),
                LogicalName = "sdds_application"
            };


        }

        [Fact]
        public void SetPriority_for_application_for_non_badger_and_a01Applicationtype()
        {
            // Arrange
            inputParameter = new ParameterCollection
            {
                { "Target", application }
            };
            //Set Application data for Application type = A01, SpiceSubject = Non Badger and Application Purpose other than 'Health and Safety'
            var datacollection = SetApplicationEntityData(true, false, false, false);
            fakecontext.Initialize(datacollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Create", "sdds_application");
            // Act and Assert
            fakecontext.ExecutePluginWith<ApplicationSetPriority>(PlugCtx);
            var updatedApplication = fakecontext.GetOrganizationService().Retrieve("sdds_application", application.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_priority" }));
            //The Application Priority must be 4 to pass the test.
            updatedApplication.GetAttributeValue<OptionSetValue>("sdds_priority").Value.Should().Be((int)ApplicationEnum.Priority.four, "The Application Priority must be 100000003 (4)");

        }

        [Fact]
        public void SetPriority_for_application_for_badger_and_a01Applicationtype_healthandsafetypurpose()
        {

            inputParameter = new ParameterCollection
            {
                { "Target", application }
            };
            //Set Application data for Application type = A01, SpiceSubject = Badger and Application Purpose = 'Health and Safety'
            var datacollection = SetApplicationEntityData(true, true, true, false);
            fakecontext.Initialize(datacollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Create", "sdds_application");
            // Act and Assert
            fakecontext.ExecutePluginWith<ApplicationSetPriority>(PlugCtx);
            var updatedApplication = fakecontext.GetOrganizationService().Retrieve("sdds_application", application.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_priority" }));
            //The Application Priority must be 4 to pass the test.
            updatedApplication.GetAttributeValue<OptionSetValue>("sdds_priority").Value.Should().Be((int)ApplicationEnum.Priority.one,
                    "The Application Priority must be 100000002 (1)");

        }
        [Fact]
        public void SetPriority_for_application_for_badger_and_a01Applicationtype_not_healthandsafetypurpose()
        {

            // Arrange
            inputParameter = new ParameterCollection
            {
                { "Target", application }
            };
            //Set Application data for Application type = A01, SpiceSubject = Badger and Application Purpose other than 'Health and Safety'
            var datacollection = SetApplicationEntityData(true, true, false, false);
            fakecontext.Initialize(datacollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Create", "sdds_application");
            // Act and Assert
            fakecontext.ExecutePluginWith<ApplicationSetPriority>(PlugCtx);
            var updatedApplication = fakecontext.GetOrganizationService().Retrieve("sdds_application", application.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_priority" }));
            //The Application Priority must be 4 to pass the test.
            updatedApplication.GetAttributeValue<OptionSetValue>("sdds_priority").Value.
                Should().Be((int)ApplicationEnum.Priority.four, "The Application Priority must be 100000003 (4)");

        }

        [Fact]
        public void SetPriority_for_application_for_badger_non_a01Applicationtype_not_healthandsafetypurpose()
        {
            // Arrange
            inputParameter = new ParameterCollection
            {
                { "Target", application }
            };
            //Set Application data for Application type != A01, SpiceSubject != Badger and Application Purpose other than 'Health and Safety'
            var datacollection = SetApplicationEntityData(false, false, false, false);
            fakecontext.Initialize(datacollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Create", "sdds_application");
            // Act and Assert
            fakecontext.ExecutePluginWith<ApplicationSetPriority>(PlugCtx);
            var updatedApplication = fakecontext.GetOrganizationService().Retrieve("sdds_application", application.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_priority" }));
            //The Application Priority must be 4 to pass the test.
            updatedApplication.GetAttributeValue<OptionSetValue>("sdds_priority").Value.
                Should().Be((int)ApplicationEnum.Priority.four, "The Application Priority must be 100000003 (4)");
        }

        [Fact]
        public void SetPriority_for_application_for_badger_and_mainsetttype_and_obstructingentrance()
        {
            // Arrange
            inputParameter = new ParameterCollection
            {
                { "Target", application }
            };
            //Set Application data for Application type != A01, SpiceSubject = Badger and Application Purpose other than 'Health and Safety'
            var datacollection = SetApplicationEntityData(false, true, false, true);
            fakecontext.Initialize(datacollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Create", "sdds_application");
            // Act and Assert
            fakecontext.ExecutePluginWith<ApplicationSetPriority>(PlugCtx);
            var updatedApplication = fakecontext.GetOrganizationService().Retrieve("sdds_application", application.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_priority" }));
            //The Application Priority must be 4 to pass the test.
            updatedApplication.GetAttributeValue<OptionSetValue>("sdds_priority").Value.
                Should().Be((int)ApplicationEnum.Priority.two, "The Application Priority must be 100000001 (2)");
        }
        [Fact]
        public void SetPriority_for_application_for_badger_and_das_pss_true()
        {
            // Arrange
            inputParameter = new ParameterCollection
            {
                { "Target", application }
            };
            //Set Application data for Application type != A01, SpiceSubject = Badger and Application Purpose other than 'Health and Safety'
            var datacollection = SetApplicationEntityData(false, true, false, false);
            application["sdds_dasdiscretionaryadvice"] = true;
            application["sdds_psspresubmissionscreening"] = true;
            fakecontext.Initialize(datacollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Update", "sdds_application");
            // Act and Assert
            fakecontext.ExecutePluginWith<ApplicationSetPriority>(PlugCtx);
            var updatedApplication = fakecontext.GetOrganizationService().Retrieve("sdds_application", application.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_priority" }));
            //The Application Priority must be 4 to pass the test.
            updatedApplication.GetAttributeValue<OptionSetValue>("sdds_priority").Value.
                Should().Be((int)ApplicationEnum.Priority.two, "The Application Priority must be 100000001 (2)");
        }

        [Fact]
        public void SetPriority_for_application_for_badger_and_das_pss_Not_true()
        {
            // Arrange
            inputParameter = new ParameterCollection
            {
                { "Target", application }
            };
            //Set Application data for Application type != A01, SpiceSubject = Badger and Application Purpose other than 'Health and Safety'
            var datacollection = SetApplicationEntityData(false, true, false, false);
            application["sdds_dasdiscretionaryadvice"] = false;
            application["sdds_psspresubmissionscreening"] = false;
            fakecontext.Initialize(datacollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Update", "sdds_application");
            // Act and Assert
            fakecontext.ExecutePluginWith<ApplicationSetPriority>(PlugCtx);
            var updatedApplication = fakecontext.GetOrganizationService().Retrieve("sdds_application", application.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_priority" }));
            //The Application Priority must be 4 to pass the test.
            updatedApplication.GetAttributeValue<OptionSetValue>("sdds_priority").Value.
                Should().Be((int)ApplicationEnum.Priority.four, "The Application Priority must be 100000003 (4)");
        }

        private XrmFakedPluginExecutionContext GetFakedXrmContext(int stage, string messageName, string primaryEntityName)
        {
            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.Stage = stage;
            PlugCtx.MessageName = messageName;
            PlugCtx.PrimaryEntityId = application.Id;
            PlugCtx.PrimaryEntityName = primaryEntityName;
            PlugCtx.InputParameters = inputParameter;

            return PlugCtx;
        }

        /// <summary>
        /// Sets the Application data for Unit test.
        /// </summary>
        /// <param name="isA01ApplicationType">Flag to indicate the application type is A01</param>
        /// <param name="isBadgerSpiceSubject">Flag to indicate the Spice subject is Badger</param>
        /// <param name="healthAndSafetyPurpose">Flag to indicate the application purpose as health and safety.</param>
        /// <returns></returns>
        private List<Entity> SetApplicationEntityData(bool isA01ApplicationType, bool isBadgerSpiceSubject, bool healthAndSafetyPurpose, bool isMainSettType)
        {
            var spiceSubject = new Entity("sdds_speciesubject");
            var applicationPurpose = new Entity("sdds_applicationpurpose");
            var licensableAction = new Entity("sdds_licensableaction");
            var site = new Entity("sdds_site");
            applicationType = new Entity("sdds_applicationtypes");

            //Spice Subject 
            spiceSubject.Id = isBadgerSpiceSubject ? new Guid("60ce79d8-87fb-ec11-82e5-002248c5c45b") : Guid.NewGuid();
            //Application Type 
            applicationType.Id = isA01ApplicationType ? new Guid("f99b0a3b-6c58-ec11-8f8f-000d3a0ce11e") : Guid.NewGuid();
            applicationType["sdds_speciesubjectid"] = new EntityReference("sdds_speciesubject", spiceSubject.Id);
            applicationType["sdds_seasonfrom"] = new OptionSetValue(2);
            applicationType["sdds_seasonto"] = new OptionSetValue(8);
           
            //Application Purpose.
            applicationPurpose.Id = healthAndSafetyPurpose ? new Guid("571706d8-54a8-ec11-9840-0022481aca85") : Guid.NewGuid();
            //Licensable Action.
            licensableAction.Id = Guid.NewGuid();
            licensableAction["sdds_setttype"] = isMainSettType
                ? new OptionSetValue((int)ApplicationEnum.SettType.Main_no_alternative_sett)
                : (object)new OptionSetValue((int)ApplicationEnum.SettType.Annex_Subsidiary);
            licensableAction["sdds_method"] = new OptionSetValueCollection(new List<OptionSetValue>
                        { new OptionSetValue((int)ApplicationEnum.License_Methods.Obstructing_Sett_Entrances) });
            licensableAction["sdds_applicationid"] = new EntityReference("sdds_application", application.Id);
            //Site
            site.Id = Guid.NewGuid();
            site["sdds_applicationid"] = new EntityReference("sdds_application", application.Id);
            //Application.
            application["sdds_applicationtypesid"] = new EntityReference("sdds_applicationtypes", applicationType.Id);
            application["sdds_applicationpurpose"] = new EntityReference("sdds_applicationpurpose", applicationPurpose.Id);
          
            var dataCollection = new List<Entity>
            {
               spiceSubject, applicationType,applicationPurpose,licensableAction,site, application
            };

            return dataCollection;
        }
    }
}
