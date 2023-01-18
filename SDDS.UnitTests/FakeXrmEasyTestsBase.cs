
namespace SDDS.UnitTests
{
    using System.Diagnostics.CodeAnalysis;
    using FakeXrmEasy.Abstractions;
    using FakeXrmEasy.Abstractions.Enums;
    using FakeXrmEasy.Middleware;
    using FakeXrmEasy.Middleware.Crud;
    using FakeXrmEasy.Middleware.Messages;
    public class FakeXrmEasyTestsBase
    {
        protected readonly IXrmFakedContext xrmFakedContext;

        public FakeXrmEasyTestsBase()
        {
            this.xrmFakedContext = MiddlewareBuilder
            .New()
            .AddCrud()
            .UseCrud()
            .UseMessages()
            .SetLicense(FakeXrmEasyLicense.RPL_1_5)
            .Build();
        }
    }
}
