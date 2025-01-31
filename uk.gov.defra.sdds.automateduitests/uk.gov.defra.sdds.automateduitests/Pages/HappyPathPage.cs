using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Extensions.Configuration;
using System.Security;
using BoDi;
using OpenQA.Selenium;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium.Interactions;
using NUnit.Framework.Internal;
using System.ComponentModel;
using OpenQA.Selenium.Support.UI;
using System;
using NUnit.Framework;
using FakerDotNet;
using Faker;
using Setupuk.gov.defra.sdds.automateduitests.Helper;
using TechTalk.SpecFlow;
using System.Globalization;
using Microsoft.VisualBasic;
using uk.gov.defra.sdds.automateduitests.Pages;
using System.Reflection.Emit;
using static System.Collections.Specialized.BitVector32;
using AventStack.ExtentReports.Gherkin.Model;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static Microsoft.Dynamics365.UIAutomation.Api.UCI.AppReference;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;

namespace uk.gov.defra.sdds.automateduitests.Pages
{
    [Binding]

    public class HappyPathPage
    {
        private readonly IObjectContainer container;
        private static IConfiguration _config = InitConfiguration();
        private readonly SecureString _username = _config["d365CaseworkerUsername"].ToSecureString();
        private readonly SecureString _password = _config["d365CaseworkerPassword"].ToSecureString();
        private readonly SecureString _Superusername = _config["d365SuperUserUsername"].ToSecureString();
        private readonly SecureString _Superuserpassword = _config["d365SuperUserPassword"].ToSecureString();
        private readonly SecureString _mfaSecretKey = string.Empty.ToSecureString();
        private readonly Uri _xrmUri = new Uri(_config["d365URL"]);
        private WebClient _xrmClient;
        public XrmApp _xrmApp;
        public IWebDriver win;

        public HappyPathPage(IObjectContainer container)
        {
            this.container = container;
            //this.container.RegisterInstanceAs<IWebDriver>(win);
        }



        public void refresh()
        {
            _xrmClient.Browser.Driver.Navigate().Refresh();
        }



        public void LaunchCRMApplication()
        {

            {

                _xrmClient = new WebClient(TestSettings.Options);
                _xrmApp = new XrmApp(_xrmClient);
                container.RegisterInstanceAs(_xrmApp);


                _xrmApp.OnlineLogin.Login(_xrmUri, _username, _password);


            }
        }

        public void LaunchCRMApplicationAsSuperUser()
        {

            {

                _xrmClient = new WebClient(TestSettings.Options);
                _xrmApp = new XrmApp(_xrmClient);
                container.RegisterInstanceAs(_xrmApp);


                _xrmApp.OnlineLogin.Login(_xrmUri, _Superusername, _Superuserpassword);


            }
        }
        public string prioritydata()
        {

            var data = _xrmApp.Entity.GetValue(new OptionSet { Name = "sdds_priority" });
            return data;
        }

        public void clickOnNextstage()
        {
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();
        }
        public void selectnewstage(string stage, string type)
        {

            Actions actions = new Actions(win);
            _xrmApp.ThinkTime(3000);
            var element = win.FindElement(By.XPath("//div[text()='Assessment ' and @role='presentation']"));
            actions.MoveToElement(element).DoubleClick().Perform();
          
            _xrmApp.ThinkTime(3000);
            var text1 = win.FindElement(By.XPath("//*[text()=' Is the proposed action likely to be successful in resolving the problem or meeting the need?']"));
            var text2 = win.FindElement(By.XPath("//*[text()=' Are you satisfied the persons undertaking the works are suitably experienced or competent for the licensed methods?']"));
            var text3 = win.FindElement(By.XPath("//*[text()='Assessment Outcome']"));
            var text = win.FindElement(By.XPath("//*[@aria-label='Licence decision and justification']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", text1);
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", text2);
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", text3);
            Thread.Sleep(1000);
            var actionFF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label='Assessment Outcome']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionFF);
            Thread.Sleep(1000);
            actions.MoveToElement(actionFF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='" + type + "']")).Click();
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();
            Thread.Sleep(4000);


        }

        public void feedbackrating()
        {
            Actions actions = new Actions(_xrmClient.Browser.Driver);

            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@title='---']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            actions.MoveToElement(actionF).DoubleClick().Perform();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//div[text()='Very satisfied']")).Click();
        }
        public void selectAstage(string stage)
        {

            var _browser = _xrmClient.Browser;
            win = _browser.Driver.SwitchTo().Window(_browser.Driver.CurrentWindowHandle);
            _xrmApp.BusinessProcessFlow.SelectStage(stage);
            Thread.Sleep(3000);
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();
            Thread.Sleep(2000);
            Actions action = new Actions(_browser.Driver);
            var actionA = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Is this an NSIP?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionA);
            action.MoveToElement(actionA).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            var actionD = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Previous DAS/PSS?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionD);
            action.MoveToElement(actionD).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            var actionE = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Is this phased/multi plot?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionD);
            action.MoveToElement(actionE).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Is it a live dig?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionD);
            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            var actionG = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Is a main sett impacted?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionD);
            action.MoveToElement(actionG).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();
            _xrmApp.ThinkTime(3000);
            var test = win.FindElement(By.CssSelector("*[aria-label='Allocator, Lookup']"));
            Actions actions = new Actions(win);
            actions.MoveToElement(test).DoubleClick().Perform();
            _xrmApp.ThinkTime(1000);
            win.FindElement(By.XPath("//*[@aria-label='Search records for Allocator, Lookup field']")).Click();
            _xrmApp.ThinkTime(5000);
            win.FindElement(By.XPath("//*[text()= 'Fin Rylatt']")).Click();
            win.FindElement(By.CssSelector("*[aria-label='Lead adviser, Lookup']")).Click();
            win.FindElement(By.XPath("//*[@aria-label='Search records for Lead adviser, Lookup field']")).Click();
            _xrmApp.ThinkTime(1000);

            win.FindElement(By.XPath("//*[text()= '# Laura Alvey (NE)']")).Click();
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();

            _xrmApp.ThinkTime(4000);

        }
     
        public void Pauseapplication()
        {
            _xrmApp.CommandBar.ClickCommand("Pause");
            var dropdown = win.FindElement(By.XPath("//*[@aria-label='Reason for pause']"));
            dropdown.Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//div[text()='Compliance/Enforcement investigation - notify only']")).Click();
            _xrmApp.CommandBar.ClickCommand("Save & Close");
            _xrmApp.ThinkTime(500);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@data-id='confirmButton']")).Click();
        }

        public string PauseValue()
        {
            return win.FindElement(By.XPath("//span[text()='Pause']")).Text;
        }

        public void AddCompliance()
        {
            Actions actions = new Actions(win);
            Thread.Sleep(3500);
            var actionB = win.FindElement(By.XPath("//*[text()='Compliance']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            Thread.Sleep(1500);
            win.FindElement(By.XPath("//*[text()='New Compliance Check']")).Click();
        }
        public void Resumeapplication()
        {
            _xrmApp.CommandBar.ClickCommand("Resume");
            _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@data-id='confirmButton']")).Click();
        }

        public void Withdrawapplication()
        {
            _xrmApp.ThinkTime(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[text()='Withdraw Case']")).Click();
            _xrmApp.ThinkTime(2500);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@data-id='confirmButton']")).Click();
            _xrmApp.ThinkTime(1500);
            Actions action = new Actions(_xrmClient.Browser.Driver);
            Thread.Sleep(2000);
            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label='Reason for Withdrawing']"));
            action.MoveToElement(actionF).DoubleClick().Perform();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//div[text()='Withdrawal due to customer request']")).Click();
            _xrmApp.CommandBar.ClickCommand("Save & Close");
        }
        public String verifyStatues()
        {
            var actualResult = win.FindElement(By.XPath("//*[@data-preview_orientation='column']/div/div")).Text;
            return actualResult;

        }

        public void OpenLicenceApp()
        {
            _xrmApp.Navigation.OpenApp(UCIAppName.LicenceApp);
        }

        public void OpenFeedbackApp()
        {
            _xrmApp.Navigation.OpenApp(UCIAppName.FeedbackApp);
        }
        public void OpenAdminApp()
        {
            _xrmApp.Navigation.OpenApp(UCIAppName.AdminApp);
        }

        public void OpenApplications(string applicationType)
        {
            if (applicationType == "Applications")
            {
                _xrmApp.Navigation.OpenSubArea("Licence", "Applications");
            }
            else if (applicationType == "Feedback")
            {
                _xrmApp.Navigation.OpenSubArea("Feedback", null);
            }
            else if (applicationType == "Admin")
            {
                Thread.Sleep(2000);

                _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@id='areaSwitcherId']/div/span")).Click();
                _xrmClient.Browser.Driver.FindElement(By.XPath("//li[@title='Admin']")).Click();
            }
        }
        public void selectpermissiontype() { 
        Actions action = new Actions(_xrmClient.Browser.Driver);

        var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Permission Type']"));
        ((IJavaScriptExecutor) _xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
        action.MoveToElement(actionF).DoubleClick().Perform();
        win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
        win.FindElement(By.XPath("//div[text()='Planning permission']")).Click();
    }

        public void selectpermissionoption()
        {
            Actions action = new Actions(_xrmClient.Browser.Driver);

            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Planning permission type']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Full']")).Click();
        }
        public void ClickonSave()
        {
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//*[text()='Save']")).Click();
            Thread.Sleep(2000);
        }

        public void populatedChargeRequest()
        {
            Actions actions = new Actions(win);
            _xrmApp.ThinkTime(2000);
            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@title='---']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            actions.MoveToElement(actionF).DoubleClick().Perform();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Apply compliance fee']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//div[text()='Yes']")).Click();
             var actionJ = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@id='duration-combobox']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionJ);
            actions.MoveToElement(actionJ).Click().Perform();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[text()='1 hour']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@data-id='quickCreateSaveAndCloseBtn']")).Click();
            _xrmApp.ThinkTime(600);

        }
        public string getwarningMessage()
        {
            return _xrmClient.Browser.Driver.FindElement(By.Id("message-SuccessInfoTabxxxx")).Text;
        }
        public void ClickSaveandClose()
        {
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//*[text()='Save & Close']")).Click();
            Thread.Sleep(2000);
        }
        public string getFeedbackRef()
        {
            Thread.Sleep(2000);
            return win.FindElement(By.Id("formHeaderTitle_2")).Text;
        }
        public void enterFeedbackRef(string reference)
        {
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//input[@placeholder='Quick find']")).SendKeys(reference);
        }

        public void clickOnSearch()
        {
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//input[@placeholder='Quick find']")).SendKeys(Keys.Enter);
        }

        public string priorityvalue()
        {
            Thread.Sleep(2000);
           string text = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label='Priority']")).Text;
            return text;
        }

        

        public void setlicencestatus()
        {
            Actions action = new Actions(_xrmClient.Browser.Driver);

            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@title='Draft']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='Draft']")).Click();
            win.FindElement(By.XPath("//div[text()='Active']")).Click();
        }
        public void ClickonSaveandClose()
        {

            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[text()='Save & Close']")).Click();
        }

        public void Continueanyway()
        {

            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[text()='Continue anyway']")).Click();
        }

        public string getref()
        {

            var refs = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@data-id='header_title']")).Text;
            return refs;
        }



        public void ClickonAdmin()
        {

            win.FindElement(By.XPath("//*[@title='Admin (change area)']")).Click();
        }
        public string getcurrentUrl()
        {
            Thread.Sleep(6000);
           Console.WriteLine(_xrmClient.Browser.Driver.WindowHandles.Count());
            _xrmClient.Browser.Driver.SwitchTo().Window(win.WindowHandles[1]);
            var url = win.Url;
            return url;
        }
        public void switchToFirstTab()
        {
            Thread.Sleep(5000);
            _xrmClient.Browser.Driver.SwitchTo().Window(_xrmClient.Browser.Driver.WindowHandles[0]);
            Thread.Sleep(6500);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@title='Accept']")).Click();
            Thread.Sleep(3000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@data-id='confirmButton']")).Click();
            Thread.Sleep(3000);


        }

        public void switchTonewFirstTab()
        {
            Thread.Sleep(5000);
            _xrmClient.Browser.Driver.SwitchTo().Window(_xrmClient.Browser.Driver.WindowHandles[0]);
        }

        public void switchToReturnFirstTab()
        {
            Thread.Sleep(2000);
            win.SwitchTo().Window(win.WindowHandles[1]);
            Thread.Sleep(5000);
            

        Actions action = new Actions(_xrmClient.Browser.Driver);

            var actionG = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did you carry out any of these actions? (The licenced actions)']"));
            //action.MoveToElement(actionB).Click();
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionG);
            action.MoveToElement(actionG).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();

            _xrmApp.Entity.SetValue("sdds_receiveddate", DateTime.Today, "dd/MM/yyyy", "HH:mm");
            var actionlu = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@title='General questions']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionlu);
            _xrmApp.Entity.SetValue("sdds_whendidyoustartwork", DateTime.Today, "dd/MM/yyyy", "HH:mm");

            _xrmApp.Entity.SetValue("sdds_whendidyoucompletethework", DateTime.Today, "dd/MM/yyyy", "HH:mm");

            var actionJ = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Were the licensed activities completed within the licensed period (between licence start and end dates)?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionJ);
            action.MoveToElement(actionJ).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            Thread.Sleep(2000);
            var actionL = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did the actions taken under the licence meet the need or purpose?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionL);
            action.MoveToElement(actionL).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='No']")).Click();

            Thread.Sleep(3000);
            _xrmApp.Entity.SetValue("sdds_developmentcouldstartdescription", "testing one");
            var actionLP = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Were all licensed conditions met?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionLP);
            action.MoveToElement(actionLP).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='No']")).Click();
            _xrmApp.Entity.SetValue("sdds_ifnolicenceconditiondescription", "testing new");
        }

        public void PopulateA24tab(string formType)
        {
            Actions action = new Actions(_xrmClient.Browser.Driver);

            if (formType == "A25")
            {
                win.FindElement(By.XPath("//li[@aria-label='A25 Badger']")).Click();
                Thread.Sleep(500);
                _xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_siteid", Value = "0ugvv25lwtup6ne8rmwerqkl05wmm0mf05ldgkv5e48gc1hgfb93cab5qs2w15526d8kyzqsjfny11d1a6xx6l998ezclhznjkya", Index = 0 });

                var actionLY = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did you carry out any of the actions on this site?']"));
                ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionLY);
                action.MoveToElement(actionLY).DoubleClick().Perform();
                win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
                win.FindElement(By.XPath("//div[text()='Yes']")).Click();
                _xrmApp.Entity.SetValue("sdds_numberofsettsdestroyed", "30");
                _xrmApp.Entity.SetValue("sdds_dateofactiondetroyed", DateTime.Today, "dd/MM/yyyy", "HH:mm");
                _xrmApp.Entity.SetValue("sdds_contributiontoconservation", "teets");
                var actionXY = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did any of the licensed actions / methods cause any concerns for badger welfare?']"));
                ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionXY);
                action.MoveToElement(actionXY).DoubleClick().Perform();
                win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
                win.FindElement(By.XPath("//div[text()='Yes']")).Click();
                _xrmApp.Entity.SetValue("sdds_yesconcernsforbadgerwelfare", "teetsnew");
                Thread.Sleep(200);
                var actionYY = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Any additional relevant information?']"));
                ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionYY);
                _xrmApp.Entity.SetValue("sdds_anyadditionalrelevantinformation", "teetsnew additional");
                win.FindElement(By.XPath("//*[@id='sdds_returnofaction|NoRelationship|Form|Mscrm.SaveAndClosePrimary10-button']")).Click();
                Thread.Sleep(2000);
            }
            else {
            win.FindElement(By.XPath("//li[@aria-label='Badger Returns Detail']")).Click();
              
            Thread.Sleep(500);
            _xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_siteid", Value = "0ugvv25lwtup6ne8rmwerqkl05wmm0mf05ldgkv5e48gc1hgfb93cab5qs2w15526d8kyzqsjfny11d1a6xx6l998ezclhznjkya", Index = 0 });
            var actionX = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did you obstruct sett entrances by means of one-way badger gates?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionX);
            action.MoveToElement(actionX).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            _xrmApp.Entity.SetValue("sdds_obstructsettentrancesonewaydescription", "test36new1");
            _xrmApp.Entity.SetValue("sdds_dateofactiononewaybadgergate", DateTime.Today, "dd/MM/yyyy", "HH:mm");
            _xrmApp.Entity.SetValue("sdds_numberofsettsaffectedonewaybadger", "2");
            var actionk = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did you obstruct access to sett entrances by blocking or proofing?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionk);
            action.MoveToElement(actionk).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            _xrmApp.Entity.SetValue("sdds_blockingorproofingdescription", "test36new2");
            _xrmApp.Entity.SetValue("sdds_dateofactionblockingorpoofing", DateTime.Today, "dd/MM/yyyy", "HH:mm");
            _xrmApp.Entity.SetValue("sdds_numberofsettsaffectedblockingorpoofing", "30");
            var actiony = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did you damage a sett by hand and mechanical means?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actiony);
            action.MoveToElement(actiony).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            _xrmApp.Entity.SetValue("sdds_damagebyhandmechanicalmeansdescription", "test36new2");
            _xrmApp.Entity.SetValue("sdds_dateofactionhandandmechanical", DateTime.Today, "dd/MM/yyyy", "HH:mm");
            _xrmApp.Entity.SetValue("sdds_numberofsettsaffectedhandandmechanical", "30");
            var actionp = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did you destroy a vacant sett by hand or mechanical means?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionp);
            action.MoveToElement(actionp).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            _xrmApp.Entity.SetValue("sdds_destroysettbyhandmechanicaldecsription", "test36new3");
            _xrmApp.Entity.SetValue("sdds_dateofactiondestroybyhandormech", DateTime.Today, "dd/MM/yyyy", "HH:mm");
            _xrmApp.Entity.SetValue("sdds_numberofsettsaffecteddestroybyhandormech", "30");
            _xrmApp.Entity.SetValue("sdds_whendidyoudestroythevacantsett", DateTime.Today.AddYears(-1), "dd/MM/yyyy");
            var actionj = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did you disturb badgers?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionj);
            action.MoveToElement(actionj).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            _xrmApp.Entity.SetValue("sdds_disturbbadgerdescription", "test");
            _xrmApp.Entity.SetValue("sdds_dateofactiondestroybyhandormech", DateTime.Today, "dd/MM/yyyy", "HH:mm");
            _xrmApp.Entity.SetValue("sdds_numberofsettsaffecteddestroybyhandormech", "30");
            var actionw= _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did you create an artificial sett?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionw);
            action.MoveToElement(actionw).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            _xrmApp.Entity.SetValue("sdds_artificialsettdescription", "testing");
            var actionx = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Was the artificial sett created before the licensed sett was closed?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionx);
            action.MoveToElement(actionx).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            _xrmApp.Entity.SetValue("sdds_evidencebadgersfoundtheartificialsett", "testing");
            _xrmApp.Entity.SetValue("sdds_artificialbadgersettgridreference", "NY123456");
            var actionA = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Did any of the licensed actions / methods cause any concerns for badger welfare?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionA);
            action.MoveToElement(actionA).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
           _xrmApp.Entity.SetValue("sdds_badgerswelfaredescription", "test36new5");
            _xrmApp.Entity.SetValue("sdds_anyadditionalrelevantinformation", "testing info");
            win.FindElement(By.XPath("//*[@id='sdds_returnofaction|NoRelationship|Form|Mscrm.SaveAndClosePrimary10-button']")).Click();
            Thread.Sleep(2000);
            }
        }

        public void seleniumRefresh()
        {


            _xrmClient.Browser.Driver.Navigate().Refresh();

        }
        public void powerAppRefresh()
        {


            win.FindElement(By.XPath("//*[text()='Refresh']")).Click();
        }
        public string GetLicenceGrantedText()
        {
            Thread.Sleep(10000);
            win.SwitchTo().Window(win.WindowHandles[0]);
            var grantedText = win.FindElement(By.XPath("//div[@data-id='form-header']")).Text;
            return grantedText;
        }

        public string GetNotGrantedText()
        {
            Thread.Sleep(10000);
            win.SwitchTo().Window(win.WindowHandles[0]);
            var grantedText = win.FindElement(By.XPath("//*[text()='Not Granted']")).Text;
            return grantedText;
        }

        public string GrantedText()
        {
            Thread.Sleep(7000);
            win.SwitchTo().Window(win.WindowHandles[1]);
            var grantedText = win.FindElement(By.XPath("//*[text()='Granted']")).Text;
            return grantedText;
        }
        public string ClickonAcceptLicence()
        {
            Thread.Sleep(14000);
            var currentUrl = win.Url;
            return currentUrl;


        }

        public void closetab()
        {


            win.Close();
        }
        public void ClickonSaveandcontinue()

        {

            win.FindElement(By.XPath("//*[text()='Save & continue']")).Click();
        }
        public void ClickonSaveandContinue()
        {

            win.FindElement(By.XPath("//*[text()='Save & Close']")).Click();
            _xrmApp.ThinkTime(7000);
            win.FindElement(By.CssSelector("span[data-id='warningNotification']")).Click();

        }
        public void ClickonExperienceassessmentTasks()
        {
            Actions action = new Actions(win);
            var actionCJ = win.FindElement(By.XPath("//*[@title='Experience of ecologist or applicant']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionCJ);
            win.FindElement(By.XPath("//*[@title='Experience of ecologist or applicant']")).Click();
            action.MoveToElement(actionCJ).DoubleClick().Perform();
            _xrmApp.Entity.SetValue("sdds_additionalinformation", FakerDotNet.Faker.Lorem.Characters(4000));
            var actionE = win.FindElement(By.XPath("//*[@title='Not Satisfied']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionE);
            var actionD = win.FindElement(By.XPath("//button[@role='combobox']"));
            action.MoveToElement(actionD).Click().Perform();
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//div[text()='Satisfied']")).Click();
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();
        }
        public void ClickonRiskAssessmentTasks(string formType)
        {

            Actions action = new Actions(win);
            var actionC = win.FindElement(By.XPath("//*[text()='Not Satisfied']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionC);
            if (formType.Equals("A24") || formType.Equals("A25"))
            {

                win.FindElement(By.XPath("//*[@title='Disease risk assessment outcome']")).Click();
                var actionAZ = win.FindElement(By.XPath("//*[@title='Disease risk assessment outcome']"));
                action.MoveToElement(actionAZ).DoubleClick().Perform();

            }
            else if (formType.Equals("A01"))
            {
                win.FindElement(By.XPath("//*[@title='Disease risk assessment outcome']")).Click();
                var actionAZ = win.FindElement(By.XPath("//*[@title='Disease risk assessment outcome']"));
                action.MoveToElement(actionAZ).DoubleClick().Perform();

            }
          
            else if (formType.Equals("A26"))
            {
                win.FindElement(By.XPath("//*[@title='Disease risk assessment outcome']")).Click();
                var actionAZ = win.FindElement(By.XPath("//*[@title='Disease risk']"));
                action.MoveToElement(actionAZ).DoubleClick().Perform();

               
            }
            Thread.Sleep(3000);
            var actionB = win.FindElement(By.XPath("//*[@title='Not Satisfied']"));

            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);

            action.MoveToElement(actionB).DoubleClick().Perform();
            action.MoveToElement(actionB).DoubleClick().Perform();



            win.FindElement(By.XPath("//*[@title='Not Satisfied']")).Click();
            win.FindElement(By.XPath("//*[text()='Satisfied']")).Click();
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();


        }
        public void addLicensableMethod(string formtype)
        {
            Thread.Sleep(2000);

            Actions action = new Actions(win);
            var actionB = win.FindElement(By.XPath("//*[text()='Licensable Action(s)']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            if (formtype.Equals("A01")) { 
            var actionA = win.FindElement(By.XPath("//*[@aria-label='Main - no alternative sett']"));
            action.MoveToElement(actionA).DoubleClick().Perform();
            Thread.Sleep(4000);
        }
            else if (formtype.Equals("A24"))
            {
                var actionA = win.FindElement(By.XPath("//*[@aria-label='Main - no alternative sett']"));
                action.MoveToElement(actionA).DoubleClick().Perform();
                Thread.Sleep(4000);
            }

            else if (formtype.Equals("A26") || formtype.Equals("A25"))
            {
                var actionA = win.FindElement(By.XPath("//*[@aria-label='Use']"));
                action.MoveToElement(actionA).DoubleClick().Perform();
                Thread.Sleep(4000);
            }
            
            var actionC = win.FindElement(By.XPath("//*[text()='Method or Field Technique']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionC);
            win.FindElement(By.XPath("//*[@aria-label='Licence Method Commands']")).Click();
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//*[text()='Add Existing Licence Method']")).Click();
            Thread.Sleep(2500);
            if (formtype.Equals("A01"))
            {
                win.FindElement(By.XPath("//li[@aria-label='Damaging a sett by hand and mechanical means, 02/05/2024 01:06']")).Click();
                win.FindElement(By.XPath("//*[text()='Add']")).Click();
                Thread.Sleep(2000);
            }
            else if (formtype.Equals("A24"))
            {
                win.FindElement(By.XPath("//li[@aria-label='Destruction of the vacant sett by hand and mechanical means, 29/09/2022 08:59']")).Click();
                win.FindElement(By.XPath("//*[text()='Add']")).Click();
                Thread.Sleep(2000);
            }
            else if (formtype.Equals("A25"))
            {
                win.FindElement(By.XPath("//li[@aria-label='Machinery, 24/11/2023 14:27']")).Click();
                Thread.Sleep(1000);
                win.FindElement(By.XPath("//*[text()='Add']")).Click();
                Thread.Sleep(1000);
            }
        }
        public void ClickonLicensableactions()
        {
            Actions action = new Actions(win);
            var actionB = win.FindElement(By.XPath("//*[text()='New Permission']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);

            var actionA = win.FindElement(By.XPath("//*[text()='New Licensable Action']"));
            action.MoveToElement(actionA).Click().Perform();

        }



        public void AddReturnReceivedDate()




        {
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of Received date']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-current='date']")).Click();
            Thread.Sleep(4000);
        }



        public void AddReceivedDate()




        {
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of Application form received date']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-current='date']")).Click();
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Time of Application form received date']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Time of Application form received date']")).SendKeys("04:00");

            Thread.Sleep(4000);
        }
        public void selectSettType()
        {
            Actions action = new Actions(_xrmClient.Browser.Driver);

            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Sett type']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Main - no alternative sett']")).Click();
        }
        public void AddProposeStartDate()




        {
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of Proposed Start Date']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of Proposed Start Date']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
  

        }

        public void AddProposeEndDate()




        {
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of Proposed End Date']")).Click();
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of Proposed End Date']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-current='date']")).Click();
           
        }
        public void AddLicenceStartDate()




        {
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of Licence start date']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
        }

        public void AddLicenceEndDate()




        {
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of Licence end date']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of Licence end date']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

        }

        public void AddSettsDate()




        {
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Date of When did you destroy the vacant sett?']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-current='date']")).Click();

        }


        public void removeAddedUser(string user, string role)

        {
            Thread.Sleep(1000);

            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Teams']")).Click();

            Thread.Sleep(2000);

            _xrmClient.Browser.Driver.FindElement(By.XPath("//span[text()=" + "'" + role + "'" + "]")).Click();
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//input[@placeholder = 'Quick find']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//input[@placeholder = 'Quick find']")).SendKeys(user);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//input[@placeholder = 'Quick find']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//input[@placeholder = 'Quick find']")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//div[@role='gridcell']//div")).Click();
            Thread.Sleep(2000);

            _xrmClient.Browser.Driver.FindElement(By.XPath("//span[text()='Remove']")).Click();

        }


        public void UserRoleSelection(string role)




        {
            Thread.Sleep(1000);

            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Teams']")).Click();
            Thread.Sleep(2000);

            _xrmClient.Browser.Driver.FindElement(By.XPath("//span[text()=" + "'" + role + "']")).Click();

        }
        public void addExistingUserAccount(string user)




        {
            Thread.Sleep(4000);

            _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label='More commands for User']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//span[text()='Add Existing User']")).Click();
            Thread.Sleep(2000);

            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@id='lookupDialogLookup-MscrmControls.FieldControls.SimpleLookupControl-Lookup_falseBoundLookup_2_microsoftIcon_searchButton']")).Click();

            _xrmClient.Browser.Driver.FindElement(By.XPath("//input[@id='lookupDialogLookup-MscrmControls.FieldControls.SimpleLookupControl-LookupResultsPopup_falseBoundLookup_2_textInputBox_with_filter_new']")).SendKeys(user);
            Thread.Sleep(2000);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@id='lookupDialogLookup-MscrmControls.FieldControls.SimpleLookupControl-fullname0_0_0']")).Click();

            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@id='lookupDialogSaveBtn']")).Click();

        }



        public void ClickOnSameAsApplicant()
        {

            win.FindElement(By.XPath("//button[@data-id='sdds_applicantthesameasbillingcustomer.fieldControl-option-set-select']")).Click();
            win.FindElement(By.XPath("//*[@data-id='sdds_applicantthesameasbillingcustomer.fieldControl-option-set-select' and @title='No']")).Click();

        }
        public void movetoelementsameuser()
        {

            Actions action = new Actions(_xrmClient.Browser.Driver);
            var actionB = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[text()='Invoice details']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Is Applicant the same as Billing Customer?']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[text()='Yes']")).Click();
        }

        public void selecapplicationcategory()
        {

            Actions action = new Actions(_xrmClient.Browser.Driver);
            var actionB = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[text()='Licence Exempted?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='Application Category']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[text()='Commercial']")).Click();
        }

        public void taskSorting()

        {
            Actions action = new Actions(win);
            var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            var actionBG = win.FindElement(By.XPath("//*[text()='Subject']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBG);
            action.MoveToElement(actionBG).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[text()='Sort A to Z']")).Click();
        }
        public void ClickonRiskSensitivity(string formtypes)
        {

            Actions action = new Actions(win);
            var actionBY = win.FindElement(By.XPath("//*[text()='Technical Assessment Tasks']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBY);
            win.FindElement(By.XPath("//*[@data-id='cell-2-2']")).Click();
            var actionBG = win.FindElement(By.XPath("//*[@data-id='cell-2-2']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBG);
            action.MoveToElement(actionBG).DoubleClick().Perform();
            Thread.Sleep(3500);
            Thread.Sleep(5000);
            _xrmApp.Entity.SetValue("sdds_additionalinformation", FakerDotNet.Faker.Lorem.Characters(4000));
            var actionC = win.FindElement(By.XPath("//*[@title='Not Satisfied']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionC);

            var actionD = win.FindElement(By.XPath("//button[@role='combobox']"));
            action.MoveToElement(actionD).Click().Perform();
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//div[text()='Satisfied']")).Click();
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();


        }

        public void SelectDecision()
        {
            Thread.Sleep(4500);

            win.FindElement(By.CssSelector("li[aria-label='More Tabs']")).Click();
            win.FindElement(By.XPath("//*[text()='Decision']")).Click();
        }
        public void clickOnRating()
        {
            win.FindElement(By.XPath("//button[@aria-label='Rating']']")).SendKeys(Keys.Return);
            Thread.Sleep(4000);
      
        }
        public void ClickOnAddNewLicence()
        {
            Thread.Sleep(4000);

            win.FindElement(By.XPath("//*[text()='Generate Licence']")).Click();
            Thread.Sleep(4000);


        }
        public void ClickonBackgroundInformation()
        {

            Actions action = new Actions(win);
            
            var actionBY = win.FindElement(By.XPath("//*[text()='Technical Assessment Tasks']"));

           
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBY);
           
            win.FindElement(By.XPath("//*[@data-id='cell-0-2']")).Click();
            var actionBG = win.FindElement(By.XPath("//*[@data-id='cell-0-2']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBG);
            action.MoveToElement(actionBG).DoubleClick().Perform();
            Thread.Sleep(3500);
            
            Thread.Sleep(4000);
            _xrmApp.Entity.SetValue("sdds_additionalinformation", FakerDotNet.Faker.Lorem.Characters(4000));
            var actionC = win.FindElement(By.XPath("//*[@title='Not Satisfied']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionC);

            var actionD = win.FindElement(By.XPath("//button[@role='combobox']"));
            action.MoveToElement(actionD).Click().Perform();
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//div[text()='Satisfied']")).Click();
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();

        }

        public void ClickonSurvey()
        {
            
           
            Actions action = new Actions(win);
            var actionBY = win.FindElement(By.XPath("//*[text()='Technical Assessment Tasks']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBY);

            var actionBG = win.FindElement(By.XPath("//*[@title='Next page']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBG);
            action.MoveToElement(actionBG).DoubleClick().Perform();

            Thread.Sleep(2500);

            var actionBK = win.FindElement(By.XPath("//*[text()='Technical Assessment Tasks']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBK);
            win.FindElement(By.XPath("//*[@data-id='cell-0-2']")).Click();
            var actionBF = win.FindElement(By.XPath("//*[@data-id='cell-0-2']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBG);
            action.MoveToElement(actionBF).DoubleClick().Perform();
            Thread.Sleep(4000);
            _xrmApp.Entity.SetValue("sdds_additionalinformation", FakerDotNet.Faker.Lorem.Characters(4000));

            var actionC = win.FindElement(By.XPath("//*[@title='Not Satisfied']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionC);

            var actionD = win.FindElement(By.XPath("//button[@role='combobox']"));
            action.MoveToElement(actionD).Click().Perform();
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//div[text()='Satisfied']")).Click();
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();

        }
        public void ClickonDiseaseRisk()
        {

            Actions action = new Actions(win);
            var actionBY = win.FindElement(By.XPath("//*[text()='Technical Assessment Tasks']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBY);
            win.FindElement(By.XPath("//*[@data-id='cell-3-2']")).Click();
            var actionBG = win.FindElement(By.XPath("//*[@data-id='cell-3-2']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBG);
            action.MoveToElement(actionBG).DoubleClick().Perform();
            Thread.Sleep(5000);
            _xrmApp.Entity.SetValue("sdds_additionalinformation", FakerDotNet.Faker.Lorem.Characters(4000));
            var actionC = win.FindElement(By.XPath("//*[@title='Not Satisfied']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionC);

            var actionD = win.FindElement(By.XPath("//button[@role='combobox']"));
            action.MoveToElement(actionD).Click().Perform();
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//div[text()='Satisfied']")).Click();
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();

        }

        public void ClickonConservation()
        {
            Actions action = new Actions(win);
            var actionBY = win.FindElement(By.XPath("//*[text()='Technical Assessment Tasks']"));
            Thread.Sleep(500);
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBY);
            win.FindElement(By.XPath("//*[@data-id='cell-1-2']")).Click();
            var actionBG = win.FindElement(By.XPath("//*[@data-id='cell-1-2']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionBG);
            action.MoveToElement(actionBG).DoubleClick().Perform();
            Thread.Sleep(5000);
            _xrmApp.Entity.SetValue("sdds_additionalinformation", FakerDotNet.Faker.Lorem.Characters(4000));
            var actionCk = win.FindElement(By.XPath("//*[@title='Not Satisfied']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionCk);

            var actionD = win.FindElement(By.XPath("//button[@role='combobox']"));
            action.MoveToElement(actionD).Click().Perform();
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//div[text()='Satisfied']")).Click();
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();

        }


        public void ClickonNewDesignatedSite()
        {

            win.FindElement(By.XPath("//*[text()='New Designated Site']")).Click();

        }
        
        public void addDesignatedSitdevelopment()
        {
            Thread.Sleep(500);
            Actions action = new Actions(_xrmClient.Browser.Driver);
            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label='Will the development take place on or next to the Site?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Next to']")).Click();

        }

        public void selectA01TakinAction()
        {
            Thread.Sleep(500);
            Actions action = new Actions(_xrmClient.Browser.Driver);
            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label='Have you taken any action to prevent the problems outlined above?']"));
            //var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[text()='Next to']"));
            //action.MoveToElement(actionB).Click();
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            action.MoveToElement(actionF).DoubleClick().Perform();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            _xrmClient.Browser.Driver.FindElement(By.XPath("//div[text()='Yes']")).Click();

        }
        public void addonornexttodesignated()
        {
            Thread.Sleep(4000);
            Actions action = new Actions(_xrmClient.Browser.Driver);

            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//*[@aria-label='On/Next to Designated site?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();

        }
        public void addNAadvice()
        {

            Actions action = new Actions(_xrmClient.Browser.Driver);

            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@title='---']"));
            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();

        }
        public void ClickonTechnicalAssessment()
        {
            Thread.Sleep(3000);

            Actions action = new Actions(win);
            var element = win.FindElement(By.XPath("//*[text()='Save']"));
            action.MoveToElement(element).DoubleClick().Perform();
            Thread.Sleep(3000);

        }
        public void SelectTechnicalAssessment()
        {
            _xrmApp.ThinkTime(2500);
            win.FindElement(By.CssSelector("li[aria-label='More Tabs']")).Click();
            win.FindElement(By.XPath("//*[text()='Technical Assessment']")).Click();
            Thread.Sleep(3000);
        }


        public void licencerequiredforproposedactivity() {
            Thread.Sleep(2000);
        Actions action = new Actions(_xrmClient.Browser.Driver);
            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label=' Is a licence required for the proposed activities?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            action.MoveToElement(actionF).DoubleClick().Perform();
        win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
        win.FindElement(By.XPath("//div[text()='Yes']")).Click();
      
        }

        public void proposedactionsproportionatescale()
        {
            Actions action = new Actions(_xrmClient.Browser.Driver);

            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label='Is the proposed action proportionate to the scale of problem or need?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);
            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();
            
        }

        public void proposedactionscouldresolveproblem()
        {
            Actions action = new Actions(_xrmClient.Browser.Driver);

            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label='Is the proposed action likely to be successful in resolving the problem or meeting the need?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);

            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();

        }

        public void satisfiedwithpersonsundertakingworks()
        {
            Actions action = new Actions(_xrmClient.Browser.Driver);

            var actionF = _xrmClient.Browser.Driver.FindElement(By.XPath("//button[@aria-label='Are you satisfied the persons undertaking the works are suitably experienced or competent for the licensed methods?']"));
            ((IJavaScriptExecutor)_xrmClient.Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", actionF);

            action.MoveToElement(actionF).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[@title='--Select--']")).Click();
            win.FindElement(By.XPath("//div[text()='Yes']")).Click();

        }
        public void ClickonQuickSaveAndContinue()
        {
            win.FindElement(By.XPath("//*[@data-id='quickCreateSaveAndCloseBtn']")).Click();
        }
        public void ClickonNewPermission()
        {
            win.FindElement(By.XPath("//*[text()='New Permission']")).Click();
        }

        public void ClickOnMiscellaneous()
        {
            win.FindElement(By.XPath("//button[@aria-label = 'Miscellaneous More Commands']")).Click();
        }
        public void ClickOnLicencegenerate()
        {
            win.FindElement(By.XPath("//*[text()='Generate Document']")).Click();
            _xrmApp.ThinkTime(13000);

        }

        public void ClickOnWithdraw()
        {
            win.FindElement(By.XPath("//*[text()='Withdraw Case']")).Click();
        }


        public void UploadDocuments()
        {
            _xrmApp.ThinkTime(2500);
            win.FindElement(By.CssSelector("button[aria-label='More commands for Sharepoint Document']")).Click();
            win.FindElement(By.XPath("//*[text()='Upload']")).Click();
            _xrmApp.ThinkTime(2500);
            Actions action = new Actions(win);

            var actionB = win.FindElement(By.XPath("//*[text()='Upload Documents']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            win.SwitchTo().DefaultContent();
            string path = "C:\\Users\\User\\Desktop\\wildlife-licencing-powerapps-plugins\\uk.gov.defra.sdds.automateduitests\\uk.gov.defra.sdds.automateduitests\\Documents\\2023-004693-WLM-LIC-licence document.pdf";
            var actionA = win.FindElement(By.CssSelector("input[aria-label='File Upload']"));
            action.MoveToElement(actionA).DoubleClick().SendKeys(path).Perform();

            win.FindElement(By.XPath("//*[text()='OK']")).Click();
            _xrmApp.ThinkTime(2500);


        }

        public void ClickOnAddExistingSite()
        {
            Actions actions = new Actions(win);

            _xrmApp.ThinkTime(1000);
            win.FindElement(By.XPath("//*[text()= 'Add Site']")).Click();
            _xrmApp.ThinkTime(500);
            win.FindElement(By.CssSelector("button[data-id='MscrmControls.FieldControls.SimpleLookupControl-LookupResultsPopup_falseBoundLookup_search']")).Click();
            _xrmApp.ThinkTime(1000);
            var news = win.FindElement(By.XPath("//li[@data-id='MscrmControls.FieldControls.SimpleLookupControl-LookupResultsPopup_falseBoundLookup_resultsContainer'][1]"));
            _xrmApp.ThinkTime(500);
            actions.MoveToElement(news).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[text()='Add']")).Click();

        }

        public void ClickOnAddReturnOfAction()
        {
            Actions actions = new Actions(win);
            _xrmApp.Entity.SelectTab("Decision");
            _xrmApp.ThinkTime(2000);
            
            IWebElement selectroa = win.FindElement(By.XPath("//*[text()='Active']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", selectroa);
            win.FindElement(By.XPath("//*[text()='Active']")).Click();
            actions.DoubleClick(selectroa).Perform();

            _xrmApp.ThinkTime(2000);
            win.FindElement(By.CssSelector("li[aria-label='Report of Action']")).Click();
          
            _xrmApp.ThinkTime(2000);
            IWebElement newone = win.FindElement(By.XPath("//Section[@aria-label='Returns']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", newone);
            var button = win.FindElement(By.XPath("//*[text()='New Report of Action']"));
            actions.DoubleClick(button).Perform();

        }
        public void ClickOnReturnOfAction()
        {
            Actions actions = new Actions(win);
            _xrmApp.ThinkTime(2000);
            var button = win.FindElement(By.XPath("//*[text()='Report of Action']"));
            actions.DoubleClick(button).Perform();

        }



        public void enterData(string stage)
        {

            var _browser = _xrmClient.Browser;
            var win = _browser.Driver.SwitchTo().Window(_browser.Driver.CurrentWindowHandle);
            _xrmApp.BusinessProcessFlow.SelectStage(stage);
            _xrmApp.Entity.SetValue(new OptionSet { Name = "header_process_sdds_isthisannsip", Value = "Yes" });
            _xrmApp.Entity.SetValue(new OptionSet { Name = "header_process_sdds_previousdaspss", Value = "Yes" });
            _xrmApp.Entity.SetValue(new OptionSet { Name = "header_process_sdds_isthisphasedmultiplot", Value = "Yes" });
            _xrmApp.Entity.SetValue(new OptionSet { Name = "header_process_sdds_isitalivedig", Value = "Yes" });
            _xrmApp.Entity.SetValue(new OptionSet { Name = "header_process_sdds_isamainsettimpacted", Value = "Yes" });
            _xrmApp.Entity.SetValue(new OptionSet { Name = "header_process_sdds_impactedtodesignatedprotectedsite", Value = "Yes" });
        }

        public void getLicenseNo()
        {
            win.FindElement(By.XPath("//*[text()='Decision']")).Click();

            win.FindElement(By.XPath("//*[@data-id='Subgrid_11-pcf_grid_control_container']/div/div[1]/div/div/div/div/div[1]/div[2]/div[3]/div[2]/div/div/div/div[2]/div/div/div/div/div[1]/div/a/div/span")).Click();
            win.FindElement(By.XPath("li[aria-label='Report of Action']")).Click();
            win.FindElement(By.XPath("button[aria-label='New Report of Action. Add New Report of Action']")).Click();

        }
        [AfterScenario]
        public void CloseApp()

        {
            if (_xrmApp != null)
            {
                _xrmApp.Dispose();
            }
        }

        //[AfterScenario]
        //public void addEvidence()
        //{ 
        //Screenshot ss = ((ITakesScreenshot)win).GetScreenshot();
        //ss.SaveAsFile(@"..\TestResultEvidence\SeleniumTestingScreenshot"+ ".jpg");
        //Console.WriteLine("Capture Screenshot as Test Result Evidence");
        //    }
        public static IConfiguration InitConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }
    }
}



