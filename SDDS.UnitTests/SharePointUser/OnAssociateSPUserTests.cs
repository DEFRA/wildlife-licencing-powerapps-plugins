
namespace SDDS.UnitTests.SharePointUser
{
    using FakeXrmEasy.Abstractions;
    using FakeXrmEasy.Plugins;
    using FluentAssertions;
    using global::SDDS.Plugin.ApplicationPriority;
    using global::SDDS.Plugin.SharePointUser;
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Collections.Generic;
    using Xunit;
    public class OnAssociateSPUserTests : FakeXrmEasyTestsBase
    {
        readonly IXrmFakedContext fakecontext;
        Entity sharepointUser;
        ParameterCollection inputParameter;

        List<Guid> teamIds = new List<Guid>()
            {
                new Guid("91bef0ff-640c-ed11-b83d-002248c5c45b"),
                new Guid("9ea9c408-8e77-ec11-8d21-000d3a87431b"),
                new Guid("090c101d-5f0c-ed11-b83d-002248c5c45b"),
                new Guid("ca30e88e-7f0b-ed11-b83d-002248c5c45b"),
                new Guid("a3e42f5c-2651-ec11-8f8e-000d3a0ce458"),
                new Guid("70891a2c-810b-ed11-b83d-002248c5c45b"),
                new Guid("dec31450-810b-ed11-b83d-002248c5c45b"),
                new Guid("3b662452-7e0b-ed11-b83d-002248c5c45b"),
                new Guid("dc46b306-9687-ec11-93b0-0022481b40f8")
            };
        public OnAssociateSPUserTests()
        {
            fakecontext = this.xrmFakedContext;
            sharepointUser = new Entity("sdds_sharepointuser")
            {
                Id = Guid.NewGuid(),
                LogicalName = "sdds_sharepointuser"
            };


        }

        [Fact]
        public void On_associate_Of_sharepointuser()
        {
            // Arrange

            var teammember = new EntityReference("", teamIds[0]);
            inputParameter = new ParameterCollection
            {
                { "RelatedEntities", teammember }
            };

            // Mocking Context 
            var dataCollection = new List<Entity>
            {
               sharepointUser
            };
            fakecontext.Initialize(dataCollection);
            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.MessageName = "Associate";
            PlugCtx.PrimaryEntityId = sharepointUser.Id;
            PlugCtx.PrimaryEntityName = sharepointUser.LogicalName;
            PlugCtx.InputParameters = inputParameter;
           
       
            //// Act and Assert
            //fakecontext.ExecutePluginWith<OnAssociateSPUser>(PlugCtx);
            //var updatedApplication = fakecontext.GetOrganizationService().Retrieve("sdds_application", application.Id,
            //                          new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_priority" }));
            ////The Application Priority must be 4 to pass the test.
            //updatedApplication.GetAttributeValue<OptionSetValue>("sdds_priority").Value.Should().Be((int)ApplicationEnum.Priority.four, "The Application Priority must be 100000003 (4)");

        }

        [Fact]
        public void On_deassociate_Of_sharepointuser()
        {
            
        }

    }
}
