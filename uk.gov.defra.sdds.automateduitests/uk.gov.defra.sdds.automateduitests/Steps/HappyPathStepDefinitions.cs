using System;
using TechTalk.SpecFlow;
using uk.gov.defra.sdds.automateduitests.Setup;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.gov.defra.sdds.automateduitests.Pages;

namespace uk.gov.defra.sdds.automateduitests.Steps

{
    [Binding]
    public class HappyPathStepDefinitions
    {
        HappyPathPage _happyPathPage;
        Hooks _hooks;
        public HappyPathStepDefinitions(HappyPathPage happyPathPage, Hooks hooks)
        {
            _hooks = hooks;
            _happyPathPage = happyPathPage;
        }

        [When(@"I navigate to Licence Application and select Application")]
        public void WhenINavigateToLicenceApplicationAndSelectApplication()

        {
            _happyPathPage.OpenLicenceApp();
            _happyPathPage.OpenApplications();
        }


        [Given(@"I will populate the conservation section")]
        public void GivenIWillPopulateTheConservationSession()
        {

            _happyPathPage._xrmApp.ThinkTime(6000);
            _happyPathPage._xrmApp.Entity.SelectTab("Conservation considerations");
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_onnexttodesignatedsite", Value = "Yes" });
            _happyPathPage.ClickonSaveandContinue();
            _happyPathPage._xrmApp.Entity.SetValue("sdds_licencedecisionandjustification", "test36new");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _happyPathPage.ClickonSaveandClose();

            _happyPathPage._xrmApp.ThinkTime(4000);
            _happyPathPage._xrmApp.Entity.SelectTab("Conservation considerations");
            _happyPathPage.ClickonNewDesignatedSite();
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_onnexttonear", Value = "Next to" });

            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_designatedsitenameid", Value = "Abberton Reservoir", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_permissionacquired", Value = true });
            _happyPathPage._xrmApp.Entity.SetValue("sdds_detailsofpermission", "test");
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_permissionfortheactivityfromne", Value = "Yes" });
            _happyPathPage._xrmApp.Entity.SetValue("sdds_neadvicefrom", "testnewnw");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_outcomeoftheadvisefromne", "testtesttetststtt");
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage.ClickonSave();
            _happyPathPage.ClickonSaveandClose();

        }

        [Given(@"the User is logged into Licence power app")]
        public void GivenTheUserIsLoggedIntoLicencePowerApp()
        {
            _happyPathPage.LaunchCRMApplication();
        }

        [Given(@"I go to Applications home screen")]
        public void GivenIGoToApplicationsHomeScreen()
        {
            _happyPathPage.OpenLicenceApp();
            _happyPathPage.OpenApplications();
        }

        [Given(@"I click on New \(plus sign\) to add a new application")]
        public void GivenIClickOnNewPlusSignToAddANewApplication()
        {
            _happyPathPage._xrmApp.CommandBar.ClickCommand("New");
        }

        [Given(@"I will populate the remaining ""([^""]*)"" Tab details")]
        public void GivenIGoToTab(string general)
        {
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicantid", Value = "AbigailPouros", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_organisationid", Value = "Total Ecology Ltd", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistid", Value = "Roger Nicholls", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistorganisationid", Value = "Total Ecology Ltd", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_billingcustomerid", Value = "UrbanStanton", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_billingorganisationid", Value = "Countryside", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeapplicantcontactid", Value = "Abagail smitham", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeecologistcontactid", Value = "Roger Nicholls", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Development", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_applicationcategory", Value = "Commercial" });
            _happyPathPage._xrmApp.Entity.SetValue("sdds_descriptionofproposal", "testtestst");
            _happyPathPage._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_issitesameasapplicants", Value = true });
            _happyPathPage._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_doestheprojectneedanypermissions", Value = true });
            _happyPathPage._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_projectpermissionsgranted", Value = true });
            _happyPathPage._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_isapplicantonwnerofland", Value = true });
            _happyPathPage._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_wildliferelatedconviction", Value = true });
            _happyPathPage._xrmApp.Entity.SetValue("sdds_detailsofconvictions", "testing 123");
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_nsiproject", Value = "No" });

        }

        [Given(@"I select Application Type as A(.*) BAdger")]
        public void GivenISelectApplicationTypeAsABAdger(int p0)
        {
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationtypesid", Value = "A24 Badger", Index = 0 });


        }

        [Given(@"I will populate and complete disease risk assessment section")]
        public void GivenIWillPopulateAndCompleteDiseaseRiskAssessmentSession()
        {
            _happyPathPage._xrmApp.Entity.SelectTab("Disease Risk Assessment");
            _happyPathPage._xrmApp.Entity.SubGrid.OpenSubGridRecord("Subgrid_16", 0);
            _happyPathPage.ClickonRiskAssessmentTasks();
            _happyPathPage._xrmApp.ThinkTime(2000);
        }

        [Given(@"I will populate and complete the ecologist experience section")]
        public void GivenIWillPopulateAndCompleteTheEcologistExperienceSession()
        {
            _happyPathPage._xrmApp.Entity.SelectTab("Experience assessment");
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_heldbadgerlicence", Value = "Yes" });
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_badgermitigationclasslicence", Value = "Yes" });
            _happyPathPage._xrmApp.Entity.SetValue("sdds_ecologistexperienceofbadgerecology", "testnewnw test 40003£$%%testnewnw");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_ecologistexperienceofmethods", "testnewnw test 40003£$%%");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_mitigationclassrefno", "test36262");
            _happyPathPage.ClickonSave();
        }

        [Given(@"I will populate and complete the technical assessment section")]
        public void GivenIWillPopulateAndCompleteTheTechnicalAssessmentSection()
        {
            _happyPathPage.SelectTechnicalAssessment();
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_licencerequiredforproposedactivity", Value = "Yes" });
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_proposedactionsproportionatescale", Value = "Yes" });
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_proposedactionscouldresolveproblem", Value = "Yes" });
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_satisfiedwithpersonsundertakingworks", Value = "Yes" });
            _happyPathPage._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _happyPathPage.ClickonRiskSensitivity();
            _happyPathPage.ClickonBackgroundInformation();
            _happyPathPage.ClickonSurvey();
            _happyPathPage.ClickonConservation();
        }

        [Given(@"I will populate and complete the decision section")]
        public void GivenIWillPopulateAndCompleteTheDecisionSection()
        {
            _happyPathPage.SelectDecision();
            _happyPathPage.ClickOnAddNewLicence();
            _happyPathPage._xrmApp.Entity.SetValue(new OptionSet { Name = "statuscode", Value = "Active" });
            _happyPathPage.ClickonSaveandClose();
        }

        [Given(@"I will complete the Assessment stage checklist with Licence granted outcome")]
        public void GivenIWillCompleteTheAssessmentStageChecklistWithLicenceGrantedOutcome()
        {
            _happyPathPage.selectnewstage("Assessment");
        }

        [Given(@"I click on Save")]
        public void GivenIClickOnSave()
        {
            _happyPathPage._xrmApp.Entity.Save();
            _happyPathPage._xrmApp.ThinkTime(3000);
        }

        [Given(@"I wiil progress from Application recieved to Assessment stage")]
        public void GivenIWiilProgressFromApplicationRecievedToAssessmentStage()
        {
            _happyPathPage.selectAstage("Application Received");
            _happyPathPage.ClickonTechnicalAssessment();
        }
        [Given(@"I will populate application details tab")]
        public void GivenIWillPopulateApplicationDetailsTab()
        {
            _happyPathPage._xrmApp.Entity.SelectTab("Application Details");
            _happyPathPage._xrmApp.ThinkTime(4000);
            _happyPathPage._xrmApp.Entity.SelectTab("Application Details");
        }
        [Given(@"I will populate an existing site and new permission")]
        public void GivenIWillPopulateAnExistingSiteAndNewPermission()
        {
            _happyPathPage.ClickOnAddExistingSite();
            _happyPathPage.ClickonNewPermission();
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage._xrmApp.QuickCreate.SetValue(new OptionSet { Name = "sdds_permissiontype", Value = "Planning permission" });
            _happyPathPage._xrmApp.QuickCreate.SetValue(new LookupItem { Name = "sdds_councilid", Value = "Aberdeen City Council", Index = 0 });
            _happyPathPage._xrmApp.QuickCreate.SetValue("sdds_refnumber", "test789");
            _happyPathPage._xrmApp.QuickCreate.SetValue(new OptionSet { Name = "sdds_planningpermissiontype", Value = "Full" });
            _happyPathPage.ClickonQuickSaveAndContinue();
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage.ClickonSave();
        }

        [Given(@"I will add an existing site")]
        public void GivenIWillAddAnExistingSite()
        {
            _happyPathPage.ClickOnAddExistingSite();
        }

        [When(@"I click on generate document")]
        public void WhenIClickOnGenerateDocument()
        {
            _happyPathPage._xrmApp.CommandBar.ClickCommand("Generate Document");
            _happyPathPage._xrmApp.ThinkTime(23000);
        }
        [Then(@"I will validate the url of the opened Licence document")]
        public void ThenIWillValidateTheUrlOfTheOpenedLicenceDocument()
        {
            _happyPathPage.switchToFirstTab();
            _happyPathPage._xrmApp.ThinkTime(1500);
            Assert.IsTrue((_happyPathPage.getcurrentUrl()).Contains("blob:https://defra-sdds-test.crm11.dynamics.com/"));

        }
        [Then(@"I will validate generate licence page")]
        public void ThenIWillValidateGenerateLicencePage()
        {
            Assert.IsTrue((_happyPathPage.GetLicenceGrantedText()).Contains("granted"));
            _happyPathPage.ClickonSaveandClose();
        }




    }
}