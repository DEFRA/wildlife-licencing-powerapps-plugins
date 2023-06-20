using System;
using TechTalk.SpecFlow;
using uk.gov.defra.sdds.automateduitests.Setup;
using uk.gov.defra.sdds.automateduitests.Pages;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace uk.gov.defra.sdds.automateduitests.Steps

{
    [Binding]
    public class HappyPathStepDefinitions
    {
        Hooks _hooks;
        HappyPathPage _helloPage;
        public HappyPathStepDefinitions(Hooks hooks, HappyPathPage helloPage)
        {
            _hooks = hooks;
            _helloPage = helloPage;
        }
        [Given(@"I login to CRM")]
        public void GivenILoginToCRM()
        {
            _hooks.LaunchCRMApplication();
        }
        [When(@"I navigate to Licence Application and select Application")]
        public void WhenINavigateToLicenceApplicationAndSelectApplication()

        {
            _helloPage.OpenLicenceApp();
            _helloPage.OpenApplications();
        }

        [Then(@"I click on New command")]
        public void ThenIClickOnNewCommand()
        {
            _hooks._xrmApp.CommandBar.ClickCommand("New");
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationtypesid", Value = "A24 Badger", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicantid", Value = "AbigailPouros", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_organisationid", Value = "Total Ecology Ltd", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistid", Value = "Roger Nicholls", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistorganisationid", Value = "Total Ecology Ltd", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_billingcustomerid", Value = "UrbanStanton", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_billingorganisationid", Value = "Countryside", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeapplicantcontactid", Value = "Abagail smitham", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeecologistcontactid", Value = "Roger Nicholls", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Development", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_applicationcategory", Value = "Commercial" });
            _hooks._xrmApp.Entity.SetValue("sdds_descriptionofproposal", "testtestst");
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_issitesameasapplicants", Value = true });
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_doestheprojectneedanypermissions", Value = true });
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_projectpermissionsgranted", Value = true });
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_isapplicantonwnerofland", Value = true });







            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_wildliferelatedconviction", Value = true });
            _hooks._xrmApp.Entity.SetValue("sdds_detailsofconvictions", "testing 123");
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_nsiproject", Value = "No" });
            _hooks._xrmApp.Entity.Save();

            _hooks._xrmApp.ThinkTime(3000);
            //   _hooks.ClickonAddAuthotisePerson();
            _hooks.selectAstage("Application Received");

            _hooks.ClickonTechnicalAssessment();
            _hooks._xrmApp.Entity.SelectTab("Application Details");
            _hooks._xrmApp.ThinkTime(4000);




            _hooks._xrmApp.Entity.SelectTab("Application Details");
            _hooks.ClickOnAddExistingSite();

            _hooks.ClickonNewPermission();
            _hooks._xrmApp.ThinkTime(2000);

            _hooks._xrmApp.QuickCreate.SetValue(new OptionSet { Name = "sdds_permissiontype", Value = "Planning permission" });
            _hooks._xrmApp.QuickCreate.SetValue(new LookupItem { Name = "sdds_councilid", Value = "Aberdeen City Council", Index = 0 });
            _hooks._xrmApp.QuickCreate.SetValue("sdds_refnumber", "test789");
            _hooks._xrmApp.QuickCreate.SetValue(new OptionSet { Name = "sdds_planningpermissiontype", Value = "Full" });
            _hooks.ClickonQuickSaveAndContinue();
            _hooks._xrmApp.ThinkTime(2000);
            _hooks.ClickonSave();



            _hooks._xrmApp.ThinkTime(4000);
            _hooks._xrmApp.Entity.SelectTab("Conservation considerations");
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_onnexttodesignatedsite", Value = "Yes" });
            _hooks.ClickonSaveandContinue();
            _hooks._xrmApp.Entity.SetValue("sdds_licencedecisionandjustification", "test36new");
            _hooks._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _hooks.ClickonSaveandClose();

            _hooks._xrmApp.ThinkTime(4000);
            _hooks._xrmApp.Entity.SelectTab("Conservation considerations");
            _hooks.ClickonNewDesignatedSite();
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_onnexttonear", Value = "Next to" });

            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_designatedsitenameid", Value = "Abberton Reservoir", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_permissionacquired", Value = true });
            _hooks._xrmApp.Entity.SetValue("sdds_detailsofpermission", "test");
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_permissionfortheactivityfromne", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue("sdds_neadvicefrom", "testnewnw");
            _hooks._xrmApp.Entity.SetValue("sdds_outcomeoftheadvisefromne", "testtesttetststtt");
            _hooks._xrmApp.ThinkTime(2000);
            _hooks.ClickonSave();
            _hooks.ClickonSaveandClose();

            _hooks._xrmApp.Entity.SelectTab("Disease Risk Assessment");
            _hooks._xrmApp.Entity.SubGrid.OpenSubGridRecord("Subgrid_16", 0);
            _hooks.ClickonRiskAssessmentTasks();
            _hooks._xrmApp.ThinkTime(2000);

            _hooks._xrmApp.Entity.SelectTab("Experience assessment");
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_heldbadgerlicence", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_badgermitigationclasslicence", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue("sdds_ecologistexperienceofbadgerecology", "testnewnw test 40003£$%%testnewnw");
            _hooks._xrmApp.Entity.SetValue("sdds_ecologistexperienceofmethods", "testnewnw test 40003£$%%");
            _hooks._xrmApp.Entity.SetValue("sdds_mitigationclassrefno", "test36262");
            _hooks.ClickonSave();

            _hooks.SelectTechnicalAssessment();
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_licencerequiredforproposedactivity", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_proposedactionsproportionatescale", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_proposedactionscouldresolveproblem", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_satisfiedwithpersonsundertakingworks", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");







            _hooks.ClickonRiskSensitivity();

            _hooks.ClickonBackgroundInformation();
            _hooks.ClickonSurvey();
            _hooks.ClickonConservation();
            _hooks.SelectDecision();
            _hooks.ClickOnAddNewLicence();
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "statuscode", Value = "Active" });
            _hooks.ClickonSaveandClose();

            _hooks.selectnewstage("Assessment");

            _hooks._xrmApp.CommandBar.ClickCommand("Generate Document");

            _hooks._xrmApp.ThinkTime(23000);


            _hooks.switchToFirstTab();
            //var licenceNumbers = _hooks.GrantedText().Length;
            //Console.WriteLine(licenceNumbers);
            //var licenceNumber = _hooks.GrantedText().ToString().Remove(16, 25);
            _hooks._xrmApp.ThinkTime(1500);
            Assert.IsTrue((_hooks.getcurrentUrl()).Contains("blob:https://defra-sdds-test.crm11.dynamics.com/"));
            Assert.IsTrue((_hooks.GetLicenceGrantedText()).Contains("granted"));
            //Assert.IsTrue((_hooks.GetLicenceGrantedText()).Contains("Application - " + licenceNumber + "LIC" + ",2 development site granted"));
            //"Application -" + licenceNumber + ", granted" 
            _hooks.ClickonSaveandClose();
            //_hooks.closetab();

            //Assert.AreEqual(_hooks.ClickonAcceptLicence(), "https://tfgm.com/public-transport/park-and-ride#main");

            //_hooks._xrmApp.ThinkTime(2000);
            //_hooks.ClickonAcceptLicence();
            //_hooks.ClickonSaveandClose();
            // _hooks.selectAstage("Assessment");
            //_hooks.selectnewstage("Assessment");
            //_hooks._xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_licencerequiredforproposedactivity", Value = "Yes" });
            //_hooks._xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_proposedactionsproportionatescale", Value = "Yes" });

            //_hooks._xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_proposedactionscouldresolveproblem", Value = "Yes" });

            //_hooks._xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_satisfiedwithpersonsundertakingworks", Value = "Yes" });

            //_hooks._xrmApp.BusinessProcessFlow.SetValue("header_process_sdds_furtherassessmentinformation",  "Not applicable");

            //_hooks._xrmApp.BusinessProcessFlow.SetValue( "header_process_sdds_licencedecisionandjustification", "New Licenece" );
            //_hooks._xrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = "header_process_sdds_assessmentoutcome", Value = "Granted" });
            //_hooks.clickOnNextstage();

            //_hooks._xrmApp.Entity.SubGrid.ClickSubgridSelectAll("Not Satisfied", 1000);
            //_hooks._xrmApp.Entity.OpenRecordSetNavigator(0); 

            //_hooks._xrmApp.RelatedGrid.ClickCommand("Not Satisfied");
            //_hooks._xrmApp.Entity.SubGrid.ClickSubgridSelectAll("Subgrid_16", 0);

            // Reference schema name of the SubGrid
            //_hooks._xrmApp.Entity.SubGrid.OpenSubGridRecord("Subgrid_16", 0);
            //_hooks._xrmApp.Entity.RelatedGrid.ClickCommand("New Designated Site");
            //_hooks._xrmApp.RelatedGrid.ClickCommand("New Designated Site");

        }
        [Given(@"I will populate the conservation section")]
        public void GivenIWillPopulateTheConservationSession()
        {

            _hooks._xrmApp.ThinkTime(4000);
            _hooks._xrmApp.Entity.SelectTab("Conservation considerations");
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_onnexttodesignatedsite", Value = "Yes" });
            _hooks.ClickonSaveandContinue();
            _hooks._xrmApp.Entity.SetValue("sdds_licencedecisionandjustification", "test36new");
            _hooks._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _hooks.ClickonSaveandClose();

            _hooks._xrmApp.ThinkTime(4000);
            _hooks._xrmApp.Entity.SelectTab("Conservation considerations");
            _hooks.ClickonNewDesignatedSite();
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_onnexttonear", Value = "Next to" });

            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_designatedsitenameid", Value = "Abberton Reservoir", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_permissionacquired", Value = true });
            _hooks._xrmApp.Entity.SetValue("sdds_detailsofpermission", "test");
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_permissionfortheactivityfromne", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue("sdds_neadvicefrom", "testnewnw");
            _hooks._xrmApp.Entity.SetValue("sdds_outcomeoftheadvisefromne", "testtesttetststtt");
            _hooks._xrmApp.ThinkTime(2000);
            _hooks.ClickonSave();
            _hooks.ClickonSaveandClose();

        }

        [Given(@"the User is logged into Licence power app")]
            public void GivenTheUserIsLoggedIntoLicencePowerApp()
            {
            _hooks.LaunchCRMApplication();
            }

            [Given(@"I go to Applications home screen")]
            public void GivenIGoToApplicationsHomeScreen()
            {
            _helloPage.OpenLicenceApp();
            _helloPage.OpenApplications();
        }

            [Given(@"I click on New \(plus sign\) to add a new application")]
            public void GivenIClickOnNewPlusSignToAddANewApplication()
            {
            _hooks._xrmApp.CommandBar.ClickCommand("New");
        }

        [Given(@"I will populate the remaining ""([^""]*)"" Tab details")]
        public void GivenIGoToTab(string general)
        {
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicantid", Value = "AbigailPouros", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_organisationid", Value = "Total Ecology Ltd", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistid", Value = "Roger Nicholls", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistorganisationid", Value = "Total Ecology Ltd", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_billingcustomerid", Value = "UrbanStanton", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_billingorganisationid", Value = "Countryside", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeapplicantcontactid", Value = "Abagail smitham", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeecologistcontactid", Value = "Roger Nicholls", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Development", Index = 0 });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_applicationcategory", Value = "Commercial" });
            _hooks._xrmApp.Entity.SetValue("sdds_descriptionofproposal", "testtestst");
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_issitesameasapplicants", Value = true });
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_doestheprojectneedanypermissions", Value = true });
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_projectpermissionsgranted", Value = true });
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_isapplicantonwnerofland", Value = true });
            _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_wildliferelatedconviction", Value = true });
            _hooks._xrmApp.Entity.SetValue("sdds_detailsofconvictions", "testing 123");
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_nsiproject", Value = "No" });
            
        }

        [Given(@"I select Application Type as A(.*) BAdger")]
            public void GivenISelectApplicationTypeAsABAdger(int p0)
            {
            _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationtypesid", Value = "A24 Badger", Index = 0 });
            

        }

        //[Given(@"I go to Applicant Details information section")]
        //    public void GivenIGoToApplicantDetailsInformationSection()
        //    {
        //        throw new PendingStepException();
        //    }

        //    [Given(@"I slect an  Applicant information if available by search functionality  or create an new Applicant by entering the details requested")]
        //    public void GivenISlectAnApplicantInformationIfAvailableBySearchFunctionalityOrCreateAnNewApplicantByEnteringTheDetailsRequested()
        //    {
        //    _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicantid", Value = "AbigailPouros", Index = 0 });
        //    _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_organisationid", Value = "Total Ecology Ltd", Index = 0 });
        //    _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistid", Value = "Roger Nicholls", Index = 0 });
        //    _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistorganisationid", Value = "Total Ecology Ltd", Index = 0 });
        //    _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_billingcustomerid", Value = "UrbanStanton", Index = 0 });
        //    _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_billingorganisationid", Value = "Countryside", Index = 0 });
        //    _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeapplicantcontactid", Value = "Abagail smitham", Index = 0 });
        //    _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeecologistcontactid", Value = "Roger Nicholls", Index = 0 });
        //    _hooks._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Development", Index = 0 });
        //    _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_applicationcategory", Value = "Commercial" });
        //    _hooks._xrmApp.Entity.SetValue("sdds_descriptionofproposal", "testtestst");
        //    _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_issitesameasapplicants", Value = true });
        //    _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_doestheprojectneedanypermissions", Value = true });
        //    _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_projectpermissionsgranted", Value = true });
        //    _hooks._xrmApp.Entity.SetValue(new BooleanItem { Name = "sdds_isapplicantonwnerofland", Value = true });
        //}

        //    [Given(@"I slect an  Applicant organisation if available by search  functionality or create an new Applicant organization by entering the details requested")]
        //    public void GivenISlectAnApplicantOrganisationIfAvailableBySearchFunctionalityOrCreateAnNewApplicantOrganizationByEnteringTheDetailsRequested()
        //    {
        //        throw new PendingStepException();
        //    }

        //    [Given(@"I go to Ecologist details information section")]
        //    public void GivenIGoToEcologistDetailsInformationSection()
        //    {
        //        throw new PendingStepException();
        //    }

        //    [Given(@"I slect an  Ecologist information if available by search functionality  or create an new Ecologist by entering the details requested")]
        //    public void GivenISlectAnEcologistInformationIfAvailableBySearchFunctionalityOrCreateAnNewEcologistByEnteringTheDetailsRequested()
        //    {
        //        throw new PendingStepException();
        //    }

        //    [Given(@"I slect an  Ecologist organisation if available by search  functionality or create an new Ecologist organization by entering the details requested")]
        //    public void GivenISlectAnEcologistOrganisationIfAvailableBySearchFunctionalityOrCreateAnNewEcologistOrganizationByEnteringTheDetailsRequested()
        //    {
        //        throw new PendingStepException();
        //    }
        [Given(@"I will populate and complete disease risk assessment section")]
        public void GivenIWillPopulateAndCompleteDiseaseRiskAssessmentSession()
        {
            _hooks._xrmApp.Entity.SelectTab("Disease Risk Assessment");
            _hooks._xrmApp.Entity.SubGrid.OpenSubGridRecord("Subgrid_16", 0);
            _hooks.ClickonRiskAssessmentTasks();
            _hooks._xrmApp.ThinkTime(2000);
        }

        [Given(@"I will populate and complete the ecologist experience section")]
        public void GivenIWillPopulateAndCompleteTheEcologistExperienceSession()
        {
            _hooks._xrmApp.Entity.SelectTab("Experience assessment");
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_heldbadgerlicence", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_badgermitigationclasslicence", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue("sdds_ecologistexperienceofbadgerecology", "testnewnw test 40003£$%%testnewnw");
            _hooks._xrmApp.Entity.SetValue("sdds_ecologistexperienceofmethods", "testnewnw test 40003£$%%");
            _hooks._xrmApp.Entity.SetValue("sdds_mitigationclassrefno", "test36262");
            _hooks.ClickonSave();
        }

        [Given(@"I will populate and complete the technical assessment section")]
        public void GivenIWillPopulateAndCompleteTheTechnicalAssessmentSection()
        {
            _hooks.SelectTechnicalAssessment();
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_licencerequiredforproposedactivity", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_proposedactionsproportionatescale", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_proposedactionscouldresolveproblem", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "sdds_satisfiedwithpersonsundertakingworks", Value = "Yes" });
            _hooks._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _hooks.ClickonRiskSensitivity();
            _hooks.ClickonBackgroundInformation();
            _hooks.ClickonSurvey();
            _hooks.ClickonConservation();
        }

        [Given(@"I will populate and complete the decision section")]
        public void GivenIWillPopulateAndCompleteTheDecisionSection()
        {
            _hooks.SelectDecision();
            _hooks.ClickOnAddNewLicence();
            _hooks._xrmApp.Entity.SetValue(new OptionSet { Name = "statuscode", Value = "Active" });
            _hooks.ClickonSaveandClose();
        }

        [Given(@"I will complete the Assessment stage checklist with Licence granted outcome")]
        public void GivenIWillCompleteTheAssessmentStageChecklistWithLicenceGrantedOutcome()
        {
            _hooks.selectnewstage("Assessment");
        }


        [Given(@"for ""([^""]*)"" field I select Development")]
            public void GivenForFieldISelectDevelopment(string p0)
            {
                throw new PendingStepException();
            }

            [Given(@"for ""([^""]*)"" field I select option (.*) from drop down list available")]
            public void GivenForFieldISelectOptionFromDropDownListAvailable(string p0, int p1)
            {
                throw new PendingStepException();
            }

            [Given(@"I go to Invoice Details information section")]
            public void GivenIGoToInvoiceDetailsInformationSection()
            {
                throw new PendingStepException();
            }

            [Given(@"I slect an Billing Customer if available by search functionality  or create an new Billing customer by entering the details requested")]
            public void GivenISlectAnBillingCustomerIfAvailableBySearchFunctionalityOrCreateAnNewBillingCustomerByEnteringTheDetailsRequested()
            {
                throw new PendingStepException();
            }

            [Given(@"I slect an Billing organisation if available by search  functionality or create an new Billing organization by entering the details requested")]
            public void GivenISlectAnBillingOrganisationIfAvailableBySearchFunctionalityOrCreateAnNewBillingOrganizationByEnteringTheDetailsRequested()
            {
                throw new PendingStepException();
            }

            [Given(@"choose Yes/No for ""([^""]*)"" field")]
            public void GivenChooseYesNoForField(string p0)
            {
                throw new PendingStepException();
            }

            [Given(@"I click on Save")]
            public void GivenIClickOnSave()
            {
            _hooks._xrmApp.Entity.Save();
            _hooks._xrmApp.ThinkTime(3000);
        }

            [Given(@"the Application number should be generated in format\(yyyy-xxxxxx-WLM-SPM\)")]
            public void GivenTheApplicationNumberShouldBeGeneratedInFormatYyyy_Xxxxxx_WLM_SPM()
            {
                throw new PendingStepException();
            }

            [Given(@"the priority should be assigned to application")]
            public void GivenThePriorityShouldBeAssignedToApplication()
            {
                throw new PendingStepException();
            }

        //    [Given(@"I go to ""([^""]*)"" stage")]
        //    public void GivenIGoToStage(string p0)
        //    {
        //    _hooks.selectAstage("Application Received");
        //}

        [Given(@"I wiil progress from Application recieved to Assessment stage")]
        public void GivenIWiilProgressFromApplicationRecievedToAssessmentStage()
        {
            _hooks.selectAstage("Application Received");
            _hooks.ClickonTechnicalAssessment();
        }
        [Given(@"I will populate application details tab")]
        public void GivenIWillPopulateApplicationDetailsTab()
        {
            _hooks._xrmApp.Entity.SelectTab("Application Details");
            _hooks._xrmApp.ThinkTime(4000);
            _hooks._xrmApp.Entity.SelectTab("Application Details");
        }
        [Given(@"I will populate an existing site and new permission")]
        public void GivenIWillPopulateAnExistingSiteAndNewPermission()
        {
            _hooks.ClickOnAddExistingSite();
            _hooks.ClickonNewPermission();
            _hooks._xrmApp.ThinkTime(2000);
            _hooks._xrmApp.QuickCreate.SetValue(new OptionSet { Name = "sdds_permissiontype", Value = "Planning permission" });
            _hooks._xrmApp.QuickCreate.SetValue(new LookupItem { Name = "sdds_councilid", Value = "Aberdeen City Council", Index = 0 });
            _hooks._xrmApp.QuickCreate.SetValue("sdds_refnumber", "test789");
            _hooks._xrmApp.QuickCreate.SetValue(new OptionSet { Name = "sdds_planningpermissiontype", Value = "Full" });
            _hooks.ClickonQuickSaveAndContinue();
            _hooks._xrmApp.ThinkTime(2000);
            _hooks.ClickonSave();
        }

        [Given(@"I will add an existing site")]
        public void GivenIWillAddAnExistingSite()
        {
            _hooks.ClickOnAddExistingSite();
        }


            [Given(@"click on Next Stage")]
            public void GivenClickOnNextStage()
            {
                throw new PendingStepException();
            }

            //[Given(@"I go to ""([^""]*)"" Stage")]
            //public void GivenIGoToStage(string triage)
            //{
            //    throw new PendingStepException();
            //}

            [Given(@"I select Yes/No to all questions from dropdown")]
            public void GivenISelectYesNoToAllQuestionsFromDropdown()
            {
                throw new PendingStepException();
            }

            [Given(@"I select an Allocator")]
            public void GivenISelectAnAllocator()
            {
                throw new PendingStepException();
            }

            //[Given(@"I go to ""([^""]*)"" Stage")]
            //public void GivenIGoToStage(string p0)
            //{
            //    throw new PendingStepException();
            //}

            [Given(@"I select an Lead Adviser")]
            public void GivenISelectAnLeadAdviser()
            {
                throw new PendingStepException();
            }

            //[Given(@"Click on Next Stage")]
            //public void GivenClickOnNextStage()
            //{
            //    throw new PendingStepException();
            //}

            //[Given(@"I go to ""([^""]*)"" tab")]
            //public void GivenIGoToTab(string p0)
            //{
            //    throw new PendingStepException();
            //}

            [Given(@"I add existing site under Sites or create an new site details")]
            public void GivenIAddExistingSiteUnderSitesOrCreateAnNewSiteDetails()
            {
                throw new PendingStepException();
            }

            [Given(@"I  Add  New Licensable Action in Licensable Action\(s\)")]
            public void GivenIAddNewLicensableActionInLicensableActionS()
            {
                throw new PendingStepException();
            }

            [Given(@"enter name as Test, Species as Badger, Activity as ""([^""]*)""")]
            public void GivenEnterNameAsTestSpeciesAsBadgerActivityAs(string p0)
            {
                throw new PendingStepException();
            }

            [Given(@"select Sett Type from dropdown options")]
            public void GivenSelectSettTypeFromDropdownOptions()
            {
                throw new PendingStepException();
            }

            [Given(@"enter propose start date and propose end date")]
            public void GivenEnterProposeStartDateAndProposeEndDate()
            {
                throw new PendingStepException();
            }

            [Given(@"select the ""([^""]*)"" to Use and click on save")]
            public void GivenSelectTheToUseAndClickOnSave(string p0)
            {
                throw new PendingStepException();
            }

            [Given(@"I add a new planning consent")]
            public void GivenIAddANewPlanningConsent()
            {
                throw new PendingStepException();
            }

            [Given(@"I select the newly added planning consent")]
            public void GivenISelectTheNewlyAddedPlanningConsent()
            {
                throw new PendingStepException();
            }

            [Given(@"I add and select existing authorised person")]
            public void GivenIAddAndSelectExistingAuthorisedPerson()
            {
                throw new PendingStepException();
            }

            //[Given(@"I click on save")]
            //public void GivenIClickOnSave()
            //{
            //    throw new PendingStepException();
            //}

            [Given(@"select yes for On/Next to Designated site")]
            public void GivenSelectYesForOnNextToDesignatedSite()
            {
                throw new PendingStepException();
            }

            [Given(@"I click on New designated site")]
            public void GivenIClickOnNewDesignatedSite()
            {
                throw new PendingStepException();
            }

            [Given(@"I select the Designated site name from the list")]
            public void GivenISelectTheDesignatedSiteNameFromTheList()
            {
                throw new PendingStepException();
            }

            [Given(@"the site code and Type populates with respect to site name")]
            public void GivenTheSiteCodeAndTypePopulatesWithRespectToSiteName()
            {
                throw new PendingStepException();
            }

            [Given(@"I select yes for permission")]
            public void GivenISelectYesForPermission()
            {
                throw new PendingStepException();
            }

            [Given(@"I enter the details of permission")]
            public void GivenIEnterTheDetailsOfPermission()
            {
                throw new PendingStepException();
            }

            [Given(@"select Yes/No for the field ""([^""]*)""\?")]
            public void GivenSelectYesNoForTheField(string p0)
            {
                throw new PendingStepException();
            }

            [Given(@"Enter the details requested if selected yes")]
            public void GivenEnterTheDetailsRequestedIfSelectedYes()
            {
                throw new PendingStepException();
            }

            [Given(@"click on save")]
            public void GivenClickOnSave()
            {
                throw new PendingStepException();
            }

            [Given(@"I add a new consultation")]
            public void GivenIAddANewConsultation()
            {
                throw new PendingStepException();
            }

            [Given(@"I select delivery team as consulted team")]
            public void GivenISelectDeliveryTeamAsConsultedTeam()
            {
                throw new PendingStepException();
            }

            [Given(@"I enter licence for consultation reason")]
            public void GivenIEnterLicenceForConsultationReason()
            {
                throw new PendingStepException();
            }

            [Given(@"I enter â€˜successful consultationâ€™ for consultation response")]
            public void GivenIEnterASuccessfulConsultationaForConsultationResponse()
            {
                throw new PendingStepException();
            }

            [Given(@"I select before today as date of consultation")]
            public void GivenISelectBeforeTodayAsDateOfConsultation()
            {
                throw new PendingStepException();
            }

            [Given(@"I select today as date of consultation response")]
            public void GivenISelectTodayAsDateOfConsultationResponse()
            {
                throw new PendingStepException();
            }

            [Given(@"I open each of the Assessment record\(s\) to record the outcome of your analysis")]
            public void GivenIOpenEachOfTheAssessmentRecordSToRecordTheOutcomeOfYourAnalysis()
            {
                throw new PendingStepException();
            }

            [Given(@"I manually mark all task as complete")]
            public void GivenIManuallyMarkAllTaskAsComplete()
            {
                throw new PendingStepException();
            }

            [Given(@"I enter Yes to agree to declaration question")]
            public void GivenIEnterYesToAgreeToDeclarationQuestion()
            {
                throw new PendingStepException();
            }

            [Given(@"I select today as date of applicant declaration")]
            public void GivenISelectTodayAsDateOfApplicantDeclaration()
            {
                throw new PendingStepException();
            }

            [Given(@"I select yes to privacy question")]
            public void GivenISelectYesToPrivacyQuestion()
            {
                throw new PendingStepException();
            }

            [Given(@"I enter not applicable to supplementary questions")]
            public void GivenIEnterNotApplicableToSupplementaryQuestions()
            {
                throw new PendingStepException();
            }

            [Given(@"I check ecologist signature")]
            public void GivenICheckEcologistSignature()
            {
                throw new PendingStepException();
            }

            [Given(@"I select today as date of signature")]
            public void GivenISelectTodayAsDateOfSignature()
            {
                throw new PendingStepException();
            }

            [Given(@"I select yes to I agree to declaration")]
            public void GivenISelectYesToIAgreeToDeclaration()
            {
                throw new PendingStepException();
            }

            [Given(@"I select today as ecologist declaration dateÂ")]
            public void GivenISelectTodayAsEcologistDeclarationDateA()
            {
                throw new PendingStepException();
            }

            //[Given(@"I select Yes to privacy question")]
            //public void GivenISelectYesToPrivacyQuestion()
            //{
            //    throw new PendingStepException();
            //}

            [Given(@"I enter Yes/No to all Survey question")]
            public void GivenIEnterYesNoToAllSurveyQuestion()
            {
                throw new PendingStepException();
            }

            [Given(@"I manually Mark all tasks as complete")]
            public void GivenIManuallyMarkAllTasksAsComplete()
            {
                throw new PendingStepException();
            }

            [Given(@"I proceed to ""([^""]*)"" tab")]
            public void GivenIProceedToTab(string decision)
            {
                throw new PendingStepException();
            }

            [Given(@"I navigate to licence section")]
            public void GivenINavigateToLicenceSection()
            {
                throw new PendingStepException();
            }

            [Given(@"I select Generate License")]
            public void GivenISelectGenerateLicense()
            {
                throw new PendingStepException();
            }

            [Given(@"the new licence form appears")]
            public void GivenTheNewLicenceFormAppears()
            {
                throw new PendingStepException();
            }

            [Given(@"I see the general section")]
            public void GivenISeeTheGeneralSection()
            {
                throw new PendingStepException();
            }

            [Given(@"I select licence granted for \(purpose\) = Development")]
            public void GivenISelectLicenceGrantedForPurposeDevelopment()
            {
                throw new PendingStepException();
            }

            [Given(@"I select licence start date = Tommorrow's date")]
            public void GivenISelectLicenceStartDateTommorrowsDate()
            {
                throw new PendingStepException();
            }

            [Given(@"I select licence end date = Today next year")]
            public void GivenISelectLicenceEndDateTodayNextYear()
            {
                throw new PendingStepException();
            }

            [Given(@"select Staus as Active")]
            public void GivenSelectStausAsActive()
            {
                throw new PendingStepException();
            }

            [Given(@"I navigate to the licensable conditions tab")]
            public void GivenINavigateToTheLicensableConditionsTab()
            {
                throw new PendingStepException();
            }

            [Given(@"I see the licensable conditions under the licence")]
            public void GivenISeeTheLicensableConditionsUnderTheLicence()
            {
                throw new PendingStepException();
            }

            [Given(@"navigate to the licence notes tab")]
            public void GivenNavigateToTheLicenceNotesTab()
            {
                throw new PendingStepException();
            }

            [Given(@"I see the licence notes under the licence")]
            public void GivenISeeTheLicenceNotesUnderTheLicence()
            {
                throw new PendingStepException();
            }

            [Given(@"I click â€œsave & closeâ€ button")]
            public void GivenIClickAŒsaveCloseaButton()
            {
                throw new PendingStepException();
            }

            [Given(@"a licence is created for the application")]
            public void GivenALicenceIsCreatedForTheApplication()
            {
                throw new PendingStepException();
            }

            [Given(@"I click on ""([^""]*)"" stage")]
            public void GivenIClickOnStage(string assessment)
            {
                throw new PendingStepException();
            }

            [Given(@"I enter Licence decision and justification")]
            public void GivenIEnterLicenceDecisionAndJustification()
            {
                throw new PendingStepException();
            }

            [Given(@"I select Assessment Outcome as Granted")]
            public void GivenISelectAssessmentOutcomeAsGranted()
            {
                throw new PendingStepException();
            }

            [Given(@"I click on Next stage")]
            public void GivenIClickOnNextStage()
            {
                throw new PendingStepException();
            }

            [Given(@"I proceed to ""([^""]*)"" stage")]
            public void GivenIProceedToStage(string decisions)
            {
                throw new PendingStepException();
            }

            [Given(@"I check Generate document icon should be avaialble in screen")]
            public void GivenICheckGenerateDocumentIconShouldBeAvaialbleInScreen()
            {
                throw new PendingStepException();
            }

            [When(@"I click on generate document")]
            public void WhenIClickOnGenerateDocument()
            {
            _hooks._xrmApp.CommandBar.ClickCommand("Generate Document");
            _hooks._xrmApp.ThinkTime(23000);
        }
        [Then(@"I will validate the url of the opened Licence document")]
        public void ThenIWillValidateTheUrlOfTheOpenedLicenceDocument()
        {
            _hooks.switchToFirstTab();
            _hooks._xrmApp.ThinkTime(1500);
            Assert.IsTrue((_hooks.getcurrentUrl()).Contains("blob:https://defra-sdds-test.crm11.dynamics.com/"));
           
        }
        [Then(@"I will validate generate licence page")]
        public void ThenIWillValidateGenerateLicencePage()
        {
            Assert.IsTrue((_hooks.GetLicenceGrantedText()).Contains("granted"));
            _hooks.ClickonSaveandClose();
        }

       
        [Then(@"the System generates the preview of License document")]
            public void ThenTheSystemGeneratesThePreviewOfLicenseDocument()
            {
                throw new PendingStepException();
            }

            [Then(@"I select Accept for License generated")]
            public void ThenISelectAcceptForLicenseGenerated()
            {
                throw new PendingStepException();
            }

            [Then(@"select yes to issue the applicant a copy of the generated licence")]
            public void ThenSelectYesToIssueTheApplicantACopyOfTheGeneratedLicence()
            {
                throw new PendingStepException();
            }

            [Then(@"the system generates the email with an A(.*) pdf granted cover letter under the License timeline")]
            public void ThenTheSystemGeneratesTheEmailWithAnAPdfGrantedCoverLetterUnderTheLicenseTimeline(int p0)
            {
                throw new PendingStepException();
            }

            [Then(@"a licence document is created")]
            public void ThenALicenceDocumentIsCreated()
            {
                throw new PendingStepException();
            }

            [Then(@"I go back to ""([^""]*)"" Stage")]
            public void ThenIGoBackToStage(string p0)
            {
                throw new PendingStepException();
            }

            [Then(@"select Yes for the question ""([^""]*)""")]
            public void ThenSelectYesForTheQuestion(string p0)
            {
                throw new PendingStepException();
            }

            [Then(@"I click on Next stage")]
            public void ThenIClickOnNextStage()
            {
                throw new PendingStepException();
            }

            [Then(@"I go to ""([^""]*)"" tab")]
            public void ThenIGoToTab(string p0)
            {
                throw new PendingStepException();
            }

            [Then(@"I create a new charge request for the licence generated")]
            public void ThenICreateANewChargeRequestForTheLicenceGenerated()
            {
                throw new PendingStepException();
            }

            [Then(@"click on save")]
            public void ThenClickOnSave()
            {
                throw new PendingStepException();
            }

            [Then(@"I go to ""([^""]*)"" Stage")]
            public void ThenIGoToStage(string p0)
            {
                throw new PendingStepException();
            }

            [Then(@"I select yes for ""([^""]*)""")]
            public void ThenISelectYesFor(string p0)
            {
                throw new PendingStepException();
            }

            [Then(@"I click on Finish")]
            public void ThenIClickOnFinish()
            {
                throw new PendingStepException();
            }

       


    }
}