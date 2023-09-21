
namespace SDDS.UnitTests.ApplicationTypes
{
    using FakeXrmEasy.Abstractions;
    using FakeXrmEasy.Plugins;
    using FluentAssertions;
   // using global::SDDS.Plugin.ApplicationTypes;
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Collections.Generic;
    using Xunit;
    public class ApplicationTypeTests : FakeXrmEasyTestsBase
    {
        readonly IXrmFakedContext fakecontext;
        readonly Entity applicationType;
        ParameterCollection inputParameter;

        public ApplicationTypeTests()
        {

            fakecontext = this.xrmFakedContext;
            applicationType = new Entity("sdds_applicationtypes")
            {
                Id = Guid.NewGuid(),
                LogicalName = "sdds_applicationtypes"

            };

        }
        [Fact]
        public void InsertOptionSetValue_on_applicationType_create()
        {
            // Arrange
            applicationType["sdds_applicationname"] = "Application type name from Unit test";
            inputParameter = new ParameterCollection
            {
                { "Target", applicationType }
            };
            var dataCollection = new List<Entity>
            {
               applicationType
            };

            fakecontext.Initialize(dataCollection);
            // Mocking Context 
            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.Stage = 40;
            PlugCtx.MessageName = "Create";
            PlugCtx.PrimaryEntityId = applicationType.Id;
            PlugCtx.PrimaryEntityName = "sdds_applicationtypes";
            PlugCtx.InputParameters = inputParameter;
            // Act and Assert
           var plugin = fakecontext. ExecutePluginWith(PlugCtx,null);
           plugin.Should().NotBeNull();
        }
    }
}
