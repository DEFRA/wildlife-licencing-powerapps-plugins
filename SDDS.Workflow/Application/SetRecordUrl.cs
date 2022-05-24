using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Workflow.Application
{
    public class SetRecordUrl : CodeActivity
    {
        [RequiredArgument]
        [Input("RecordUrl")]
        public InArgument<string> RecordUrl { get; set; }

        [RequiredArgument]
        [Input("AppId")]
        public InArgument<string> AppId { get; set; }

        [Output("ConcatRecordUrl")]
        public OutArgument<string> ConcatRecordUrl { get; set; }
        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)executionContext.GetExtension<ITracingService>();

            try
            {
                string url = RecordUrl.Get<string>(executionContext);
                string applicationId = AppId.Get<string>(executionContext);
                if (url != null && applicationId != null) ConcatRecordUrl.Set(executionContext, url+applicationId);
            }
            catch (Exception ex)
            {

                throw new InvalidWorkflowException("Failure in custom workflow action: " + ex.Message);
            }
        }
    }
}
