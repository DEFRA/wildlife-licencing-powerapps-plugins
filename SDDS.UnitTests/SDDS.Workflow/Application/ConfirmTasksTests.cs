using FakeXrmEasy.Abstractions;
using FakeXrmEasy.CodeActivities;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using SDDS.Workflow.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SDDS.UnitTests.SDDS.Workflow.Application
{
    public class ConfirmTasksTests : FakeXrmEasyTestsBase
    {
        readonly IXrmFakedContext fakecontext;
        private Entity application;
        public ConfirmTasksTests()
        {
            fakecontext = this.xrmFakedContext;
            application = new Entity("sdds_application")
            {
                Id = Guid.NewGuid()
            };
        }


        [Fact]
        public void Confirm_tasks_test()
        {
            //Inputs
            var inputs = new Dictionary<string, object>() {
                          { "ApplicationRef", new EntityReference("sdds_application",application.Id ) }
                        };
            var result = fakecontext.ExecuteCodeActivity<ConfirmTasks>(inputs);

            result["Result"].Should().NotBeNull();
        }
    }
}
