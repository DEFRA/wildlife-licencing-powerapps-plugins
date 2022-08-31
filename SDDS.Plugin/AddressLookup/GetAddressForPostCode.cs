using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using SDDS.Plugin.AddressLookup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;

using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Caching;

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

            var postcodeInputParameter = (string)context.InputParameters["PostCode"];


            var url = "https://integration-snd.azure.defra.cloud/ws/rest/DEFRA/v1/address/postcodes?postcode="+ postcodeInputParameter;
            tracing.Trace("input "+postcodeInputParameter);
            //retrieve annotation file
            QueryExpression Notes = new QueryExpression { EntityName = "annotation", ColumnSet = new ColumnSet("filename", "subject", "annotationid", "documentbody") };
            Notes.Criteria.AddCondition("filename", ConditionOperator.Equal, "BOOMI-SDDS-SND.pfx");
            EntityCollection NotesRetrieve = service.RetrieveMultiple(Notes);
            tracing.Trace("before retrieve");
            if (NotesRetrieve != null && NotesRetrieve.Entities.Count > 0)
            {

                //converting document body content to bytes
                byte[] filecontent = Convert.FromBase64String(NotesRetrieve.Entities[0].Attributes["documentbody"].ToString());
                X509Certificate2 certificate = new X509Certificate2(filecontent, "r!45PZ059ZCB");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ClientCertificates.Add(certificate);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusDescription == "OK")
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    //Remove the unwanted response data before passing to the output.
                    int pos = responseFromServer.IndexOf("\"results");
                    if (pos >= 0)
                    {
                        responseFromServer = responseFromServer.Remove(0, pos);
                        pos = responseFromServer.IndexOf("_info");
                        if (pos >= 0)
                            responseFromServer = responseFromServer.Remove(pos);
                        responseFromServer = responseFromServer.Insert(0, "{");
                        responseFromServer = responseFromServer.Remove(responseFromServer.LastIndexOf(','));
                        responseFromServer = responseFromServer + "}";
                    }
                        //string tst = "{\"header\":{ \"offset\":\"0\", \"totalresults\":\"21\",\"format\":\"JSON\",\"dataset\":\"DPA\", \"lr\":\"EN\", \"maxresults\":\"100\", \"matching_totalresults\":\"21\" }}";

                        //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(header));
                        //MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(tst));
                        //header dataObject = jsonSerializer.ReadObject(ms) as header;

                        //  tracing.Trace(dataObject.Address.AddressLine);
                    context.OutputParameters["Response"] = responseFromServer;
                    tracing.Trace(context.OutputParameters["Response"].ToString());

                }

            }


        }
    }
}
