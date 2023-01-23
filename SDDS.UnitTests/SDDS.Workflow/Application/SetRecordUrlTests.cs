using FakeXrmEasy.Abstractions;
using FakeXrmEasy.CodeActivities;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using SDDS.Workflow.Application;
using System;
using System.Collections.Generic;
using Xunit;

namespace SDDS.UnitTests.SDDS.Workflow.Application
{
    public class SetRecordUrlTests : FakeXrmEasyTestsBase
    {
        readonly IXrmFakedContext fakecontext;
        private Entity application;
        public SetRecordUrlTests()
        {
            fakecontext = this.xrmFakedContext;
            application = new Entity("sdds_application")
            {
                Id = Guid.NewGuid()
            };
        }


        [Fact]
        public void Set_record_url_test()
        {
            //Inputs
            var url = new Uri("https://defra-sdds-dev2.crm11.dynamics.com/main.aspx?appid=63c4b352-eb39-ed11-9db0-0022481b53bf&forceUCI=1&pagetype=entityrecord&etn=sdds_application&id=fc588306-cf90-ec11-b400-000d3a872ae7");
            // var url = new Uri("https://defra-sdds-dev2.crm11.dynamics.com");
            var inputs = new Dictionary<string, object>() {
                          { "RecordUrl",url.ToString() },
                          { "AppId", application.Id.ToString()  }

            };
            var result = fakecontext.ExecuteCodeActivity<SetRecordUrl>(inputs);

            result["ConcatRecordUrl"].Should().NotBeNull();
        }
    }
}
