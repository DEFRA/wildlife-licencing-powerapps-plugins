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
    public class GetCurrentUserTests : FakeXrmEasyTestsBase
    {
        readonly IXrmFakedContext fakecontext;
        public GetCurrentUserTests()
        {
            fakecontext = this.xrmFakedContext;

        }


        [Fact]
        public void Get_current_user_test()
        {
            var result = fakecontext.ExecuteCodeActivity<GetCurrentUser>(fakecontext.GetDefaultWorkflowContext());

            if (result != null)
            {
                result["CurrentUser"].Should().NotBeNull();
            }

        }

    }
}
