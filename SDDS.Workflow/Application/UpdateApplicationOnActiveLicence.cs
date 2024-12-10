using Microsoft.Xrm.Sdk.Query;
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
    public  class UpdateApplicationOnActiveLicence: CodeActivity
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


            tracingService.Trace("Entering Checking Active licence");
            try
            {
                EntityReference application = ApplicationRef.Get<EntityReference>(executionContext);
                if (application == null || application.LogicalName != "sdds_application") return;

                QueryExpression licences = new QueryExpression("sdds_license");
                licences.ColumnSet.AddColumn("sdds_licenseid");
                licences.Criteria = new FilterExpression();
                //licences.Criteria.AddCondition("statuscode", ConditionOperator.Equal, 1);
                licences.Criteria.AddCondition("sdds_applicationid", ConditionOperator.Equal, application.Id);
                licences.Criteria = new FilterExpression(LogicalOperator.Or)
                {
                    Conditions = {
                         new ConditionExpression(
                            attributeName:"statuscode",
                            conditionOperator: ConditionOperator.Equal,
                            value: 452120002),
                         new ConditionExpression(
                            attributeName:"statuscode",
                            conditionOperator: ConditionOperator.Equal,
                            value: 1)
                    }
                };
                
                EntityCollection EC = service.RetrieveMultiple(licences);
                if (EC.Entities.Count > 0) Result.Set(executionContext, true); else Result.Set(executionContext, false);
            }
            catch (Exception ex)
            {
                tracingService.Trace("Failure in custom workflow action: {0}", ex.Message);
                ExceptionHandler.SaveToTable(service, ex, context.MessageName, "UpdateApplicationOnActiveLicence", (int)ErrorPriority.Medium);

                throw new InvalidWorkflowException("Failure in custom workflow action: " + ex.Message);
            }


        }
    }
}
