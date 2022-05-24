using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.MemberShip
{
    public class CreateMemberShipChoice : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));


            if (context.Depth > 1) return;

            if (context.InputParameters["Target"] is Entity appType)
            {
                tracing.Trace("entering plugin:" + appType.LogicalName);
                if ((appType.LogicalName == "sdds_membership") || (appType.LogicalName == "sdds_qualification"))
                {
                    tracing.Trace("Entity name:" + appType.LogicalName);
                    if (appType.Contains("sdds_name") && context.MessageName.ToLower() == "create")
                    {
                        string typeName = appType.GetAttributeValue<string>("sdds_name");
                        string choiceName = appType.LogicalName == "sdds_membership" ? "sdds_membership" : "sdds_qualifications";
                        tracing.Trace("entering ChoiceName:" + choiceName);
                        InsertOptionValueRequest insertOptionValueRequest =
                        new InsertOptionValueRequest
                        {
                            OptionSetName = choiceName,
                            Label = new Label(typeName, 1033)
                        };

                        try
                        {
                            // Execute the request and store the newly inserted option value
                            // for cleanup, used in the later part of this sample.
                            var _insertedOptionValue = ((InsertOptionValueResponse)service.Execute(
                            insertOptionValueRequest)).NewOptionValue;
                            tracing.Trace(_insertedOptionValue.ToString());

                            //Publish the OptionSet
                            PublishXmlRequest pxReq2 = new PublishXmlRequest { ParameterXml = String.Format("<importexportxml><optionsets><optionset>{0}</optionset></optionsets></importexportxml>", choiceName) };
                            service.Execute(pxReq2);

                            service.Update(new Entity(appType.LogicalName, appType.Id)
                            {
                                ["sdds_choicevalue"] = _insertedOptionValue
                            });
                        }
                        catch (Exception ex)
                        {
                            tracing.Trace(ex.Message);
                            throw new InvalidPluginExecutionException(ex.Message);
                        }

                    }
                    else if (appType.Contains("sdds_name") && context.MessageName.ToLower() == "update")
                    {
                        tracing.Trace("entering update");
                        Entity preImageEntity = (Entity)context.PreEntityImages["Image"];

                        string typeName = appType.GetAttributeValue<string>("sdds_name");

                        tracing.Trace("entering entity name :" + typeName);
                        int value = preImageEntity.GetAttributeValue<int>("sdds_choicevalue");

                        tracing.Trace("entering optionsetValue:" + value.ToString());
                        string choiceName = appType.LogicalName == "sdds_membership" ? "sdds_membership" : "sdds_qualifications";

                        tracing.Trace("entering ChoiceName:" + choiceName);
                        try
                        {

                            UpdateOptionValueRequest updateOptionValueRequest =
                                new UpdateOptionValueRequest
                                {
                                    OptionSetName = choiceName,
                                    // Update the second option value.
                                    Value = value,
                                    Label = new Label(typeName, 1033)
                                };

                            service.Execute(updateOptionValueRequest);

                            //Publish the OptionSet
                            PublishXmlRequest pxReq3 = new PublishXmlRequest { ParameterXml = String.Format("<importexportxml><optionsets><optionset>{0}</optionset></optionsets></importexportxml>", choiceName) };
                            service.Execute(pxReq3);
                        }
                        catch (Exception ex)
                        {
                            tracing.Trace(ex.Message);
                            throw new InvalidPluginExecutionException(ex.Message);
                        }
                    }
                }
            }
            else if (context.InputParameters["Target"] is EntityReference appType2 && context.MessageName.ToLower() == "delete")
            {
                Entity preImageEntity = (Entity)context.PreEntityImages["Image"];

                tracing.Trace("entering optionsetValue:" + preImageEntity.LogicalName);

                string choiceName = appType2.LogicalName == "sdds_membership" ? "sdds_membership" : "sdds_qualifications";
                int value = preImageEntity.GetAttributeValue<int>("sdds_choicevalue");
                tracing.Trace("entering optionsetValue:" + value.ToString());
                // Use the DeleteOptionValueRequest message 
                // to remove the newly inserted label.
                DeleteOptionValueRequest deleteOptionValueRequest =
                    new DeleteOptionValueRequest
                    {
                        OptionSetName = choiceName,
                        Value = value
                    };

                try
                {
                    // Execute the request.
                    service.Execute(deleteOptionValueRequest);
                }
                catch (Exception ex)
                {
                    tracing.Trace(ex.Message);
                    throw new InvalidPluginExecutionException(ex.Message);
                }
            }
        }
    }
}
