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
using Setupuk.gov.defra.sdds.automateduitests.Helper;
using TechTalk.SpecFlow;
using System.Globalization;
using Microsoft.VisualBasic;

namespace uk.gov.defra.sdds.automateduitests.Pages
{
    [Binding]

    public class HappyPathPage
    {
        private readonly IObjectContainer container;
        private static IConfiguration _config = InitConfiguration();
        private readonly SecureString _username = _config["d365CaseworkerUsername"].ToSecureString();
        private readonly SecureString _password = _config["d365CaseworkerPassword"].ToSecureString();
        private readonly SecureString _mfaSecretKey = string.Empty.ToSecureString();
        private readonly Uri _xrmUri = new Uri(_config["d365URL"]);
        private WebClient _xrmClient;
        public XrmApp _xrmApp;
        public IWebDriver win;
         
        public HappyPathPage(IObjectContainer container)
        {
            this.container = container;
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
            select.SelectByText(type);
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();
            Thread.Sleep(4000);


        }
        public void selectAstage(string stage)
        {
            
            var _browser = _xrmClient.Browser;
            win = _browser.Driver.SwitchTo().Window(_browser.Driver.CurrentWindowHandle);
            _xrmApp.BusinessProcessFlow.SelectStage(stage);
            Thread.Sleep(4000);
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_isthisannsip", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_previousdaspss", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_isthisphasedmultiplot", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_isitalivedig", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_isamainsettimpacted", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_impactondesignatedprotectedsite", Value = "Yes" });
            _xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_priority", Value = "1" });
            win.FindElement(By.Id("MscrmControls.Containers.ProcessStageControl-Next Stage")).Click();

            _xrmApp.ThinkTime(3500);
            var test = win.FindElement(By.CssSelector("*[aria-label='Allocator, Lookup']"));
            Actions action = new Actions(win);
            action.MoveToElement(test).DoubleClick().Perform();
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
            var select = new SelectElement(dropdown);
            select.SelectByText("Compliance/Enforcement investigation - notify only");
            _xrmApp.CommandBar.ClickCommand("Save & Close");
            _xrmApp.ThinkTime(500);
            win.FindElement(By.XPath("//button[@aria-label='Yes']")).Click();
        }

        public string PauseValue()
        {
            return win.FindElement(By.XPath("//span[text()='Pause']")).Text;
        }
        public void Resumeapplication()
        {
            _xrmApp.CommandBar.ClickCommand("Resume");
            win.FindElement(By.XPath("//button[@aria-label='Yes']")).Click();
            _xrmApp.CommandBar.ClickCommand("Save & Close");

        }

        public void Withdrawapplication()
        {
            _xrmApp.CommandBar.ClickCommand("Withdraw Case");
            win.FindElement(By.XPath("//button[@aria-label='Yes']")).Click();
            _xrmApp.ThinkTime(1500);
            var element = win.FindElement(By.XPath("//*[@title='New Withdraw Application']"));
            Actions actions = new Actions(win);
            actions.MoveToElement(element);
            _xrmApp.ThinkTime(1500);
            win.FindElement(By.XPath("//select[@aria-label='Reason for Withdrawing']")).Click();
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Reason for Withdrawing']"));
            var select = new SelectElement(dropdown);
            select.SelectByText("Withdrawal due to no response received from customer");
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

        public void OpenApplications()
        {

           _xrmApp.Navigation.OpenSubArea("Licence", "Applications");
        }
       
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
            Thread.Sleep(5000);
            win.FindElement(By.XPath("//button[@data-id='confirmButton']")).Click();
            Thread.Sleep(5000);
            win.FindElement(By.XPath("//button[@data-id='confirmButton']")).Click();
        }

        public void switchToReturnFirstTab()
        {
            Thread.Sleep(2000);
            win.SwitchTo().Window(win.WindowHandles[1]);
            Thread.Sleep(5000);
            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_didyoucarryouttheactions", Value = "Yes" });
            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_developmentcouldstart", Value = "Yes" });
            _xrmApp.Entity.SetValue("sdds_receiveddate", DateTime.Today, "dd/MM/yyyy", "HH:mm");

            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_didyoucompleteworkbetweenlicenseddates", Value = "Yes" });
            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_didyoucomplywithconditionsofthelicence", Value = "Yes" });

    }

        public void PopulateA24tab()
        {
            win.FindElement(By.XPath("//li[@aria-label='A24 Badger']")).Click();
            Thread.Sleep(500);
            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_obstructsettentrancesbymeansofonewaygates", Value = "Yes" });

            _xrmApp.Entity.SetValue("sdds_obstructsettentrancesonewaydescription", "test36new1");


            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_obstructaccesstosettbyblockingorproofing", Value = "Yes" });

            _xrmApp.Entity.SetValue("sdds_blockingorproofingdescription", "test36new2");

            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_damagesettbyhandsmechanicalmeans", Value = "Yes" });

            _xrmApp.Entity.SetValue("sdds_damagebyhandmechanicalmeansdescription", "test36new2");

            _xrmApp.Entity.SetValue(new OptionSet { Name = "destroyvacantsettbyhandormechanicalmeans", Value = "Yes" });

            _xrmApp.Entity.SetValue("sdds_destroysettbyhandmechanicaldecsription", "test36new3");

         
            _xrmApp.Entity.SetValue("sdds_whendidyoudestroythevacantsett", DateTime.Today.AddYears(-1), "dd/MM/yyyy");
           
            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_didyoudisturbbadgers", Value = "Yes" });

            _xrmApp.Entity.SetValue("sdds_disturbbadgerdescription", "test");

            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_didyoucreateanartificialsett", Value = "Yes" });

            _xrmApp.Entity.SetValue("sdds_artificialsettdescription", "testing");

            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_artificialsettcreatedbeforesettwasclosed", Value = "Yes" });

            _xrmApp.Entity.SetValue("sdds_evidencebadgersfoundtheartificialsett", "test36new4");

            _xrmApp.Entity.SetValue("sdds_artificialbadgersettgridreference", "test36new5");

            _xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_concernsforbadgerswelfare", Value = "Yes" });

            _xrmApp.Entity.SetValue("sdds_badgerswelfaredescription", "test36new5");
   
            win.FindElement(By.XPath("//*[@id='sdds_returnofaction|NoRelationship|Form|Mscrm.SaveAndClosePrimary10-button']")).Click();
            Thread.Sleep(2000);
            win.Navigate().Refresh();

        }


    public string GetLicenceGrantedText()
        {
            Thread.Sleep(10000);
            win.SwitchTo().Window(win.WindowHandles[2]);
            var grantedText = win.FindElement(By.XPath("//*[@data-id='header_title']")).Text;
            return grantedText;
        }

        public string GetNotGrantedText()
        {
            Thread.Sleep(3000);
            win.SwitchTo().Window(win.WindowHandles[1]);
            var grantedText = win.FindElement(By.XPath("//*[@data-id='header_title']")).Text;
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
            _xrmApp.ThinkTime(7000);
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
        public void AddReceivedDate()




        {
            _xrmApp.Entity.SetValue("sdds_applicationformreceiveddate", DateTime.Today, "dd/MM/yyyy", "HH:mm");
      
        }

        public void ClickOnSameAsApplicant()
        {
     
            win.FindElement(By.XPath("//button[@data-id='sdds_applicantthesameasbillingcustomer.fieldControl-option-set-select']")).Click();
            win.FindElement(By.XPath("//*[@data-id='sdds_applicantthesameasbillingcustomer.fieldControl-option-set-select' and @title='No']")).Click();

        }


        public void ClickonRiskSensitivity()
        {
            Thread.Sleep(6000);
            Actions action = new Actions(win);
            var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            var actionA = win.FindElement(By.XPath("//div[@title='Risk/Sensitivity']"));
            win.FindElement(By.XPath("//div[@title='Risk/Sensitivity']")).Click();
            Thread.Sleep(4000);
            action.MoveToElement(actionA).DoubleClick().Perform();
            Thread.Sleep(5000);

            win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']")).Click();
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
            win.FindElement(By.XPath("//input[@data-id='sdds_licencestartdate.fieldControl-date-time-input']")).SendKeys(today.ToString("dd/MM/yyyy"));
            Thread.Sleep(2000);
            _xrmApp.Entity.SetValue("sdds_licenceenddate", DateTime.Today.AddYears(1), "dd/MM/yyyy");

        }
        public void ClickonBackgroundInformation()
        {
            Thread.Sleep(5000);

            Actions action = new Actions(win);
            var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            var actionA = win.FindElement(By.XPath("//div[@title='Background information']"));
            win.FindElement(By.XPath("//div[@title='Background information']")).Click();

            Thread.Sleep(4000);
            action.MoveToElement(actionA).DoubleClick().Perform();
            Thread.Sleep(4000);

            win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']")).Click();
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']"));
            var select = new SelectElement(dropdown);
            Thread.Sleep(4000);
            select.SelectByText("Satisfied");
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();

        }

        public void ClickonSurvey()
        {

            Thread.Sleep(5000);
            Actions action = new Actions(win);
            var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            var actionA = win.FindElement(By.XPath("//div[@title='Survey effort, methods, mitigation and timing']"));
            win.FindElement(By.XPath("//div[@title='Survey effort, methods, mitigation and timing']")).Click();
            Thread.Sleep(5000);
            action.MoveToElement(actionA).DoubleClick().Perform();       
            Thread.Sleep(5000);
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']"));
            var select = new SelectElement(dropdown);
            Thread.Sleep(4000);
            select.SelectByText("Satisfied");
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();


        }

        public void ClickonConservation()
        {
            Thread.Sleep(6000);
            Actions action = new Actions(win);
            var actionB = win.FindElement(By.XPath("//*[@wj-part='root']"));

            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", actionB);
            var actionA = win.FindElement(By.XPath("//div[@title='Conservation considerations outcome']"));
            win.FindElement(By.XPath("//div[@title='Conservation considerations outcome']")).Click();

            Thread.Sleep(5000);
            action.MoveToElement(actionA).DoubleClick().Perform();
            Thread.Sleep(5000);
            var dropdown = win.FindElement(By.XPath("//select[@aria-label='Mark as Satisfied/Not-satisfied']"));
            var select = new SelectElement(dropdown);
            Thread.Sleep(4000);
            select.SelectByText("Satisfied");
            win.FindElement(By.XPath("//button[@aria-label='Mark Complete']")).Click();
         
        }


        public void ClickonNewDesignatedSite()
        {

            win.FindElement(By.XPath("//*[text()='New Designated Site']")).Click();

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
        }

        public void ClickonQuickSaveAndContinue()
        {
            win.FindElement(By.XPath("//*[@data-id='quickCreateSaveAndCloseBtn']")).Click();
        }
        public void Clickonmic()
        {
            win.FindElement(By.XPath("//button[@aria-label = 'Miscellaneous More Commands']")).Click();
        }
        public void Clickonlicevcegenerate()
        {
            win.FindElement(By.XPath("//*[text()='Generate Document']")).Click();
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
            _xrmApp.ThinkTime(1000);
            var news = win.FindElement(By.XPath("//li[@data-id='MscrmControls.FieldControls.SimpleLookupControl-LookupResultsPopup_falseBoundLookup_resultsContainer'][1]"));
            _xrmApp.ThinkTime(500);
            actions.MoveToElement(news).DoubleClick().Perform();
            win.FindElement(By.XPath("//*[text()='Add']")).Click();

        }

        public void ClickOnAddReturnOfAction()
        {
            _xrmApp.Entity.SelectTab("Decision");
            win.FindElement(By.XPath("//*[@data-id='Subgrid_11-pcf_grid_control_container']/div/div[1]/div/div/div/div/div[1]/div[2]/div[3]/div[2]/div/div/div/div[2]/div/div/div/div/div[1]/div/a/div/span")).Click();
            _xrmApp.ThinkTime(2000);
            win.FindElement(By.CssSelector("li[aria-label='Report of Action']")).Click();
            Actions actions = new Actions(win);
            _xrmApp.ThinkTime(2000);
            IWebElement newone = win.FindElement(By.XPath("//Section[@aria-label='Returns']"));
            ((IJavaScriptExecutor)win).ExecuteScript("arguments[0].scrollIntoView(true);", newone);
            var button = win.FindElement(By.XPath("//*[text()='New Report of Action']"));
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
        //[AfterScenario]
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



