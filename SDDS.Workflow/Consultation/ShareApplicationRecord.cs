using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace SDDS.Workflow.Consultation
{
    public class ShareApplicationRecord : CodeActivity
    {
        [RequiredArgument]
        [Input("ApplicationRef")]
        [ReferenceTarget("sdds_application")]
        public InArgument<EntityReference> ApplicationRef { get; set; }

        [RequiredArgument]
        [Input("TeamRef")]
        [ReferenceTarget("team")]
        public InArgument<EntityReference> TeamRef { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)executionContext.GetExtension<ITracingService>();


            tracingService.Trace("Entering Checking task workflow");
            try
            {
                EntityReference application = ApplicationRef.Get<EntityReference>(executionContext);
                if (application == null || application.LogicalName != "sdds_application") return;

                EntityReference team = TeamRef.Get<EntityReference>(executionContext);
                if (team == null || team.LogicalName != "team") return;

                // Grant the team read/write access to the lead.
                var grantAccessRequest = new GrantAccessRequest
                {
                    PrincipalAccess = new PrincipalAccess
                    {
                        AccessMask = AccessRights.ReadAccess | AccessRights.WriteAccess,
                        Principal = team
                    },
                    Target = application
                };

                service.Execute(grantAccessRequest);

            }
            catch (Exception ex)
            {
                tracingService.Trace("Failure in custom workflow action: {0}", ex.Message);

                throw new InvalidWorkflowException("Failure in custom workflow action: " + ex.Message);
            }
        }
    }
}
