using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities.Expressions;

namespace SDDS.Workflow.Site
{
    public class ConvertGridReference : CodeActivity
    {
        [RequiredArgument]
        [Input("GridRef")]
        public InArgument<string> GridReference { get; set; }

        [Output("Easting")]
        [Default("0")]
        public OutArgument<int> Easting { get; set; }

        [Output("Northing")]
        [Default("0")]
        public OutArgument<int> Northing { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            IWorkflowContext wfContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(wfContext.UserId);
            ITracingService tracingService = (ITracingService)context.GetExtension<ITracingService>();

            tracingService.Trace("Entering GridRef to EN conversion");
            try
            {
                var gridRef = GridReference.Get<string>(context);
                var bng = BuildNationalGrid().FirstOrDefault(x => x.HMS == gridRef.Substring(0, 2));

                if (bng != null)
                {
                    var eastnorth = gridRef.Substring(2);
                    var half = eastnorth.Length / 2;
                    var east = eastnorth.Substring(0, half);
                    var north = eastnorth.Substring(half);
                    Easting.Set(context, int.Parse((bng.X + east).PadRight(6, '0')));
                    Northing.Set(context, int.Parse((bng.Y + north).PadRight(6, '0')));
                }
                else
                {
                    Easting.Set(context, 0);
                    Northing.Set(context, 0);
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace($"{ex.Message}: {ex.StackTrace}");
                ExceptionHandler.SaveToTable(service, ex, wfContext.MessageName, this.GetType().Name);
                throw ex;
            }
        }

        private List<BNG> BuildNationalGrid()
        {
            return new List<BNG>
            {
                new BNG("SV", 1, 1),new BNG("SW", 2, 1),new BNG("SX", 3, 1),new BNG("SY", 4, 1),new BNG("SZ", 5, 1),new BNG("TV", 6, 1),
                new BNG("SR", 2, 2),new BNG("SS", 3, 2),new BNG("ST", 4, 2),new BNG("SU", 5, 2),new BNG("TQ", 6, 2),new BNG("TR", 7, 2),
                new BNG("SM", 2, 3),new BNG("SN", 3, 3),new BNG("SO", 4, 3),new BNG("SP", 5, 3),new BNG("TL", 6, 3),new BNG("TM", 7, 3),
                new BNG("SH", 3, 4),new BNG("SJ", 4, 4),new BNG("SK", 5, 4),new BNG("TF", 6, 4),new BNG("TG", 7, 4),
                new BNG("SC", 3, 5),new BNG("SD", 4, 5),new BNG("SE", 5, 5),new BNG("TA", 6, 5),
                new BNG("NW", 2, 6),new BNG("NX", 3, 6),new BNG("NY", 4, 6),new BNG("NZ", 5, 6),
                new BNG("NR", 2, 7),new BNG("NS", 3, 7),new BNG("NT", 4, 7),new BNG("NU", 5, 7),
                new BNG("NL", 1, 8),new BNG("NM", 2, 8),new BNG("NN", 3, 8),new BNG("NO", 4, 8),
                new BNG("NF", 1, 9),new BNG("NG", 2, 9),new BNG("NH", 3, 9),new BNG("NJ", 4, 9),new BNG("NK", 5, 9),
                new BNG("NA", 1, 10),new BNG("NB", 2, 10),new BNG("NC", 3, 10),new BNG("ND", 4, 10),
                new BNG("HW", 2, 11),new BNG("HX", 3, 11),new BNG("HY", 4, 11),new BNG("HZ", 5, 11),
                new BNG("HT", 4, 12),new BNG("HU", 5, 12),
                new BNG("HP", 5, 13)
            };
        }

        private class BNG
        {
            public BNG(string hMS, int x, int y)
            {
                HMS = hMS;
                X = x;
                Y = y;
            }

            public string HMS { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
