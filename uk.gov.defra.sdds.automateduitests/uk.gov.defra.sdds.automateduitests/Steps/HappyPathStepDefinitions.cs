using System;
using TechTalk.SpecFlow;
using uk.gov.defra.sdds.automateduitests.Setup;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uk.gov.defra.sdds.automateduitests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Faker;
using OpenQA.Selenium.Support.UI;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using TechTalk.SpecFlow.CommonModels;

namespace uk.gov.defra.sdds.automateduitests.Steps

{
    [Binding]
    public class HappyPathStepDefinitions
    {
        HappyPathPage _happyPathPage;
        Hooks _hooks;
        string refs;
        public HappyPathStepDefinitions(HappyPathPage happyPathPage, Hooks hooks)
        {
            _hooks = hooks;  
            _happyPathPage = happyPathPage;
        }

        //[When(@"I navigate to (.*) and select Application")]
        //public void WhenINavigateToLicenceApplicationAndSelectApplication()

        //{
        //    _happyPathPage.OpenLicenceApp();
        //    _happyPathPage.OpenApplications();
        //}


        [Given(@"I will populate the conservation section")]
        public void GivenIWillPopulateTheConservationSession()
        {

            _happyPathPage._xrmApp.ThinkTime(500);
            _happyPathPage._xrmApp.Entity.SelectTab("Conservation considerations");
            _happyPathPage.addonornexttodesignated();
            _happyPathPage._xrmApp.ThinkTime(400);
            _happyPathPage._xrmApp.Entity.SelectTab("Conservation considerations");
            _happyPathPage.ClickonNewDesignatedSite();
            _happyPathPage._xrmApp.ThinkTime(400);
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_designatedsitenameid", Value = "Abberton Reservoir", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue("sdds_detailsofpermission", "test");
            _happyPathPage.addDesignatedSitdevelopment();
            _happyPathPage._xrmApp.Entity.SetValue("sdds_neadvicefrom", "test3");
            
            _happyPathPage.addNAadvice();   
            _happyPathPage._xrmApp.Entity.SetValue("sdds_neadvicefrom", "testnewnw");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_outcomeoftheadvisefromne", "testtesttetststtt");
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage.ClickonSave();
            _happyPathPage.ClickonSaveandClose();

        }

        [Given(@"the (.*) is logged into Licence power app")]
        public void GivenTheUserIsLoggedIntoLicencePowerApp(string role)
        {if (role == "User")
                {
                _happyPathPage.LaunchCRMApplication();
            }
            else
            {
                _happyPathPage.LaunchCRMApplicationAsSuperUser(); ;
            }
        }

        [Given(@"I go to (.*) home screen")]
        public void GivenIGoToApplicationsHomeScreen(string appType)
        {
            if (appType == "Feedback")
            {
                _happyPathPage.OpenFeedbackApp();

            }
            else if (appType == "Applications")
            {
                _happyPathPage.OpenLicenceApp();
                _happyPathPage.OpenApplications(appType);

            }
            else if (appType == "Admin")
            {
                _happyPathPage.OpenAdminApp();
                _happyPathPage.OpenApplications(appType);
            }

        }
        [Given(@"the user select the ([^']*) role")]
        public void GivenAUserIsAddedAsAn(string allocator)
        {
            _happyPathPage.UserRoleSelection(allocator);
        }

        [Given(@"'([^']*)' is added to the selected role")]
        public void GivenAIsAddedToTheSelectedRole(string user)
        {
            _happyPathPage.addExistingUserAccount(user);
        }

        [Given(@"'([^']*)' is removed from the (.*) role")]
        public void GivenIsRemovedFromTheAllocatorRole(string user, string roleType)
        {
            _happyPathPage.removeAddedUser(user, roleType);
        }


        [Given(@"I click on New \(plus sign\) to add a new application")]
        public void GivenIClickOnNewPlusSignToAddANewApplication()
        {
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage._xrmApp.CommandBar.ClickCommand("New");
            _happyPathPage._xrmApp.ThinkTime(2000);
           
        }




        [Given(@"I select satisfied rating")]
        public void GivenISelectSatisfiedRating()
        {
            _happyPathPage._xrmApp.ThinkTime(5000);
       


            _happyPathPage.feedbackrating();



            _happyPathPage._xrmApp.Entity.SetValue("sdds_howcanweimprovethisservice", Faker.Lorem.Paragraph(1));

           
        }


        [Given(@"I will enter text on '([^']*)' field")]
        public void GivenIWillEnterTextOnField(string p0)
        {
            _happyPathPage._xrmApp.Entity.SetValue("sdds_howcanweimprovethisservice",Faker.Lorem.Paragraph(300));

        }


        [Given(@"I will populate the remaining ""([^""]*)"" Tab details for (.*)")]
     
        public void GivenIGoToTab(string general,string types)
        {
            
                _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage._xrmApp.Entity.SetValue("sdds_applicationformreceiveddate", DateTime.Today.AddDays(-1), "dd/MM/yyyy", "HH:mm");

            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistid", Value = "Roger Nichollas", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_ecologistorganisationid", Value = "Total Ecology Ltd.", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicantid", Value = "AbigailPouros", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_organisationid", Value = "Total Ecology Ltd.-Dele", Index = 0 });
 
            _happyPathPage._xrmApp.ThinkTime(2000);
            if (types.Equals("A24"))
            {
                _happyPathPage.movetoelementsameuser();
            }
           
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeapplicantcontactid", Value = "Abagail smitham", Index = 0 });
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_alternativeecologistcontactid", Value = "Roger Nicholls", Index = 0 });
            if (types.Equals("A24")) { 
                _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Development ", Index = 0 });
            }
            else if (types.Equals("A01"))
            { 
                _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Agricultural, Forestry or Drainage operations ", Index = 0 }); 
        }
            else if (types.Equals("A25"))
            {
                _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Science, education or conservation", Index = 0 });
            }
            else if (types.Equals("A26"))
            {
                _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationpurpose", Value = "Preservation/investigation of ancient monuments", Index = 0 });

            }
            
            _happyPathPage.selecapplicationcategory();  
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage._xrmApp.Entity.Save();
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage._xrmApp.Entity.SetValue("sdds_descriptionofproposal", Faker.Address.StreetName());
            if (types.Equals("A01")) {
                _happyPathPage._xrmApp.Entity.SetValue("sdds_licencejustification", Faker.Address.SecondaryAddress());
                _happyPathPage._xrmApp.Entity.SetValue("sdds_inrelationtolicencepurpose", Faker.Address.UkCounty());
                _happyPathPage.selectA01TakinAction();
                _happyPathPage._xrmApp.Entity.SetValue("sdds_providedetailsoftheactionstaken", Faker.Address.UkCounty());

            }
            else if (types.Equals("A26"))
            {
                _happyPathPage._xrmApp.Entity.SetValue("sdds_licencejustification", Faker.Address.SecondaryAddress());
                _happyPathPage._xrmApp.Entity.SetValue("sdds_inrelationtolicencepurpose", Faker.Address.UkCounty());
                _happyPathPage.selectA01TakinAction();
                _happyPathPage._xrmApp.Entity.SetValue("sdds_providedetailsoftheactionstaken", Faker.Address.UkCounty());

            }
            else if (types.Equals("A25"))
            {
                _happyPathPage._xrmApp.Entity.SetValue("sdds_licencejustification", Faker.Address.SecondaryAddress());
                _happyPathPage._xrmApp.Entity.Save();

            }

        }

        [Given(@"I select Application Type as (.*) BAdger")]
        public void GivenISelectApplicationTypeAsABAdger(string formType)
        {
            if (formType.Equals("A24"))
            {
                _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationtypesid", Value = "A24 Badger", Index = 0 });
            }
            else if (formType.Equals("A01"))
            {
                _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationtypesid", Value = "A01 Badger", Index = 0 });
            }
            else if (formType.Equals("A25"))
            {
                _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationtypesid", Value = "A25 Badger", Index = 0 });
            }
            else if (formType.Equals("A26"))
            {
                _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_applicationtypesid", Value = "A26 Badger", Index = 0 });
            }


        }

        [Given(@"I will populate and complete disease risk assessment section for (.*)")]
        public void GivenIWillPopulateAndCompleteDiseaseRiskAssessmentSession(string formType)
        {
            _happyPathPage._xrmApp.Entity.SelectTab("Disease Risk Assessment");
            _happyPathPage._xrmApp.Entity.SubGrid.OpenSubGridRecord("Subgrid_16", 0);
            _happyPathPage.ClickonRiskAssessmentTasks(formType);
            _happyPathPage._xrmApp.ThinkTime(2000);
        }

        [Given(@"I will populate and complete the ecologist experience section")]
        public void GivenIWillPopulateAndCompleteTheEcologistExperienceSession()
        {
            _happyPathPage._xrmApp.Entity.SelectTab("Experience assessment");
            _happyPathPage.ClickonExperienceassessmentTasks();
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage._xrmApp.Entity.SetValue("sdds_ecologistexperienceofbadgerecology", "testnewnw test 40003£$%%testnewnw");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_ecologistexperienceofmethods", "testnewnw test 40003£$%%");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_mitigationclassrefno", "test36262");
            _happyPathPage.ClickonSave();
        }

        [Given(@"I will populate and complete the technical assessment section for (.*)")]
        public void GivenIWillPopulateAndCompleteTheTechnicalAssessmentSection(string formtype)
        {
            _happyPathPage.SelectTechnicalAssessment();
            _happyPathPage.licencerequiredforproposedactivity();
            _happyPathPage.proposedactionsproportionatescale();
            _happyPathPage.proposedactionscouldresolveproblem(); 
            _happyPathPage.satisfiedwithpersonsundertakingworks();
            _happyPathPage._xrmApp.Entity.SetValue("sdds_licencedecisionandjustification", "test36new");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _happyPathPage.ClickonBackgroundInformation();
            _happyPathPage._xrmApp.Entity.SetValue("sdds_licencedecisionandjustification", "test36new");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _happyPathPage.ClickonConservation();
            _happyPathPage._xrmApp.Entity.SetValue("sdds_licencedecisionandjustification", "test36new");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _happyPathPage.ClickonRiskSensitivity(formtype);
            _happyPathPage._xrmApp.Entity.SetValue("sdds_licencedecisionandjustification", "test36new");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _happyPathPage.ClickonDiseaseRisk();
            _happyPathPage._xrmApp.Entity.SetValue("sdds_licencedecisionandjustification", "test36new");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_furtherassessmentinformation", "test36262");
            _happyPathPage.ClickonSurvey();
         
        }

        [Given(@"I will populate and complete the decision section")]
        public void GivenIWillPopulateAndCompleteTheDecisionSection()
        {
            _happyPathPage.SelectDecision();
            _happyPathPage.ClickOnAddNewLicence();
      
            _happyPathPage._xrmApp.Entity.SetValue("sdds_licencestartdate", DateTime.Today, "dd/MM/yyyy", "HH:mm");
            _happyPathPage._xrmApp.Entity.SetValue("sdds_licenceenddate", DateTime.Now.AddYears(1), "dd/MM/yyyy", "HH:mm");
            _happyPathPage.setlicencestatus();
            _happyPathPage.ClickonSaveandClose();
        }

        [Given(@"I will click on decision tab to add a new compliance")]
        public void GivenIWillClickOnDecisionTabButton()
        {
            _happyPathPage.SelectTechnicalAssessment();
            _happyPathPage.AddCompliance();
        }

        [Given(@"I will complete the Assessment stage checklist with Licence (.*) outcome")]
        public void GivenIWillCompleteTheAssessmentStageChecklistWithLicenceGrantedOutcome(string type)
        {
            _happyPathPage.selectnewstage("Assessment", type); 
            _happyPathPage._xrmApp.Entity.Save();

        }
        [Given(@"I will click on decision tab")]
        public void GivenIWillClickOnDecisionTab()
        {
            _happyPathPage.ClickOnAddReturnOfAction();
 
        }

        [Given(@"I will populate the general tab of return")]
        public void GivenIWillPopulateTheGeneralTabOfReturn()
        {
           _happyPathPage.switchToReturnFirstTab();
   
        }

        [Given(@"I will populate the (.*) badger tab of return")]
        public void GivenIWillPopulateTheABadgerTabOfReturn(string _formType)
        {
            _happyPathPage.PopulateA24tab(_formType);
            _happyPathPage._xrmApp.ThinkTime(4000);

            _happyPathPage.win.SwitchTo().Window(_happyPathPage.win.WindowHandles[0]);
            _happyPathPage._xrmApp.CommandBar.ClickCommand("Refresh");
            _happyPathPage.ClickOnReturnOfAction();
            //_happyPathPage.powerAppRefresh();   
                }

        [Given(@"I click on Save feedback")]
        public void GivenIClickOnSaveFeedback()
        {
            _happyPathPage._xrmApp.Entity.Save();
            _happyPathPage._xrmApp.ThinkTime(2000);
            refs = _happyPathPage.getref().Substring(0, 12);

        }

        [Given(@"I click on Save")]
        public void GivenIClickOnSave()
        {
            _happyPathPage._xrmApp.Entity.Save();
            _happyPathPage._xrmApp.ThinkTime(2000);

        }
        [Given(@"i will validate the created feedback")]
        public void GivenIWillValidateTheCreatedFeedback()
        {
            Assert.IsNotNull(refs);
         
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage.ClickonSaveandClose();
            _happyPathPage._xrmApp.ThinkTime(3000);
            _happyPathPage._xrmApp.Grid.Search(refs);
            _happyPathPage.enterFeedbackRef(refs);
            _happyPathPage.clickOnSearch();
        }


        [Given(@"I pause the application")]
        public void GivenIPauseTheApplication()
        {
            _happyPathPage.Pauseapplication();  
        }

        [Given(@"I will resume the application")]
        public void GivenIWillResumeTheApplication()
        {
            _happyPathPage.Resumeapplication();
        }
        [Given(@"I will validate the application is Resumed")]
        public void GivenIWillValidateTheApplicationIsResumed()
        {
            Assert.AreEqual(_happyPathPage.PauseValue(), "Pause");
        }
        [Given(@"I will withdraw the application")]
        public void GivenIWillWithdrawTheApplication()
        {
            _happyPathPage.Withdrawapplication();
        }



        [Then(@"I will verify the case status is ""([^""]*)""")]
        public void ThenIWillVerifyTheCaseStatusIs(String expected)
        {
           Assert.AreEqual(_happyPathPage.verifyStatues(), expected);
            
        }


        [Given(@"I will progress from Application recieved to Assessment stage")]
        public void GivenIWiilProgressFromApplicationRecievedToAssessmentStage()
        {


            _happyPathPage.selectAstage("Application Received");
        }
        [Given(@"I will check priority is not null")]
        public void GivenIWillCheckPriorityIsNotNull()
        {

            _happyPathPage.seleniumRefresh();
            _happyPathPage.priorityvalue();
            Console.WriteLine(_happyPathPage.priorityvalue());
            Assert.IsNotNull(_happyPathPage.priorityvalue());

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
            _happyPathPage.win.SwitchTo().Window(_happyPathPage.win.WindowHandles[0]);

            _happyPathPage.ClickOnAddExistingSite();
            _happyPathPage.ClickonNewPermission();
            _happyPathPage._xrmApp.ThinkTime(2000);
            _happyPathPage.selectpermissiontype();


            _happyPathPage._xrmApp.QuickCreate.SetValue(new LookupItem { Name = "sdds_councilid", Value = "Aberdeen City Council", Index = 0 });
            _happyPathPage._xrmApp.QuickCreate.SetValue("sdds_refnumber", "test789");
            _happyPathPage.selectpermissionoption();

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
            //_happyPathPage.ClickOnMiscellaneous();
            _happyPathPage.ClickOnLicencegenerate();
        }
        [Then(@"I will validate the url of the opened Licence document")]
        public void ThenIWillValidateTheUrlOfTheOpenedLicenceDocument()
        {
            _happyPathPage._xrmApp.ThinkTime(3500);
            Console.WriteLine(_happyPathPage.getcurrentUrl());
            Assert.IsTrue((_happyPathPage.getcurrentUrl()).Contains("blob:https://defra-sdds-test.crm11.dynamics.com/"));

        }

        [Given(@"I add Licensable methods for (.*)")]
        public void GivenIAddLicensableMethods(string formtype)
        {
            Thread.Sleep(2000);
            _happyPathPage._xrmApp.Entity.SelectTab("Application Details");
            _happyPathPage.addLicensableMethod(formtype);
            _happyPathPage.ClickonSave();
            _happyPathPage.ClickSaveandClose();
        }


        [Given(@"I will add Licensable action for (.*)")]
            public void GivenIWillAddLicensableAction(string types)
            {

            _happyPathPage._xrmApp.ThinkTime(3000);
            _happyPathPage._xrmApp.Entity.SelectTab("Application Details");

            _happyPathPage.ClickonLicensableactions();
            _happyPathPage._xrmApp.ThinkTime(3500);


            
            _happyPathPage._xrmApp.Entity.SetValue("sdds_species", Faker.Address.StreetName());
            _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_siteid", Value = "0ugvv25lwtup6ne8rmwerqkl05wmm0mf05ldgkv5e48gc1hgfb93cab5qs2w15526d8kyzqsjfny11d1a6xx6l998ezclhznjkya", Index = 0 });

            if (types.Equals("A24") || types.Equals("A01"))
            {
                _happyPathPage.selectSettType();
                _happyPathPage._xrmApp.Entity.SetValue("sdds_proposedstartdate", DateTime.Today, "dd/MM/yyyy", "HH:mm");
                _happyPathPage._xrmApp.Entity.SetValue("sdds_proposedenddate", DateTime.Now.AddYears(1), "dd/MM/yyyy", "HH:mm");
                _happyPathPage._xrmApp.Entity.SetValue("sdds_osgridref", "NY123456");
            }

            else if (types.Equals("A25"))
            {
                _happyPathPage._xrmApp.Entity.SetValue(new LookupItem { Name = "sdds_licenseactivityid", Value = "Destroy a sett", Index = 0 });
                _happyPathPage._xrmApp.Entity.SetValue("sdds_proposedstartdate", DateTime.Today, "dd/MM/yyyy", "HH:mm");
                _happyPathPage._xrmApp.Entity.SetValue("sdds_proposedenddate", DateTime.Now.AddYears(1), "dd/MM/yyyy", "HH:mm");
                _happyPathPage._xrmApp.Entity.SetValue("sdds_osgridref", "NY123456");



            }
            
     
            if (types.Equals("A24"))
            {
                _happyPathPage._xrmApp.Entity.SetValue("sdds_noentranceholeofbadgersett", "8");
                _happyPathPage._xrmApp.Entity.SetValue("sdds_howmanyentranceholesareactive", "3");
            }
            if (types.Equals("A01"))
            {
                _happyPathPage._xrmApp.Entity.SetValue("sdds_noentranceholeofbadgersett", "8");
                _happyPathPage._xrmApp.Entity.SetValue("sdds_howmanyentranceholesareactive", "3");
            }
                _happyPathPage.ClickonSaveandClose();
        




        }


        [Then(@"I will search for the licence number")]
        public void ThenIWillSearchForTheLicenceNumber()
        {
           _happyPathPage.getLicenseNo();
        }

        [Then(@"I will validate generate '([^']*)' licence email page")]
        public void ThenIWillValidateGenerateLicenceEmailPage(string type)

        {
            
            if (type.Equals("Not Grant"))
            {
                _happyPathPage.switchTonewFirstTab();
                Console.WriteLine(_happyPathPage.GetNotGrantedText());
                Console.WriteLine(type);
                Console.WriteLine(_happyPathPage.GetNotGrantedText().ToLower());
                Assert.IsTrue((_happyPathPage.GetNotGrantedText().ToLower()).Contains(type.ToLower()));
            }
            else
            {
                _happyPathPage.switchToFirstTab();
                _happyPathPage._xrmApp.ThinkTime(2000);
                Console.WriteLine(_happyPathPage.GetLicenceGrantedText());
                Console.WriteLine(type);
                Assert.IsTrue((_happyPathPage.GetLicenceGrantedText().ToLower()).Contains(type.ToLower()));
            }
            _happyPathPage.ClickonSave();
        }

        [Then(@"I will create a charge request")]
        public void ThenIWillCreateAChargeRequest()
        {
            _happyPathPage._xrmApp.CommandBar.ClickCommand("Charge Request");
            _happyPathPage.populatedChargeRequest();

        }

        [Then(@"I will validate completed charge request notification text")]
        public void ThenIWillValidateCompletedChargeRequestNotificationText()
        {
            Console.WriteLine(_happyPathPage.getwarningMessage());
            Assert.AreEqual(_happyPathPage.getwarningMessage(), "Payment request has been created. Please check the payment request tab for status updates");
        }


        [Given(@"I will complete the Compliance check entry BPF")]
        public void GivenIWillCompleteTheComplianceCheckEntryBPF()
        {
            _happyPathPage._xrmApp.ThinkTime(3000);

            _happyPathPage.win.SwitchTo().Window(_happyPathPage.win.WindowHandles[1]);

            _happyPathPage._xrmApp.Entity.SetValue("sdds_compliancecheckduedate", DateTime.Now, "dd/MM/yyyy", "HH:mm");

        }

        [Given(@"I will complete the Triage BPF")]
        public void GivenIWillCompleteTheTriageBPF()
        {
            throw new PendingStepException();
        }

        [Given(@"I will complete the Allocate for assessment BPF")]
        public void GivenIWillCompleteTheAllocateForAssessmentBPF()
        {
            throw new PendingStepException();
        }

        [Given(@"I will complete the Compliance check assessment BPF")]
        public void GivenIWillCompleteTheComplianceCheckAssessmentBPF()
        {
            throw new PendingStepException();
        }

        [Given(@"I will complete the Enforcement Outcome BPF")]
        public void GivenIWillCompleteTheEnforcementOutcomeBPF()
        {
            throw new PendingStepException();
        }

        [Given(@"I supporting upload documents to the sharepoint")]
        public void GivenISupportingUploadDocumentsToTheSharepoint()
        {
            _happyPathPage.UploadDocuments();


        }






    }
}