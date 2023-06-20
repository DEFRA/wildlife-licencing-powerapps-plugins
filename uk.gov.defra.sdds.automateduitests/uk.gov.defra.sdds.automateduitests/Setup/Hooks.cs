using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Extensions.Configuration;
using System.Security;
using uk.gov.defra.sdds.automateduitests.Setup;
using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium.Interactions;
using NUnit.Framework.Internal;
using System.ComponentModel;
using OpenQA.Selenium.Support.UI;
using System;
using NUnit.Framework;
using Setupuk.gov.defra.sdds.automateduitests.Helper;

namespace uk.gov.defra.sdds.automateduitests.Setup
{
   public class Hooks
    {
        private readonly IObjectContainer container;
        private static IConfiguration _config = InitConfiguration();
        private readonly SecureString _username = _config["d365CaseworkerUsername"].ToSecureString();
        private readonly SecureString _password = _config["d365CaseworkerPassword"].ToSecureString();
        private readonly SecureString _mfaSecretKey = string.Empty.ToSecureString();
        private readonly Uri _xrmUri = new Uri(_config["d365URL"]);
        private WebClient _xrmClient;   
        public XrmApp _xrmApp;
        public IWebDriver _driver;
        public IWebDriver win;
        public Hooks(IObjectContainer container) 
        {
            this.container = container;
        
        }
        //public void brower()
        //{
        //    _xrmClient  = new WebClient(TestSettings.Options);
        //    _xrmApp = new XrmApp(_xrmClient);
        //    container.RegisterInstanceAs(_xrmApp);





        //}
        public void LaunchCRMApplication()
        {
        
            {
               
                _xrmClient = new WebClient(TestSettings.Options);
                _xrmApp = new XrmApp(_xrmClient);
                container.RegisterInstanceAs(_xrmApp);
                //container.RegisterInstanceAs(_driver);

                _xrmApp.OnlineLogin.Login(_xrmUri, _username, _password);
                //_driver.FindElement(By.CssSelector("#passwordInput")).SendKeys("jtel^U0T&s534USw");


            }
        }

        public void clickOnNextstage()
        {
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();
        }
        public void selectnewstage(string stage)
        {
            //var _browser = _xrmClient.Browser;
            //_browser.Driver.SwitchTo().Window(_browser.Driver.CurrentWindowHandle);
            Actions actions = new Actions(win);
            _xrmApp.ThinkTime(3000);
            var element = win.FindElement(By.XPath("//div[text()='Assessment ']"));
            actions.MoveToElement(element).Click().Perform();
            //win(By.XPath("//*[text()='Assessment ']").Click
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_licencerequiredforproposedactivity", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_proposedactionsproportionatescale", Value = "Yes" });

            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_proposedactionscouldresolveproblem", Value = "Yes" });

            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_satisfiedwithpersonsundertakingworks", Value = "Yes" });
           
            var text = win.FindElement(By.XPath("//*[@aria-label='Licence decision and justification']"));

            actions.MoveToElement(text).DoubleClick();

            Thread.Sleep(500);
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Assessment Outcome']"));
            var select = new SelectElement(dropdown);
            Thread.Sleep(4000);
            select.SelectByText("Grant");
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();

          
        }
        public void selectAstage(string stage)
        {
            var _browser = _xrmClient.Browser;
             win = _browser.Driver.SwitchTo().Window(_browser.Driver.CurrentWindowHandle);

            _xrmApp.BusinessProcessFlow.SelectStage(stage);
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();
    
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_isthisannsip", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_previousdaspss", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_isthisphasedmultiplot", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_isitalivedig", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_isamainsettimpacted", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_impactondesignatedprotectedsite", Value = "Yes" });
           
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_priority", Value = "1" });

            var test = win.FindElement(By.CssSelector("*[aria-label='Allocator, Lookup']"));
            Actions action = new Actions(win);
            action.MoveToElement(test).DoubleClick().Perform();
            _xrmApp.ThinkTime(1000);
            win.FindElement(By.XPath("//*[@aria-label='Search records for Allocator, Lookup field']")).Click();

            _xrmApp.ThinkTime(5000);

            win.FindElement(By.XPath("//*[text()= 'Fin Rylatt']")).Click();
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();
            _xrmApp.ThinkTime(5000);

            win.FindElement(By.CssSelector("*[aria-label='Lead adviser, Lookup']")).Click();
            win.FindElement(By.XPath("//*[@aria-label='Search records for Lead adviser, Lookup field']")).Click();
            _xrmApp.ThinkTime(1000);

            win.FindElement(By.XPath("//*[text()= '# Laura Alvey (NE)']")).Click();
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();





        }

        //public void ClickonAddAuthotisePerson()
        //{
        //var _browser = _xrmClient.Browser;
        //win = _browser.Driver.SwitchTo().Window(_browser.Driver.CurrentWindowHandle);
        //xrmApp.ThinkTime(2000);


        //    IWebElement more = win.FindElement(By.XPath("//*[text()= 'Add Existing Contact']"));
        //    more.Click();
        //    win.FindElement(By.XPath("//*[text() ='Add Existing Contact']")).Click();
        //}

        public void ClickonSave()
        {
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//*[text()='Save']")).Click();
            Thread.Sleep(2000);
        }
        public void ClickonSaveandClose()
        {

            win.FindElement(By.XPath("//*[text()='Save & Close']")).Click();
        }
        public string getcurrentUrl()
        {
            Thread.Sleep(2000);
            win.WindowHandles.Count();
            win.SwitchTo().Window(win.WindowHandles[1]);
            var url = win.Url;
            return url;
        }
        public void switchToFirstTab()
        {
            Thread.Sleep(5000);
            win.SwitchTo().Window(win.WindowHandles[0]);
            Thread.Sleep(3000);
            win.FindElement(By.XPath("//button[@data-id='confirmButton']")).Click();
            Thread.Sleep(2000);
            win.FindElement(By.XPath("//button[@data-id='confirmButton']")).Click();
        }
        public string GetLicenceGrantedText()
        {
            Thread.Sleep(7000);
            win.SwitchTo().Window(win.WindowHandles[2]);
            var grantedText =  win.FindElement(By.XPath("//*[@data-id='header_title']")).Text;
            return grantedText;
        }

        public string GrantedText()
        {
            Thread.Sleep(7000);
            win.SwitchTo().Window(win.WindowHandles[0]);
            var grantedText = win.FindElement(By.XPath("//*[@data-id='header_title']")).Text;
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
            _xrmApp.ThinkTime(4000);
            win.FindElement(By.CssSelector("span[data-id='warningNotification']")).Click();

        }

        public void ClickonRiskAssessmentTasks()
        {
           
            Actions action = new Actions(win);
            var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));
             ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);

            var actionA = win.FindElement(By.XPath("//div[@aria-label='Risk Assessment Tasks']"));
            action.MoveToElement(actionA).DoubleClick().Perform();
            Thread.Sleep(4000);
            win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']")).Click();
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']"));
            var select = new SelectElement(dropdown);
            Thread.Sleep(4000);
            select.SelectByText("Satisfied");
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();
                    

        }

        public void ClickonLicensableactions()
        {
            Actions action = new Actions(win);
             var actionB = win.FindElement(By.XPath("//div[@aria-label='Technical Assessment Tasks']"));

            action.MoveToElement(actionB).DoubleClick().Perform();
            Thread.Sleep(4000);
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();


        }

        public void ClickonRiskSensitivity()
        {
            var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));
             ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            Actions action = new Actions(win);
            var actionA = win.FindElement(By.XPath("//span[text()='Risk/Sensitivity']"));
            Thread.Sleep(3000);
            action.MoveToElement(actionA).Click().Perform();
            win.FindElement(By.XPath("//button[@title='Navigate to Risk/Sensitivity']")).Click();
            Thread.Sleep(5000);
           
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']"));
            var select = new SelectElement(dropdown);
            Thread.Sleep(4000);
            select.SelectByText("Satisfied");
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();


        }

        public void SelectDecision()
        {
            Thread.Sleep(4500);

            win.FindElement(By.CssSelector("li[aria-label='More Tabs']")).Click();
            win.FindElement(By.XPath("//*[text()='Decision']")).Click();
        }
        public void ClickOnAddNewLicence()
        {
            Thread.Sleep(4000);

            win.FindElement(By.XPath("//*[text()='Generate Licence']")).Click();
            Thread.Sleep(4000);
            DateTime day = DateTime.Now;

            var today = DateOnly.FromDateTime(day);
            var nextYear = DateOnly.FromDateTime(day.AddYears(1));
            win.FindElement(By.XPath("//input[@data-id='sdds_licencestartdate.fieldControl-date-time-input']")).SendKeys((today).ToString("dd/MM/yyyy"));

            win.FindElement(By.XPath("//input[@data-id='sdds_licenceenddate.fieldControl-date-time-input']")).SendKeys((nextYear).ToString("dd/MM/yyyy"));
         
        }
        public void ClickonBackgroundInformation()
        {
            Thread.Sleep(8000);
            var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));
            var actionA = win.FindElement(By.XPath("//span[text()='Background information']"));
            Actions action = new Actions(win);
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            action.MoveToElement(actionA).DoubleClick().Perform();
            win.FindElement(By.XPath("//button[@title='Navigate to Background information']")).Click();
           
            Thread.Sleep(4000);
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']"));
            var select = new SelectElement(dropdown);
            Thread.Sleep(4000);
            select.SelectByText("Satisfied");
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();


        }

        public void ClickonSurvey()
        {
            
            Thread.Sleep(5500);

            var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));
            var actionA = win.FindElement(By.XPath("//span[text()='Survey effort, methods, mitigation and timing']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            Actions action = new Actions(win);
            action.MoveToElement(actionA).DoubleClick().Perform();
            win.FindElement(By.XPath("//button[@title='Navigate to Survey effort, methods, mitigation and timing']")).Click();

            Thread.Sleep(5000);
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']"));
            var select = new SelectElement(dropdown);
            Thread.Sleep(4000);
            select.SelectByText("Satisfied");
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();


        }

        public void ClickonConservation()
        {
            Thread.Sleep(6500);
             var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));
            var actionA = win.FindElement(By.XPath("//span[text()='Conservation considerations outcome']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            Actions action = new Actions(win);
            action.MoveToElement(actionA).Click().Perform();
            win.FindElement(By.XPath("//button[@title='Navigate to Conservation considerations outcome']")).Click();

            Thread.Sleep(5000);
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']"));
            var select = new SelectElement(dropdown);
            Thread.Sleep(4000);
            select.SelectByText("Satisfied");
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();
            Thread.Sleep(2500);


        }


        public void ClickonNewDesignatedSite()
        {
            
            win.FindElement(By.XPath("//*[text()='New Designated Site']")).Click();

        }
        public void ClickonTechnicalAssessment()
        {
            Thread.Sleep(5000);
          
            Actions action = new Actions(win);
            var element = win.FindElement(By.XPath("//*[text()='Save']"));
            action.MoveToElement(element).DoubleClick().Perform();
            Thread.Sleep(2000);
          
       



        }
        public void SelectTechnicalAssessment()
        {
            _xrmApp.ThinkTime(2500);
            win.FindElement(By.CssSelector("li[aria-label='More Tabs']")).Click();
            win.FindElement(By.XPath("//*[text()='Technical Assessment']")).Click();
        }

            public void ClickonQuickSaveAndContinue()
        {
            win.FindElement(By.XPath("//*[@data-id='quickCreateSaveAndCloseBtn']")).Click();
        }
        public void ClickonNewPermission()
        {
            win.FindElement(By.XPath("//*[text()='New Permission']")).Click();
        }
        public void ClickOnAddExistingSite()
        {
            Actions actions = new Actions(win);

            _xrmApp.ThinkTime(1000);
            win.FindElement(By.XPath("//*[text()= 'Add Existing Site']")).Click();
            _xrmApp.ThinkTime(500);
            win.FindElement(By.CssSelector("button[data-id='MscrmControls.FieldControls.SimpleLookupControl-LookupResultsPopup_falseBoundLookup_search']")).Click();
            _xrmApp.ThinkTime(2000);

            var news =  win.FindElement(By.XPath("//li[@aria-label='Applicant rad site, 27/04/2023 12:34']"));
            _xrmApp.ThinkTime(500);
            actions.MoveToElement(news).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[text()='Add']")).Click();

        }
        public void enterData(string stage) {

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
[AfterScenario] 
        public void CloseApp() 
        
        {
            if (_xrmApp != null) 
            {
                _xrmApp.Dispose();  
            }
        }
        public static IConfiguration InitConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }
    }
}



