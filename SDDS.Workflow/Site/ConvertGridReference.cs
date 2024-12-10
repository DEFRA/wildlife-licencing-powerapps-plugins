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
                var gridRef = GridReference.Get<string>(context).Replace(" ", "").Trim();
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
                ExceptionHandler.SaveToTable(service, ex, wfContext.MessageName, this.GetType().Name, (int)ErrorPriority.Medium);
                throw ex;
            }
        }

        private List<BNG> BuildNationalGrid()
        {
            return new List<BNG>
            {
                new BNG("SV", 0, 0),new BNG("SW", 1, 0),new BNG("SX", 2, 0),new BNG("SY", 3, 0),new BNG("SZ", 4, 0),new BNG("TV", 5, 0),
                new BNG("SR", 1, 1),new BNG("SS", 2, 1),new BNG("ST", 3, 1),new BNG("SU", 4, 1),new BNG("TQ", 5, 1),new BNG("TR", 6, 1),
                new BNG("SM", 1, 2),new BNG("SN", 2, 2),new BNG("SO", 3, 2),new BNG("SP", 4, 2),new BNG("TL", 5, 2),new BNG("TM", 6, 2),
                new BNG("SH", 2, 3),new BNG("SJ", 3, 3),new BNG("SK", 4, 3),new BNG("TF", 5, 3),new BNG("TG", 6, 3),
                new BNG("SC", 2, 4),new BNG("SD", 3, 4),new BNG("SE", 4, 4),new BNG("TA", 5, 4),
                new BNG("NW", 1, 5),new BNG("NX", 2, 5),new BNG("NY", 3, 5),new BNG("NZ", 4, 5),
                new BNG("NR", 1, 6),new BNG("NS", 2, 6),new BNG("NT", 3, 6),new BNG("NU", 4, 6),
                new BNG("NL", 0, 7),new BNG("NM", 1, 7),new BNG("NN", 2, 7),new BNG("NO", 3, 7),
                new BNG("NF", 0, 8),new BNG("NG", 1, 8),new BNG("NH", 2, 8),new BNG("NJ", 3, 8),new BNG("NK", 4, 8),
                new BNG("NA", 0, 9),new BNG("NB", 1, 9),new BNG("NC", 2, 9),new BNG("ND", 3, 9),
                new BNG("HW", 1, 10),new BNG("HX", 2, 10),new BNG("HY", 3, 10),new BNG("HZ", 4, 10),
                new BNG("HT", 3, 11),new BNG("HU", 4, 11),
                new BNG("HP", 4, 12)
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
