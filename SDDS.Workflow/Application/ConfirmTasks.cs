using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Workflow.Application
{
    public class ConfirmTasks : CodeActivity
    {
        [RequiredArgument]
        [Input("ApplicationRef")]
        [ReferenceTarget("sdds_application")]
        public InArgument<EntityReference> ApplicationRef { get; set; }

        [Output("Result")]
        [Default("False")]
        public OutArgument<bool> Result { get; set; }

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

                QueryExpression tasks = new QueryExpression("task");
                tasks.ColumnSet = new ColumnSet(true);
                tasks.Criteria = new FilterExpression();
                tasks.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                tasks.Criteria.AddCondition("regardingobjectid", ConditionOperator.Equal, application.Id);

                EntityCollection EC = service.RetrieveMultiple(tasks);
                if (EC.Entities.Count > 0) Result.Set(executionContext, false); else Result.Set(executionContext, true);
            }
            catch (Exception ex)
            {
                tracingService.Trace("Failure in custom workflow action: {0}", ex.Message);
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, this.GetType().Name, (int)ErrorPriority.Medium);

                throw new InvalidWorkflowException("Failure in custom workflow action: " + ex.Message);
            }
        }
       
    }
}
