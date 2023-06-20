using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Setupuk.gov.defra.sdds.automateduitests.Helper;
using System.Security;
using uk.gov.defra.sdds.automateduitests.Setup;

namespace uk.gov.defra.sdds.automateduitests.Applications
{
    [TestClass]
    public class ApplicationCreation
    {
        private static IConfiguration _config = InitConfiguration();
        private readonly SecureString _username = _config["d365CaseworkerUsername"].ToSecureString();
        private readonly SecureString _password = _config["d365CaseworkerPassword"].ToSecureString();
        private readonly SecureString _mfaSecretKey = string.Empty.ToSecureString();
        private readonly Uri _xrmUri = new Uri(_config["d365URL"]);

        [TestMethod]
        public void UCITestCreateA24BadgerApplicationHasWLMSPMSuffixNPriority2()
        {
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password);

                xrmApp.Navigation.OpenApp(UCIAppName.LicenceApp);

                xrmApp.Navigation.OpenSubArea("Licence", "Applications");
                //string applicationNo, priority;
                var result = CreateNewApplication(client, xrmApp);//, out applicationNo, out priority);

                xrmApp.ThinkTime(3000);
                Assert.IsTrue(result.ApplicationNo.EndsWith("WLM-SPM"));
                Assert.AreEqual(result.Priority, "2");

            }
        }

        private static dynamic CreateNewApplication(WebClient client, XrmApp xrmApp)//, out string applicationNo, out string priority)
        {
            xrmApp.CommandBar.ClickCommand("New");

            //XrmApp.Entity.SelectLookup();
            xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationtypesid", Value = "A24 Badger", Index = 0 });
            xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicantid", Value = "AbigailPouros", Index = 0 });
            xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Development", Index = 0 });
            xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_applicationcategory", Value = "Commercial" });
            xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_issitesameasapplicants", Value = true });


            xrmApp.Entity.Save();
            xrmApp.ThinkTime(3000);
            client.Browser.Driver.Navigate().Refresh();
            xrmApp.ThinkTime(3000);
            var applicationNo = xrmApp.Entity.GetValue("sdds_applicationnumber");
            var priority = xrmApp.Entity.GetValue(new OptionSet { Name = "sdds_priority" });
            while (string.IsNullOrWhiteSpace(applicationNo) || applicationNo.Equals("---") || string.IsNullOrWhiteSpace(priority))
            {
                xrmApp.ThinkTime(10000);
                client.Browser.Driver.Navigate().Refresh();
                applicationNo = xrmApp.Entity.GetValue("sdds_applicationnumber");
                priority = xrmApp.Entity.GetValue(new OptionSet { Name = "sdds_priority" });
            }
            return new
            {
                ApplicationNo = applicationNo,
                Priority = priority
            };
        }

        [TestMethod]
        public void UCITestOpenA24BadgerApplicationMoveBPFToTriage()
        {
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password);
                xrmApp.Navigation.OpenApp(UCIAppName.LicenceApp);
                xrmApp.Navigation.OpenSubArea("Licence", "Applications");

                //string applicationNo, priority;
                var result = CreateNewApplication(client, xrmApp);//, out applicationNo, out priority);

                xrmApp.BusinessProcessFlow.NextStage("Application Received");
                xrmApp.ThinkTime(5000);
                string status = xrmApp.Entity.GetHeaderValue(new OptionSet { Name = "statuscode" });
                for (var i = 0; i < 5; i++)
                {
                    if (status != "Received") break;
                    xrmApp.ThinkTime(5000);
                    client.Browser.Driver.Navigate().Refresh();
                    status = xrmApp.Entity.GetHeaderValue(new OptionSet { Name = "statuscode" });
                }
                Assert.AreEqual(status, "Awaiting Allocation");
            }
        }

        [TestMethod]
        public void UCITestCreateApplicationHas3Tabs()
        {
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password);

                xrmApp.Navigation.OpenApp(UCIAppName.LicenceApp);

                xrmApp.Navigation.OpenSubArea("Licence", "Applications");

                xrmApp.CommandBar.ClickCommand("New");

                var visibleTabs = xrmApp.Entity.GetVisibleTabs();
                Assert.AreEqual(visibleTabs.Count(), 3);
            }
        }

        [TestMethod]
        public void UCITestCreateA01BadgerApplicationHasSPMWLMSuffixNPriority4()
        {
            var client = new WebClient(TestSettings.Options);
            using (var xrmApp = new XrmApp(client))
            {
                xrmApp.OnlineLogin.Login(_xrmUri, _username, _password);

                xrmApp.Navigation.OpenApp(UCIAppName.LicenceApp);

                xrmApp.Navigation.OpenSubArea("Licence", "Applications");

                xrmApp.CommandBar.ClickCommand("New");

                //XrmApp.Entity.SelectLookup();
                xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationtypesid", Value = "A01 Badger", Index = 0 });
                xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicantid", Value = "AbigailPouros", Index = 0 });
                xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Prevention of damage", Index = 0 });
                xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_applicationcategory", Value = "Commercial" });
                xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_issitesameasapplicants", Value = true });



                xrmApp.Entity.Save();
                xrmApp.ThinkTime(3000);
                client.Browser.Driver.Navigate().Refresh();
                xrmApp.ThinkTime(3000);
                var applicationNo = xrmApp.Entity.GetValue("sdds_applicationnumber");
                var priority = xrmApp.Entity.GetValue(new OptionSet { Name = "sdds_priority" });
                while (string.IsNullOrWhiteSpace(applicationNo) || applicationNo.Equals("---") || string.IsNullOrWhiteSpace(priority))
                {
                    xrmApp.ThinkTime(10000);
                    client.Browser.Driver.Navigate().Refresh();
                    applicationNo = xrmApp.Entity.GetValue("sdds_applicationnumber");
                    priority = xrmApp.Entity.GetValue(new OptionSet { Name = "sdds_priority" });
                }

                xrmApp.ThinkTime(3000);
                Assert.IsTrue(applicationNo.EndsWith("SPM-WLM"));
                Assert.AreEqual(priority, "4");

            }
        }

        public static IConfiguration InitConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }
    }
}