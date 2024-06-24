using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using SDDS.Plugin.Model;
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
                if ((appType.LogicalName == "sdds_membership") || (appType.LogicalName == "sdds_qualification") || (appType.LogicalName == "sdds_licensemethod"))
                {
                    tracing.Trace("Entity name:" + appType.LogicalName);
                    if ((appType.Contains("sdds_name") || (appType.Contains("sdds_methodname")) && context.MessageName.ToLower() == "create"))
                    {
                        string name = appType.LogicalName == "sdds_licensemethod" ? "sdds_methodname" : "sdds_name";
                        string typeName = appType.GetAttributeValue<string>(name);
                        string choiceName = GetEntityLogicalName(appType.LogicalName); 
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
                            ExceptionHandler.SaveToTable(service, ex, context.MessageName, this.GetType().Name, (int)ErrorPriority.Medium);
                            throw new InvalidPluginExecutionException(ex.Message);
                        }

                    }
                    else if ((appType.Contains("sdds_name") || appType.Contains("sdds_methodname")) && context.MessageName.ToLower() == "update")
                    {
                        tracing.Trace("entering update");
                        Entity preImageEntity = (Entity)context.PreEntityImages["Image"];

                        string name = appType.LogicalName == "sdds_licensemethod" ? "sdds_methodname" : "sdds_name";
                        string typeName = appType.GetAttributeValue<string>(name);

                        tracing.Trace("entering entity name :" + typeName);
                        int value = preImageEntity.GetAttributeValue<int>("sdds_choicevalue");

                        tracing.Trace("entering optionsetValue:" + value.ToString());
                        string choiceName = GetEntityLogicalName(appType.LogicalName); 

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
                            ExceptionHandler.SaveToTable(service, ex, context.MessageName, this.GetType().Name, (int)ErrorPriority.Medium);
                            throw new InvalidPluginExecutionException(ex.Message);
                        }
                    }
                }
            }
            else if (context.InputParameters["Target"] is EntityReference appType2 && context.MessageName.ToLower() == "delete")
            {
                Entity preImageEntity = (Entity)context.PreEntityImages["Image"];

                tracing.Trace("entering optionsetValue:" + preImageEntity.LogicalName);

                string choiceName = GetEntityLogicalName(appType2.LogicalName); 
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
                    ExceptionHandler.SaveToTable(service, ex, context.MessageName, this.GetType().Name, (int)ErrorPriority.Medium);
                    throw new InvalidPluginExecutionException(ex.Message);
                }
            }
        }

        private string GetEntityLogicalName(string logicalName)
        {
            string entity;
            switch (logicalName)
            {
                case "sdds_membership":
                    entity = "sdds_membership";
                    break;
                case "sdds_qualification":
                    entity = "sdds_qualification";
                    break;
                default:
                    entity = "sdds_licensemethod";
                    break;
            }

            return entity;
        }
    }
}
