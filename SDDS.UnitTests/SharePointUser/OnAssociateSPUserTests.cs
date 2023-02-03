
namespace SDDS.UnitTests.SharePointUser
{
    using FakeXrmEasy.Abstractions;
    using FakeXrmEasy.Plugins;
    using FluentAssertions;
    using global::SDDS.Plugin.ApplicationPriority;
    using global::SDDS.Plugin.SharePointUser;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Collections.Generic;
    using System.Web.Services.Description;
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
        public void Add_user_to_team_when_sharepointuser_exists_and_remove_user_from_sharepoint_flag_is_no()
        {
            // Arrange

            var teammember = new EntityReference("team", teamIds[0]);
            var userdata = new Entity("systemuser");
            userdata.Id = teamIds[0];
            Relationship relationship = new Relationship("teammembership_association");
            EntityReferenceCollection relatedEntities = new EntityReferenceCollection();
            sharepointUser["sdds_name"] = "SharePoint User test";
            sharepointUser["sdds_removeuserfromsharepointgroup"] = false;
            sharepointUser["sdds_sharepointuserid"] = sharepointUser.Id;
            relatedEntities.Add(new EntityReference("systemuser", userdata.Id));
            sharepointUser["sdds_userid"] = new EntityReference("systemuser", userdata.Id);

            inputParameter = new ParameterCollection
            {
                { "Target", teammember},
                {"Relationship", relationship },
                 {"RelatedEntities", relatedEntities }
            };

            // Mocking Context 
            var dataCollection = new List<Entity>
            {
               sharepointUser, userdata
            };
            fakecontext.Initialize(dataCollection);
            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.MessageName = "Associate";
            PlugCtx.PrimaryEntityId = sharepointUser.Id;
            PlugCtx.PrimaryEntityName = sharepointUser.LogicalName;
            PlugCtx.InputParameters = inputParameter;

            //// Act and Assert
            fakecontext.ExecutePluginWith<OnAssociateSPUser>(PlugCtx);
            var updatedSharePointUser = fakecontext.GetOrganizationService().Retrieve("sdds_sharepointuser", sharepointUser.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_removeuserfromsharepointgroup" }));
            //The remove user from sharepoint must be 'false' as there is no update.
            updatedSharePointUser.GetAttributeValue<bool>("sdds_removeuserfromsharepointgroup").Should().Be(false,"The Remove user from SharePointUser must be false");

        }


        [Fact]
        public void Add_user_to_team_when_sharepointuser_exists_and_remove_user_from_sharepoint_flag_is_yes()
        {
            // Arrange

            var teammember = new EntityReference("team", teamIds[0]);
            var userdata = new Entity("systemuser");
            userdata.Id = teamIds[0];
            Relationship relationship = new Relationship("teammembership_association");
            EntityReferenceCollection relatedEntities = new EntityReferenceCollection();
            sharepointUser["sdds_name"] = "SharePoint User test";
            sharepointUser["sdds_removeuserfromsharepointgroup"] = true;
            sharepointUser["sdds_sharepointuserid"] = sharepointUser.Id;
            relatedEntities.Add(new EntityReference("systemuser", userdata.Id));
            sharepointUser["sdds_userid"] = new EntityReference("systemuser", userdata.Id);

            inputParameter = new ParameterCollection
            {
                { "Target", teammember},
                {"Relationship", relationship },
                 {"RelatedEntities", relatedEntities }
            };

            // Mocking Context 
            var dataCollection = new List<Entity>
            {
               sharepointUser, userdata
            };
            fakecontext.Initialize(dataCollection);
            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.MessageName = "Associate";
            PlugCtx.PrimaryEntityId = sharepointUser.Id;
            PlugCtx.PrimaryEntityName = sharepointUser.LogicalName;
            PlugCtx.InputParameters = inputParameter;
            // Act and Assert
            fakecontext.ExecutePluginWith<OnAssociateSPUser>(PlugCtx);
            var updatedSharePointUser = fakecontext.GetOrganizationService().Retrieve("sdds_sharepointuser", sharepointUser.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_removeuserfromsharepointgroup" }));
            //The remove user from sharepoint must be 'false' as there is no update.
            updatedSharePointUser.GetAttributeValue<bool>("sdds_removeuserfromsharepointgroup").Should().Be(false, "The Remove user from SharePointUser must be false");
        }

        [Fact]
        public void Add_user_to_team_when_user_not_present_in_sharepointuser_table()
        {
            // Arrange

            var teammember = new EntityReference("team", teamIds[0]);
            var userdata = new Entity("systemuser");
            userdata.Id = teamIds[0];
            Relationship relationship = new Relationship("teammembership_association");
            EntityReferenceCollection relatedEntities = new EntityReferenceCollection();
            relatedEntities.Add(new EntityReference("systemuser", userdata.Id));
            inputParameter = new ParameterCollection
            {
                { "Target", teammember},
                {"Relationship", relationship },
                {"RelatedEntities", relatedEntities }
            };

            // Mocking Context 
            var dataCollection = new List<Entity>
            {
               sharepointUser,userdata
            };
            fakecontext.Initialize(dataCollection);
            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.MessageName = "Associate";
            PlugCtx.PrimaryEntityId = sharepointUser.Id;
            PlugCtx.PrimaryEntityName = sharepointUser.LogicalName;
            PlugCtx.InputParameters = inputParameter;
            // Act and Assert
            fakecontext.ExecutePluginWith<OnAssociateSPUser>(PlugCtx);
            var query = new QueryExpression("sdds_sharepointuser");
            query.ColumnSet.AddColumns("sdds_name", "sdds_sharepointuserid", "sdds_userid");
            query.Criteria.AddCondition("sdds_userid", ConditionOperator.Equal, userdata.Id);
            var addedSharepointUser = fakecontext.GetOrganizationService().RetrieveMultiple(query);
            //The ShrePoint User is added with the user reference.
            addedSharepointUser.Entities.Count.Should().Be(1,"The user is added into the sharepoint user table.");
        }

        [Fact]
        public void Remove_user_from_team_when_sharepointuser_exists()
        {
            // Arrange

            var teammember = new EntityReference("team", teamIds[0]);
            var userdata = new Entity("systemuser");
            userdata.Id = teamIds[0];
            Relationship relationship = new Relationship("teammembership_association");
            EntityReferenceCollection relatedEntities = new EntityReferenceCollection();
            sharepointUser["sdds_name"] = "SharePoint User test";
            sharepointUser["sdds_removeuserfromsharepointgroup"] = false;
            sharepointUser["sdds_sharepointuserid"] = sharepointUser.Id;
            relatedEntities.Add(new EntityReference("systemuser", userdata.Id));
            sharepointUser["sdds_userid"] = new EntityReference("systemuser", userdata.Id);

            inputParameter = new ParameterCollection
            {
                { "Target", teammember},
                {"Relationship", relationship },
                 {"RelatedEntities", relatedEntities }
            };

            // Mocking Context 
            var dataCollection = new List<Entity>
            {
               sharepointUser, userdata
            };
            fakecontext.Initialize(dataCollection);
            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.MessageName = "disassociate";
            PlugCtx.PrimaryEntityId = sharepointUser.Id;
            PlugCtx.PrimaryEntityName = sharepointUser.LogicalName;
            PlugCtx.InputParameters = inputParameter;

            //// Act and Assert
            fakecontext.ExecutePluginWith<OnAssociateSPUser>(PlugCtx);
            var updatedSharePointUser = fakecontext.GetOrganizationService().Retrieve("sdds_sharepointuser", sharepointUser.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_removeuserfromsharepointgroup" }));
            //The remove user from sharepoint must be 'true' as there is no update.
            updatedSharePointUser.GetAttributeValue<bool>("sdds_removeuserfromsharepointgroup").Should().Be(true, "The Remove user from SharePointUser must be true");

        }

        [Fact]
        public void Remove_user_from_team_when_sharepointuser_not_exists()
        {
            // Arrange

            var teammember = new EntityReference("team", teamIds[0]);
            var userdata = new Entity("systemuser");
            userdata.Id = teamIds[0];
            Relationship relationship = new Relationship("teammembership_association");
            EntityReferenceCollection relatedEntities = new EntityReferenceCollection();
            sharepointUser["sdds_name"] = "SharePoint User test";
            sharepointUser["sdds_removeuserfromsharepointgroup"] = false;
            relatedEntities.Add(new EntityReference("systemuser", userdata.Id));
           
            inputParameter = new ParameterCollection
            {
                { "Target", teammember},
                {"Relationship", relationship },
                 {"RelatedEntities", relatedEntities }
            };

            // Mocking Context 
            var dataCollection = new List<Entity>
            {
               sharepointUser, userdata
            };
            fakecontext.Initialize(dataCollection);
            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.MessageName = "disassociate";
            PlugCtx.PrimaryEntityId = sharepointUser.Id;
            PlugCtx.PrimaryEntityName = sharepointUser.LogicalName;
            PlugCtx.InputParameters = inputParameter;

            //// Act and Assert
            fakecontext.ExecutePluginWith<OnAssociateSPUser>(PlugCtx);
            var updatedSharePointUser = fakecontext.GetOrganizationService().Retrieve("sdds_sharepointuser", sharepointUser.Id,
                                      new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "sdds_userid" }));
         
            updatedSharePointUser.GetAttributeValue<EntityReference>("sdds_userid").Should().BeNull("The user id reference must be null");

        }


    }
}
