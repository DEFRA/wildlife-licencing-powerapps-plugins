using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SDDS.Plugin.AddressLookup;
using SDDS.Plugin.Common;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;

namespace SDDS.Plugin.GetAddressForPostCode
{
    public class GetAddressForPostCode : IPlugin
    {
        /*public override void ExecutePluginLogic(LocalPluginContext context)
        {
            var postcodeInputParameter = (string)context.InputParameters["PostCode"];
            context.Trace("input postcode " + postcodeInputParameter);
            var envSchemas = new string[] { "sdds_addresslookupapiurl", "sdds_addresslookupapipassphrase" };
            var environmentVariables = GetEnvironmentVariables(context, envSchemas);

            if (!environmentVariables.TryGetValue(envSchemas[0], out string API_url))
                throw new Exception($"Couldn't read environmentvariable {envSchemas[0]}!");
            if (!environmentVariables.TryGetValue(envSchemas[1], out string API_passphrase))
                throw new Exception($"Couldn't read environmentvariable {envSchemas[1]}!");
            var API_cert = GetEnvironmentVariableSecret(context, "sdds_addresslookupcert")?.ToString();

            if (API_cert == null) throw new Exception($"Couldn't read environmentvariable sdds_addresslookupcert!");
            

            context.Trace("API_cert " + API_cert);
            byte[] filecontent = Convert.FromBase64String(API_cert);
            X509Certificate2 certificate = new X509Certificate2(filecontent, API_passphrase);
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ClientCertificates.Add(certificate);

            var address = GetAddress(handler, API_url).GetAwaiter().GetResult();
            context.OutputParameters["Response"] = address; //.results;
            context.Trace(context.OutputParameters["Response"].ToString());
        }*/

        public void Execute(IServiceProvider serviceProvider)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            try
            {
                var postcodeInputParameter = (string)context.InputParameters["PostCode"];
                tracing.Trace("input postcode " + postcodeInputParameter);
                var fetchConfig =
                    @"<fetch> <entity name='sdds_generalconfiguration'>" +
                    " <attribute name='sdds_generalconfigurationid' />" +
                    " <attribute name='sdds_configurationkey' />" +
                    " <attribute name='sdds_value' />" +
                    " <filter type='and'>" +
                    " <filter type='or'>" +
                    " <condition attribute='sdds_configurationkey' operator='eq' value='Passphrase' />" +
                    " <condition attribute='sdds_configurationkey' operator='eq' value='AddressApiHostUrl' />" +
                    " </filter> </filter>" +
                    "</entity></fetch>";
                var url = string.Empty;
                var passphrase = string.Empty;
                EntityCollection configData = service.RetrieveMultiple(new FetchExpression(fetchConfig));
                tracing.Trace("eNTITY RETRIEVED: " + configData.Entities.Count.ToString());
                if (configData != null && configData.Entities.Count == 2)
                {
                    if (configData.Entities[0].Attributes["sdds_configurationkey"].ToString() == "AddressApiHostUrl")
                        url = configData[0].Attributes["sdds_value"].ToString() + postcodeInputParameter;
                    else
                        passphrase = configData.Entities[0].Attributes["sdds_value"].ToString();
                    if (configData.Entities[1].Attributes["sdds_configurationkey"].ToString() == "AddressApiHostUrl")
                        url = configData.Entities[1].Attributes["sdds_value"].ToString() + postcodeInputParameter;
                    else
                        passphrase = configData.Entities[1].Attributes["sdds_value"].ToString();

                }

                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(passphrase))
                    return;
                tracing.Trace($"Got URL:{url} and Passphrase:{passphrase}");
                //retrieve annotation file
                QueryExpression Notes = new QueryExpression
                {
                    EntityName = "annotation",
                    ColumnSet = new
                                            ColumnSet("filename", "subject", "annotationid", "documentbody")
                };
                // Add link-entity sdds_generalconfiguration
                var generalConfig = Notes.AddLink("sdds_generalconfiguration", "objectid", "sdds_generalconfigurationid", JoinOperator.Inner);
                generalConfig.LinkCriteria.AddCondition("sdds_configurationkey", ConditionOperator.Equal, "SecureCertificateFile");
                //Fetch the certificate file.
                EntityCollection NotesRetrieve = service.RetrieveMultiple(Notes);
                if (NotesRetrieve != null && NotesRetrieve.Entities.Count > 0)
                {
                    tracing.Trace("nOTE RETREIVED");
                    //converting document body content to bytes
                    byte[] filecontent = Convert.FromBase64String(NotesRetrieve.Entities[0].Attributes["documentbody"].ToString());
                    X509Certificate2 certificate = new X509Certificate2(filecontent, passphrase);

                    
                    tracing.Trace("cERT RETRIEVED");

                    HttpClientHandler handler = new HttpClientHandler();
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ClientCertificates.Add(certificate);

                    tracing.Trace("cERT PASSED");

                    var address = GetAddress(handler, url, tracing).GetAwaiter().GetResult();
                    tracing.Trace(address.ToString());
                    context.OutputParameters["Response"] = address; //.results;
                    tracing.Trace(context.OutputParameters["Response"].ToString());

                }

            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                throw new InvalidPluginExecutionException(ex.Message, ex);

            }
        }

        private async Task<string> GetAddress(HttpClientHandler handler, string url, ITracingService tracing)
        {
            
            HttpClient httpClient = new HttpClient(handler);
          
            Rootobject address = new Rootobject();
            string addressResult = null;
            try
            {
                tracing.Trace("Getting address");

                tracing.Trace("URL: "+url);
                HttpResponseMessage responseAddress = await httpClient.GetAsync(url);
                
                responseAddress.EnsureSuccessStatusCode();
                tracing.Trace("rESULT RETRIEVED SUCCESSFULLY");
                addressResult = await responseAddress.Content.ReadAsStringAsync(); //ReadAsAsync<Rootobject>();
                
               
            }
            catch (HttpRequestException ex)
            {
                tracing.Trace(ex.Message);
            }
           
            handler.Dispose();
            httpClient.Dispose();

            return addressResult;
        }
    }

}
