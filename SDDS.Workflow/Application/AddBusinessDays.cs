using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Workflow.Application
{
    public class AddBusinessDays : CodeActivity
    {
        [RequiredArgument]
        [Input("StartDate")]
        public InArgument<DateTime> StartDate { get; set; }

        [RequiredArgument]
        [Input("Days")]
        public InArgument<int> Days { get; set; }

        [Output("EndDate")]
        public OutArgument<DateTime> EndDate { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var startDate = StartDate.Get<DateTime>(context);
            var days = StartDate.Get<int>(context);
            EndDate.Set(context, AddDays(startDate, days));
        }

        private DateTime AddDays(DateTime current, int days)
        {
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                } while (current.DayOfWeek == DayOfWeek.Saturday ||
                         current.DayOfWeek == DayOfWeek.Sunday);
            }
            return current;
        }
    }
}
