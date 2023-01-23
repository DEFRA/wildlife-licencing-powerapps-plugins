
namespace SDDS.UnitTests.MemberShip
{
    using FakeXrmEasy.Abstractions;
    using FakeXrmEasy.Plugins;
    using FluentAssertions;
    using global::SDDS.Plugin.MemberShip;
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Collections.Generic;
    using Xunit;
    public class MembershipChoiceTests : FakeXrmEasyTestsBase
    {
        readonly IXrmFakedContext fakecontext;
        Entity qualification;
        Entity membership;
        Entity licensemethod;
        ParameterCollection inputParameter;
        public MembershipChoiceTests()
        {
            fakecontext = this.xrmFakedContext;


        }

        [Fact]
        public void ExecuteLogic_on_membership_create()
        {
            membership = new Entity("sdds_membership");
            membership.Id = Guid.NewGuid();
            membership["sdds_name"] = "membership1";
            inputParameter = new ParameterCollection
            {
                { "Target", membership }
            };
            var dataCollection = new List<Entity>
            {
               membership
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Create", membership.LogicalName, membership.Id);
            // Act and Assert
            var plugin = fakecontext.ExecutePluginWith<CreateMemberShipChoice>(PlugCtx);
            plugin.Should().NotBeNull();
           
        }


        [Fact]
        public void ExecuteLogic_on_licensemethod_create()
        {
            licensemethod = new Entity("sdds_licensemethod");
            licensemethod.Id = Guid.NewGuid();
            licensemethod["sdds_methodname"] = "License method name1";
           
            inputParameter = new ParameterCollection
            {
                { "Target", licensemethod }
            };
            var dataCollection = new List<Entity>
            {
               licensemethod
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Create", licensemethod.LogicalName, licensemethod.Id);
            // Act and Assert
            var plugin = fakecontext.ExecutePluginWith<CreateMemberShipChoice>(PlugCtx);
            plugin.Should().NotBeNull();

        }

        //

        [Fact]
        public void ExecuteLogic_on_qualification_create()
        {
            qualification = new Entity("sdds_qualification");
            qualification.Id = Guid.NewGuid();
            qualification["sdds_name"] = "Qualification 1";

            inputParameter = new ParameterCollection
            {
                { "Target", qualification }
            };
            var dataCollection = new List<Entity>
            {
               qualification
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Create", qualification.LogicalName, qualification.Id);
            // Act and Assert
            var plugin = fakecontext.ExecutePluginWith<CreateMemberShipChoice>(PlugCtx);
            plugin.Should().NotBeNull();

        }

        [Fact]
        public void ExecuteLogic_on_membership_update()
        {
            membership = new Entity("sdds_membership");
            membership.Id = Guid.NewGuid();
            membership["sdds_name"] = "membership1";
            inputParameter = new ParameterCollection
            {
                { "Target", membership }
            };
            var dataCollection = new List<Entity>
            {
               membership
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Update", membership.LogicalName, membership.Id);
            var preImage = new Entity("sdds_membership");
            preImage["sdds_choicevalue"] = 100001;
            PlugCtx.PreEntityImages.Add("Image", preImage);
            // Act and Assert
            var plugin = fakecontext.ExecutePluginWith<CreateMemberShipChoice>(PlugCtx);
            plugin.Should().NotBeNull();

        }


        [Fact]
        public void ExecuteLogic_on_licensemethod_update()
        {
            licensemethod = new Entity("sdds_licensemethod");
            licensemethod.Id = Guid.NewGuid();
            licensemethod["sdds_methodname"] = "License method name1";

            inputParameter = new ParameterCollection
            {
                { "Target", licensemethod }
            };
            var dataCollection = new List<Entity>
            {
               licensemethod
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Update", licensemethod.LogicalName, licensemethod.Id);
            var preImage = new Entity("sdds_licensemethod");
            preImage["sdds_choicevalue"] = 100001;
            PlugCtx.PreEntityImages.Add("Image", preImage);
            // Act and Assert
            var plugin = fakecontext.ExecutePluginWith<CreateMemberShipChoice>(PlugCtx);
            plugin.Should().NotBeNull();

        }

        //

        [Fact]
        public void ExecuteLogic_on_qualification_update()
        {
            qualification = new Entity("sdds_qualification");
            qualification.Id = Guid.NewGuid();
            qualification["sdds_name"] = "Qualification 1";

            inputParameter = new ParameterCollection
            {
                { "Target", qualification }
            };
            var dataCollection = new List<Entity>
            {
               qualification
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Update", qualification.LogicalName, qualification.Id);
            var preImage = new Entity("sdds_qualification");
            preImage["sdds_choicevalue"] = 100001;
            PlugCtx.PreEntityImages.Add("Image", preImage);
            // Act and Assert
            var plugin = fakecontext.ExecutePluginWith<CreateMemberShipChoice>(PlugCtx);
            plugin.Should().NotBeNull();

        }


        [Fact]
        public void ExecuteLogic_on_membership_delete()
        {
            membership = new Entity("sdds_membership");
            membership.Id = Guid.NewGuid();
            membership["sdds_name"] = "membership1";
            inputParameter = new ParameterCollection
            {
                { "Target", membership }
            };
            var dataCollection = new List<Entity>
            {
               membership
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Delete", membership.LogicalName, membership.Id);
            var preImage = new Entity("sdds_membership");
            preImage["sdds_choicevalue"] = 100001;
            PlugCtx.PreEntityImages.Add("Image", preImage);
            // Act and Assert
            var plugin = fakecontext.ExecutePluginWith<CreateMemberShipChoice>(PlugCtx);
            plugin.Should().NotBeNull();

        }


        [Fact]
        public void ExecuteLogic_on_licensemethod_delete()
        {
            licensemethod = new Entity("sdds_licensemethod");
            licensemethod.Id = Guid.NewGuid();
            licensemethod["sdds_methodname"] = "License method name1";

            inputParameter = new ParameterCollection
            {
                { "Target", licensemethod }
            };
            var dataCollection = new List<Entity>
            {
               licensemethod
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Delete", licensemethod.LogicalName, licensemethod.Id);
            var preImage = new Entity("sdds_licensemethod");
            preImage["sdds_choicevalue"] = 100001;
            PlugCtx.PreEntityImages.Add("Image", preImage);
            // Act and Assert
            var plugin = fakecontext.ExecutePluginWith<CreateMemberShipChoice>(PlugCtx);
            plugin.Should().NotBeNull();

        }

        //

        [Fact]
        public void ExecuteLogic_on_qualification_delete()
        {
            qualification = new Entity("sdds_qualification");
            qualification.Id = Guid.NewGuid();
            qualification["sdds_methodname"] = "License method name1";

            inputParameter = new ParameterCollection
            {
                { "Target", qualification }
            };
            var dataCollection = new List<Entity>
            {
               qualification
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = GetFakedXrmContext(40, "Delete", qualification.LogicalName, qualification.Id);
            var preImage = new Entity("sdds_qualification");
            preImage["sdds_choicevalue"] = 100001;
            PlugCtx.PreEntityImages.Add("Image", preImage);
            // Act and Assert
            var plugin = fakecontext.ExecutePluginWith<CreateMemberShipChoice>(PlugCtx);
            plugin.Should().NotBeNull();

        }


        private XrmFakedPluginExecutionContext GetFakedXrmContext(int stage, string messageName, string primaryEntityName, Guid primaryEntityId)
        {
            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.Stage = stage;
            PlugCtx.MessageName = messageName;
            PlugCtx.PrimaryEntityId = primaryEntityId;
            PlugCtx.PrimaryEntityName = primaryEntityName;
            PlugCtx.InputParameters = inputParameter;

            return PlugCtx;
        }

    }
}
