using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Penman.Html2OpenXml;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HTMLToWordConverter
{
    public static class ConvertHTMLToWord
    {
        [FunctionName("ConvertHTMLToWord")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["filename"];

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                using (MemoryStream generatedDocument = new MemoryStream())
                {
                    using (WordprocessingDocument package = WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                    {
                        MainDocumentPart mainPart = package.MainDocumentPart;
                        if (mainPart == null)
                        {
                            mainPart = package.AddMainDocumentPart();
                            new Document(new Body()).Save(mainPart);
                        }

                        HtmlConverter converter = new HtmlConverter(mainPart);
                        converter.ParseHtml(requestBody);
                        log.LogInformation("HTML Converted!");
                        mainPart.Document.Save();
                        log.LogInformation("HTML Saved!");
                    }
                    return new FileContentResult(generatedDocument.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    {
                        FileDownloadName = name
                    };
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
