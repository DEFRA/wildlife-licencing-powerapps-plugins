
using FakeItEasy.Sdk;
using FakeXrmEasy;
using FakeXrmEasy.Plugins;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using SDDS.Plugin.GetAddressForPostCode;
using System;
using System.Diagnostics;
using System.IdentityModel.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Xml.Linq;
using Xunit;

namespace SDDS.UnitTests.AddressLookup
{
    public class AddressLookupResponseTests : FakeXrmEasyTestsBase
    {
        [Fact]

        public void ExecuteBusinessLogic()
        {
            // Prepare context

            var fakecontext = this.xrmFakedContext;

             var iTracing = new XrmFakedTracingService();

            // Arrange
             var targetEntityId = new Guid(); ;

            // Define target with expected Attributes


            // Define Input Parameter

            var inputParameter = new ParameterCollection
            {
               
                { "PostCode", "TW34AP" }
            };

            // Mocking Context with your own parameters

            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.Stage = 40;
            PlugCtx.MessageName = "sdds_AddressLookup";

            //PlugCtx.PrimaryEntityName = "none";
            PlugCtx.PrimaryEntityId = targetEntityId;
            PlugCtx.PrimaryEntityName = "none";


            PlugCtx.InputParameters = inputParameter;
            fakecontext.Initialize(new Entity()
            {
                Id = Guid.NewGuid(), LogicalName ="none"

            });


            // Act and Assert

            var fakedPlugin = fakecontext.ExecutePluginWith<GetAddressForPostCode>(PlugCtx);
            iTracing.Trace("tracee"+fakedPlugin);
            Assert.NotNull(fakedPlugin);

           // this.systemtestobj.Invoking(system => system.Execute(null)).Should().Throw<InvalidPluginExecutionException>();

            // fakedPlugin.Should();

        }
    }
}
