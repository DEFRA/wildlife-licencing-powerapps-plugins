using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Plugins;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using SDDS.Plugin.GetAddressForPostCode;
using System;
using System.Collections.Generic;
using Xunit;

namespace SDDS.UnitTests.AddressLookup
{
    public class AddressLookupResponseTests : FakeXrmEasyTestsBase
    {
        readonly IXrmFakedContext fakecontext;

        public AddressLookupResponseTests()
        {
            fakecontext = this.xrmFakedContext;


        }
        [Fact]

        public void ExecuteBusinessLogic_get_address_by_postcode()
        {
            // Prepare context

            var inputParameter = new ParameterCollection
            {

                { "PostCode", "TW34AP" }
            };

            var generalconfiguration = new Entity("sdds_generalconfiguration");
            generalconfiguration.Id = Guid.NewGuid();
            generalconfiguration["sdds_configurationkey"] = "Passphrase";
            generalconfiguration["sdds_value"] = "passpharase@1234";

            EntityCollection configdata = new EntityCollection();
            configdata.Entities.Add(generalconfiguration);
            generalconfiguration = new Entity("sdds_generalconfiguration");
            generalconfiguration.Id = Guid.NewGuid();
            generalconfiguration["sdds_configurationkey"] = "AddressApiHostUrl";
            generalconfiguration["sdds_value"] = "https://getaddressbyposrcode.org";
            configdata.Entities.Add(generalconfiguration);

            var generalconfigForSecureFile = new Entity("sdds_generalconfiguration");
            generalconfigForSecureFile.Id = Guid.NewGuid();
            generalconfigForSecureFile["sdds_configurationkey"] = "SecureCertificateFile1";

            var notes = new Entity("annotation");
            notes.Id = Guid.NewGuid();
            notes["filename"] = "TestFile.pfx";
            notes["subject"] = "The certificate file for Address Lookup API";
            notes["annotationid"] = notes.Id;
            notes["documentbody"] = "Qk5HIDogU2Vzc2lvbiB3aXRoIFNhdHlhCgotIDMgYXBwLCBsYW5kIG93bmVyIHBvcnRhbCwgZGV2ZWxvcGVyIHBvcnRhbAotIHNwYXJrbGUgdG8gZGVwbG95IGx1Z2luLCB4cm1kZWZpbmF0ZWx5IGZvciBzY3JpcHQgZWFybHkgYm91bmQ=";
            notes["objectid"] = new EntityReference("sdds_generalconfiguration", generalconfigForSecureFile.Id);

            // Mocking Context with your own parameters

            var PlugCtx = fakecontext.GetDefaultPluginContext();
            PlugCtx.Stage = 40;
            PlugCtx.MessageName = "sdds_AddressLookup";

            //PlugCtx.PrimaryEntityName = "none";
            PlugCtx.PrimaryEntityName = "none";
            var dataCollection = new List<Entity>
            {
               configdata[0], configdata[1], generalconfigForSecureFile, notes
            };
            PlugCtx.InputParameters = inputParameter;
            fakecontext.Initialize(dataCollection);

            // Act and Assert
            var pluginres = fakecontext.ExecutePluginWith<GetAddressForPostCode>(PlugCtx);

            pluginres.Should().NotBeNull();




        }
    }
}
