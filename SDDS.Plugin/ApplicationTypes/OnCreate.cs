using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace SDDS.Plugin.ApplicationTypes
{
    //public class OnCreate : IPlugin
    //{
    //    public void Execute(IServiceProvider serviceProvider)
    //    {
    //        IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
    //        IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
    //        IOrganizationService service = factory.CreateOrganizationService(context.UserId);
    //        ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

    //        if (context.Depth > 1) return;
    //        Entity appType = (Entity)context.InputParameters["Target"];


    //        if (appType.Contains("sdds_applicationname") && context.MessageName.ToLower() == "create")
    //        {
    //            string typeName = appType.GetAttributeValue<string>("sdds_applicationname");
    //            InsertOptionValueRequest insertOptionValueRequest =
    //            new InsertOptionValueRequest
    //            {
    //                OptionSetName = "sdds_appicationtypes",
    //                Label = new Label(typeName, 1033)
    //            };

    //            try
    //            {
    //                // Execute the request and store the newly inserted option value
    //                // for cleanup, used in the later part of this sample.
    //                var _insertedOptionValue = ((InsertOptionValueResponse)service.Execute(
    //                insertOptionValueRequest)).NewOptionValue;
    //                tracing.Trace(_insertedOptionValue.ToString());

    //                //Publish the OptionSet
    //                PublishXmlRequest pxReq2 = new PublishXmlRequest { ParameterXml = String.Format("<importexportxml><optionsets><optionset>{0}</optionset></optionsets></importexportxml>", "sdds_appicationtypes") };
    //                service.Execute(pxReq2);

    //                service.Update(new Entity(appType.LogicalName, appType.Id)
    //                {
    //                    ["sdds_choicevalue"] = _insertedOptionValue
    //                });
    //            }
    //            catch (Exception ex)
    //            {
    //                tracing.Trace(ex.Message);
    //                throw new InvalidPluginExecutionException(ex.Message);
    //            }

    //        }
    //    }
    //}
}
