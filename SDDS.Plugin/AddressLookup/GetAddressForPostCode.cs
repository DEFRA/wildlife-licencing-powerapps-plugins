using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SDDS.Plugin.AddressLookup;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SDDS.Plugin.GetAddressForPostCode
{
    public class GetAddressForPostCode : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

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

                //retrieve annotation file
                QueryExpression Notes = new QueryExpression { EntityName = "annotation", ColumnSet = new 
                                            ColumnSet("filename", "subject", "annotationid", "documentbody") };
                // Add link-entity sdds_generalconfiguration
                var generalConfig = Notes.AddLink("sdds_generalconfiguration", "objectid", "sdds_generalconfigurationid", JoinOperator.Inner);
                generalConfig.LinkCriteria.AddCondition("sdds_configurationkey", ConditionOperator.Equal, "SecureCertificateFile");
                //Fetch the certificate file.
                EntityCollection NotesRetrieve = service.RetrieveMultiple(Notes);
                if (NotesRetrieve != null && NotesRetrieve.Entities.Count > 0)

                {
                    //converting document body content to bytes
                    byte[] filecontent = Convert.FromBase64String(NotesRetrieve.Entities[0].Attributes["documentbody"].ToString());
                    X509Certificate2 certificate = new X509Certificate2(filecontent, passphrase);
                    HttpClientHandler handler = new HttpClientHandler();
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ClientCertificates.Add(certificate);

                    var address = GetAddress(handler, url).GetAwaiter().GetResult();
                    context.OutputParameters["Response"] = address; //.results;
                    tracing.Trace(context.OutputParameters["Response"].ToString());
                    
                }

            }
            catch (Exception ex)
            {
                tracing.Trace(ex.Message);
                throw new InvalidPluginExecutionException(ex.Message);

            }
        }

        private async Task<string> GetAddress(HttpClientHandler handler, string url)
        {
            HttpClient httpClient = new HttpClient(handler);

            //Rootobject addressResult = new Rootobject();
            string addressResult = null;
            HttpResponseMessage responseAddress = await httpClient.GetAsync(url);
            if (responseAddress.IsSuccessStatusCode)
            {
                addressResult = await responseAddress.Content.ReadAsStringAsync(); //ReadAsAsync<Rootobject>();
            }

            return addressResult;
        }
    }
    
}
