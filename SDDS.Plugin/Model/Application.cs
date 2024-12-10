// *********************************************************************
// Created by : Latebound Constants Generator 1.2023.12.1 for XrmToolBox
// Author     : Jonas Rapp https://jonasr.app/
// GitHub     : https://github.com/rappen/LCG-UDG/
// Source Org : https://defra-sdds-dev2.crm11.dynamics.com
// Filename   : C:\Users\vo000010\OneDrive - Defra\Documents\Application.cs
// Created    : 2024-05-09 12:38:18
// *********************************************************************
namespace SDDS.Plugin.Model
{
    /// <summary>OwnershipType: UserOwned, IntroducedVersion: 1.0</summary>
    public static class Application
    {
        public const string EntityName = "sdds_application";
        public const string EntityCollectionName = "sdds_applications";

        #region Attributes

        /// <summary>Type: Uniqueidentifier, RequiredLevel: SystemRequired</summary>
        public const string PrimaryKey = "sdds_applicationid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string PrimaryName = "sdds_name";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: Text</summary>
        public const string ecologistexperienceonmitigationprojects = "sdds_ecologistexperienceonmitigationprojects";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Pleaseoutlinemitigationforlightdisturbance = "sdds_outlinemitigationforlightdisturbance";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Summaryofmitigationandcompensationmeasur = "sdds_summaryofmitigationandcompensationmeasur";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Bat-ER -  Why is a licence needed?, OptionSetType: Picklist</summary>
        public const string Whyisalicenceneeded = "sdds_whyisalicenceneeded";
        /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
        public const string DeprecatedStageId = "stageid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1250, Format: Text</summary>
        public const string DeprecatedTraversedPath = "traversedpath";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
        public const string _1stReferee = "sdds_firstreferee";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
        public const string _2ndReferee = "sdds_secondreferee";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string _30daysfromrecieveddate = "sdds_daysfromrecieveddate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Additionalinformation = "sdds_additionalinformation";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser</summary>
        public const string Adviser = "sdds_assessorid";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string Allocateforassessmentdate = "sdds_allocateforassessmentdate";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser</summary>
        public const string Allocator = "sdds_advisormanager";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
        public const string Alternativeapplicantcontact = "sdds_alternativeapplicantcontactid";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
        public const string AlternativeEcologistcontact = "sdds_alternativeecologistcontactid";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string AnyfurtherNEcasedetails_DONOTUSE = "sdds_anyfurthernecasedetails";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Assessment Category, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string AppAssessment = "sdds_appassessment";
        /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: contact</summary>
        public const string Applicant = "sdds_applicantid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Applicantagreetodelcaration = "sdds_applicantagreetodelcaration";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Communication Preference, OptionSetType: Picklist</summary>
        public const string ApplicantCommunicationPreference = "sdds_applicantcommpreference";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string ApplicantContactNo = "sdds_applicantcontactno";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: account</summary>
        public const string ApplicantOrganisation = "sdds_organisationid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Applicantownerofland = "sdds_isapplicantonwnerofland";
        /// <summary>Type: Picklist, RequiredLevel: ApplicationRequired, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string ApplicantthesameasBillingCustomer = "sdds_applicantthesameasbillingcustomer";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string applicationaffectEUprotectspecies = "sdds_applicationaffecteuprotectspecies";
        /// <summary>Type: Picklist, RequiredLevel: Recommended, DisplayName: Purpose Application Category, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string ApplicationCategory = "sdds_applicationcategory";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: TimeZoneIndependent</summary>
        public const string Applicationformreceiveddate = "sdds_applicationformreceiveddate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string ApplicationNumber = "sdds_applicationnumber";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string ApplicationNumber2 = "sdds_applicationnumber2";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string Applicationrecieveddate_DONOTUSE = "sdds_applicationrecieveddate";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Applicationsource = "sdds_sourceremote";
        /// <summary>Type: Status, RequiredLevel: None, DisplayName: Status Reason, OptionSetType: Status</summary>
        public const string ApplicationStatus = "statuscode";
        /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: sdds_applicationtypes</summary>
        public const string Applicationtype = "sdds_applicationtypesid";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string AreMinimumrequirementsforhabitatmanagement = "sdds_areminimumrequirementforhabitatmanagement";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string AreNBCRMsbeingproposedinabat_accessiblearea = "sdds_renbcrmsbeingproposedinabataccessiblearea";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Areyouprovidingreferences = "sdds_areyouprovidingreferences";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Assessment Category, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string ASRAssessment = "sdds_asrassessment";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Assessing team, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Assessingteam_DONOTUSE = "sdds_assessingteam";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string AssessmentActivity = "sdds_assessmentactivity";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Assessment Category, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string AssessmentCategory = "sdds_assessmentcategory";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: sdds_assessmentcategorylogic</summary>
        public const string AssessmentCategoryLogic = "sdds_assessmentcategorylogicid";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string Assessmentdate = "sdds_assessmentdate";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
        public const string AssessmentDuration = "sdds_assessmentduration";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Assessment Outcome, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string AssessmentOutcome = "sdds_assessmentoutcome";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Assessment type, OptionSetType: Picklist</summary>
        public const string Assessmenttype = "sdds_assessmenttype";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string BadgerMitigationClasslicence = "sdds_badgermitigationclasslicence";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Exemption reason, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Badgerreasonforexemption = "sdds_badgerreasonforexemption";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Badgersitecommitmenthavebeenmet = "sdds_badgersitecommitmenthavebeenmet";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
        public const string BillingCustomer = "sdds_billingcustomerid";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: account</summary>
        public const string BillingOrganisation = "sdds_billingorganisationid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string BPFFinished = "sdds_bpffinished";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Briefdescriptionofproposal = "sdds_descriptionofproposal";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string buildingconsentrequired_DONOTUSE = "sdds_buildingconsentrequired";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string CentralSLATimer = "sdds_centralslatimer";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Outcome, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string CentralTeamSLAStatus = "sdds_centralteamslastatus";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Chargerequestraised = "sdds_chargerequestraised";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
        public const string CharterDeadline_DONOTUSE = "sdds_charterdeadline";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string ClosureDate = "sdds_closuredate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: Url</summary>
        public const string CompensationLink = "sdds_compensationlink";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string Completeinitialchecksdate = "sdds_completeinitialchecksdate";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string CompletedAssessment = "sdds_completedassessment";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string CompletedPre_Assessment = "sdds_completedpreassessment";
        /// <summary>Type: Lookup, RequiredLevel: ApplicationRequired, Targets: sdds_applicationpurpose</summary>
        public const string ConfirmApplicationPurpose = "sdds_applicationpurpose";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string confirmsubmitofanyconsent_DONOTUSE = "sdds_confirmsubmitofanyconsent";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Conflictsbetweenapplicationotherlegalcom = "sdds_conflictsbtwappotherlegalcommitment";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Consent granted, OptionSetType: Picklist</summary>
        public const string Consentgranted_DONOTUSE = "sdds_consentgranted";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No/NA, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Consentobtained_DONOTUSE = "sdds_consentobtained";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Consentreferencenumbers_DONOTUSE = "sdds_consentreferencenumbers";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Consent required?, OptionSetType: Picklist</summary>
        public const string Consentrequired_DONOTUSE = "sdds_consentrequired";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Consentrequired_DONOTUSE1 = "sdds_noconsentrequired";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: account</summary>
        public const string ConsentingAuthority_DONOTUSE = "sdds_consentingauthorityid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Consultationended = "sdds_consultationended";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string consultationSLATimer = "sdds_consultationslatimer";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: 100000001</summary>
        public const string Consultedspecialist_DONOTUSE = "sdds_consultedspecialist";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: transactioncurrency</summary>
        public const string Currency = "transactioncurrencyid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string DAS_discretionaryadvice = "sdds_dasdiscretionaryadvice";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string DAScaserefno = "sdds_dascasereference";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string Dateforreconsideration = "sdds_dateforreconsideration";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string Dateofapplicantdeclaration = "sdds_dateofapplicantdeclaration";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string DecisionIssued = "sdds_emaildocumentsent";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string DemolitionConsents = "sdds_demolitionconsents";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Describehowtheworkaffectconservationroost = "sdds_describehowtheworkaffactroost";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string Describethepotentialconflicts = "sdds_describethepotentialconflicts";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: Url</summary>
        public const string Designatedsiteview = "sdds_designatedsiteview";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string DetailsofCondtionsnotdischarged_DONOTUSE = "sdds_detailsofcondtionsnotdischarged";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Detailsofconvictions = "sdds_detailsofconvictions";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: Text</summary>
        public const string Detailsofecologistqualifications = "sdds_detailsofecologistqualifications";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string detailsofeffecttoEUspecies = "sdds_detailsofeffecttoeuspecies";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string detailsofotherconsent_DONOTUSE = "sdds_detailsofotherconsent";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string detailsoftheoutstandingconsent_DONOTUSE = "sdds_detailsoftheoutstandingconsent";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: Text</summary>
        public const string Detailsonecologistcurrentscience_DONOTUSE = "sdds_detailsonecologistcurrentscience";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Diseaseriskadditionalinfo_TOBEDELETED = "sdds_diseaseriskadditionalinfo";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Disease risk Outcome, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string DiseaseriskOutcome = "sdds_diseaseriskoutcome";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Domitigationandcompensationproposalsmeetreq = "sdds_omitigationandcompensationproposalsmeetreq";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Preferred Contact, OptionSetType: Picklist</summary>
        public const string DocumentationSentTo_DONOTUSE = "sdds_documentationsentto";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: True</summary>
        public const string Doestheprojectneedanypermissions = "sdds_doestheprojectneedanypermissions";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Doesyourapplicationrequireareasonedstatement = "sdds_applicationreasonedstatement";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Download_DONOTUSE = "sdds_download";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: sdds_earnedrecognisationlicence</summary>
        public const string EarnedRecognitionLicence = "sdds_earnedrecognisationlicenceid";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: contact</summary>
        public const string Ecologist = "sdds_ecologistid";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Communication Preference, OptionSetType: Picklist</summary>
        public const string EcologistCommunicationPreference = "sdds_ecologistcommpreference";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string EcologistContactNo = "sdds_ecologistcontactno";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Ecologistdeclaration = "sdds_ecologistdeclaration";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string Ecologistdeclarationdate = "sdds_ecologistdeclarationdate";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string EcologistexperienceofBadgerEcology = "sdds_ecologistexperienceofbadgerecology";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string Ecologistexperienceofmethods = "sdds_ecologistexperienceofmethods";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Ecologistholdalicence = "sdds_ecologistholdalicence";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Ecologistname_DONOTUSE = "sdds_ecologistname";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Ecologistonbatmitigationpast3years = "sdds_ecologistonbatmitigationpast3years";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: account</summary>
        public const string EcologistOrganisation = "sdds_ecologistorganisationid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Ecologistprivacynotice = "sdds_ecologistprivacynotice";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string Ecologistsignaturedate = "sdds_ecologistsignaturedate";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Ecologistsignaturetick = "sdds_ecologistsignaturetick";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: sdds_site</summary>
        public const string EnterSitehereDONOTUSE = "sdds_siteid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Europeanprotectedspecies_DONOTUSE = "sdds_europeanprotectedspecies";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Evidenceofcompetencyprovided = "sdds_evidenceofcompetencyprovided";
        /// <summary>Type: Decimal, RequiredLevel: None, MinValue: 0.0000000001, MaxValue: 100000000000, Precision: 10</summary>
        public const string ExchangeRate = "exchangerate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: Url</summary>
        public const string ExperienceChecker = "sdds_experiencechecker";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Explainhowitaffectlocalimportance = "sdds_explainhowitaffectlocalimportance";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string ExplainNon_standardmitigationCompensation = "sdds_explainnonstandardmitigationcompensation";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Explainwhynoactionshavebeentaken = "sdds_explainwhynoactionshavebeentaken";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string explainwhynoconsentrequired_DONOTUSE = "sdds_explainwhynoconsentrequired";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Outcome, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string FCSoutcome = "sdds_fcsoutcome";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Filesdeleted = "sdds_fiilesdeleted";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string fulljustificationprovided = "sdds_fulljustificationprovided";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string FullPlanning = "sdds_fullplanning";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string FurtherAssessmentInformation = "sdds_furtherassessmentinformation";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Url</summary>
        public const string GridFinderURL = "sdds_gridfinderurl";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Habitatissuesdischarged = "sdds_habitatissuesdischarged";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No/Not sure, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string HascasebeenseenbyNE_DONOTUSE = "sdds_seenbyne";
        /// <summary>Type: Boolean, RequiredLevel: ApplicationRequired, True: 1, False: 0, DefaultValue: False</summary>
        public const string Hastherebeenapreviousapplication_DONOTUS = "sdds_previousapplication";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Hasthisbeenmet = "sdds_hasthisbeenmet";
        /// <summary>Type: Boolean, RequiredLevel: ApplicationRequired, True: 1, False: 0, DefaultValue: False</summary>
        public const string Havesurveysbeenconducted = "sdds_havesurveysbeenconducted";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Haveyoucompletedthechargerequest = "sdds_haveyoucompletedthechargerequest";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No/Not sure, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string HaveyouConsultatedNEfor_DONOTUSE = "sdds_haveconsultedneforsite";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Haveyouemailedthedecisiondocument = "sdds_haveyouemailedthedecisiondocument";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No/NA, OptionSetType: Picklist, DefaultFormValue: 100000001</summary>
        public const string Haveyoureceivedownerpermission = "sdds_ownerpermissionreceived";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string HaveyousendrecordstoLRC = "sdds_haveyousendrecordstolrc";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: True</summary>
        public const string HeldBadgerLicence = "sdds_heldbadgerlicence";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Homeimprovement = "sdds_homeimprovement";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Url</summary>
        public const string IbTbUrl = "sdds_ibtbrrl";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 400, Format: TextArea</summary>
        public const string IfDisturbanceonly = "sdds_ifdisturbanceonly";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: Text</summary>
        public const string IfNBCRMpresent_Justification = "sdds_nbcrmpresentjustification";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string IfNoMitigationcompensationreq = "sdds_ifnomitigationcompensationreq";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Ifotherpleaseprovidedetails_DONOTUSE = "sdds_ifotherpleaseprovidedetails";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string Ifyeswhoprovidedadviceandwhen_DONOTUSE = "sdds_yesnecasedetails";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: 100000001</summary>
        public const string Impactondesignated_protectedsite = "sdds_impactondesignatedprotectedsite";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
        public const string ImportSequenceNumber = "importsequencenumber";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Inrelationtolicencepurpose = "sdds_inrelationtolicencepurpose";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Outcome, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string IROPIoutcome = "sdds_iropioutcome";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Isamainsettimpacted = "sdds_isamainsettimpacted";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Isaprotectedsite = "sdds_isaprotectedsite";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Isitalivedig = "sdds_isitalivedig";
        /// <summary>Type: Boolean, RequiredLevel: ApplicationRequired, True: 1, False: 0, DefaultValue: False</summary>
        public const string IsSiteaddresssameasapplicants = "sdds_issitesameasapplicants";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Istheproposedactionsproportionate = "sdds_proposedactionsproportionatescale";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Isthisaformalapplication = "sdds_isformalapplication";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Isthisasubsequentdraft = "sdds_issubsequentdraft";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string IsthisanNSIP = "sdds_isthisannsip";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Isthisphased_multiplot = "sdds_isthisphasedmultiplot";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string Issuedate = "sdds_issuedate";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser</summary>
        public const string IssuedBy = "sdds_issuedbyid";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Issuednotes = "sdds_issuednotes";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser</summary>
        public const string LeadAdviser = "sdds_leadadvisor";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: TimeZoneIndependent</summary>
        public const string LicenceApplicationDuedate = "sdds_licenceapplicationduedate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Licencedecisionandjustification = "sdds_licencedecisionandjustification";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string LicenceExempted = "sdds_licenceexempted";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string LicenceJustification = "sdds_licencejustification";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Licencerequiredforproposedactivity = "sdds_licencerequiredforproposedactivity";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string Licenseeffectivedate = "sdds_licenseeffectivedate";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string LicenseIssuedate = "sdds_licenseissuedate";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string ListedBuildingConsent = "sdds_listedbuildingconsent";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string MakePayment_DONOTUSE = "sdds_makepayment";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string maximumnumbersofrooststhatwillbedamaged = "sdds_maximumnumbersofrooststhatwillbedamaged";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 1, MaxValue: 2147483647</summary>
        public const string maximumnumbersofrooststhatwillbedestroyed = "sdds_maximumnumbersofrooststhatwillbedestroyed";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string mechanismforsafeguardofmitigation = "sdds_mechanismforsafeguardofmitigation";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Messagesenttoadviser = "sdds_messagesenttoadviser";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string MitigationClassrefno = "sdds_mitigationclassrefno";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string Mitigationworkdetails = "sdds_mitigationworkdetails";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string ModificationWhatactionshavenotbeencompleted = "sdds_modificationhatactionshavenotbeencomplete";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string ModificationWhatlicensableactionshavebeen = "sdds_modificationwhatactionsbeencompleted";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Why is a modification required? , OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string ModificationWhyismodificationrequired = "sdds_modificationwhyismodificationrequired";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Nameofapplicant = "sdds_nameofapplicant";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string nameofissuingauthorityandlicencenumber = "sdds_nameofissuingauthority";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string NationallySignificantInfrastucture = "sdds_nsiproject";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string NEConsultationOutcome_DONOTUSE = "sdds_neconsultationoutcome";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Newls has undertaken, OptionSetType: Picklist</summary>
        public const string NEWLShasundertakenthefollowing_DONOTUSE = "sdds_newlshasundertakenthefollowing";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string Nextannualreturnduedate_DELETED = "sdds_nextannualreturnduedate";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string No_Minimumrequirementforhabitatmgnt = "sdds_nominimumrequirementforhabitatmgnt";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string No_Otherprotectedspeciecommitment = "sdds_yesotherprotectedspeciecommitment";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string No_PostImpactmonitoring = "sdds_nopostimpactmonitoring";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Nopermissionrequired_Other = "sdds_nopermissionrequiredother";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
        public const string Noofyearslicencebeenheld = "sdds_noofyearslicencebeenheld";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Non_standardmitigationandcompensationmeasures = "sdds_nonstandardmitigationandcompensation";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Novelcomplexorourofseason = "sdds_novelcomplexorourofseason";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Outcome, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string NSAoutcome = "sdds_nsaoutcome";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
        public const string Numbersofindividualbatsofeachspecies = "sdds_numbersofindividualbatsofeachspecies";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
        public const string OldStatus = "sdds_oldstatus";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No/NA, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string On_NexttoDesignatedsite = "sdds_onnexttodesignatedsite";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string OtherCategory = "sdds_othercategory";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string OtherConsent = "sdds_otherconsent";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: True</summary>
        public const string Otherprotectedspciecommitmenthavebeenmet = "sdds_protectedspciecommitmentmet";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Otherprotectedspeciecommitment = "sdds_otherprotectedspeciecommitment";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Otherreasonforexemption = "sdds_otherreasonforexemption";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string otherreferencenumber = "sdds_otherreferencenumber";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string OutcomeDate = "sdds_outcomedate";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string OutlinePlanning = "sdds_outlineplanning";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Outcome, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Overallconsentoutcome_DonotUse = "sdds_overallconsentoutcome";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: TextArea</summary>
        public const string Permissionsobtainednotenough = "sdds_permissionsobtainednotenough";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PermittedDevelopmentRights = "sdds_permitteddevelopmentrights";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Assessment Category, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PlanningConsentAssessment = "sdds_planningconsentassessment";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
        public const string PostAssessmentDuration = "sdds_postassessmentduration";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Post Impact Population Monitoring, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PostImpactPopulationmonitoring = "sdds_postimpactpopulationmonitoring";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Post_developmentworkswillbesecured = "sdds_postdevelopmentworkswillbesecured";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
        public const string Pre_assessmentDuration = "sdds_preassessmentduration";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Preferred Contact, OptionSetType: Picklist</summary>
        public const string PreferredContact = "sdds_preferredcontact";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string PreviousApplicationorlicensenumber = "sdds_prevapplicationlicensenumber";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Previous Application Outcome, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PreviousApplicationOutcome_DONOTUSE = "sdds_previousapplicationoutcome";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string PreviousApplicationStatusReason = "sdds_previousapplicationstatusreason";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string PreviousDAS_PSS = "sdds_previousdaspss";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Prior Advice for this application, OptionSetType: Picklist</summary>
        public const string PriorAdviceforthisapplication = "sdds_prioradviceforthisapplication";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Priority, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Priority = "sdds_priority";
        /// <summary>Type: Uniqueidentifier, RequiredLevel: None</summary>
        public const string ProcessId = "processid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Projectpermissionsgranted = "sdds_projectpermissionsgranted";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Proposedactionscouldresolveproblem = "sdds_proposedactionscouldresolveproblem";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
        public const string Proposedenddate = "sdds_proposedenddate";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
        public const string Proposedstartdate = "sdds_proposedstartdate";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Proposedworkaffectaroostconservationsigfica = "sdds_proposedworkaffectaroost";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Providedetailsoftheactionstaken = "sdds_providedetailsoftheactionstaken";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string PSS_pre_submissionscreening = "sdds_psspresubmissionscreening";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string PSScaserefno = "sdds_psscaserefno";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Readprivacynotice = "sdds_readprivacynotice";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Reasons Newls Consultation with Area Team, OptionSetType: Picklist</summary>
        public const string ReasonforAreateamconsultation_DONOTUSE = "sdds_reasonforareateamconsultation";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Reason for pause, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Reasonforpause = "sdds_reasonforpause";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Reasonedstatementexceptiondetails = "sdds_reasonedstatementexceptiondetails";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: Url</summary>
        public const string Reasonedstatementlink = "sdds_reasonedstatementlink";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string RecordCreatedOn = "overriddencreatedon";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 300, Format: Url</summary>
        public const string RecordUrl = "sdds_recordurl";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string ReferenceorPurchaseOrderNumber = "sdds_referenceorpurchaseordernumber";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Relatedtoanypreviouslylicensedmitigation = "sdds_relatedtoprevlicensedmitigationwork";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Request Type, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string RequestType = "sdds_requesttype";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string ReservedMatters = "sdds_reservedmatters";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string ResubmissionProvidearesponsetoeachFIR = "sdds_resubmissionresponsetoeachfir";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
        public const string Retaindatauntil = "sdds_retaindatauntil";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Returnnotes_DELETED = "sdds_returnnotes";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No/NA, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Returnreceived_DELETED = "sdds_returnreceived";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No/NA, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Returnsatisfactory_DONOTUSE = "sdds_returnsatisfactory";
        /// <summary>Type: Lookup, RequiredLevel: None, Targets: systemuser</summary>
        public const string Returnedby_DELETED = "sdds_returnedbyuserid";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: UserLocal</summary>
        public const string Returneddate_DELETED = "sdds_returneddate";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Risk Level, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string RiskLevel = "sdds_level";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Satisfiedwithpersonsundertakingworks = "sdds_satisfiedwithpersonsundertakingworks";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Secretaryofstate = "sdds_secretaryofstate";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Section106_278 = "sdds_section106278";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Signaturetickbox_DONOTUSE = "sdds_signaturetickbox";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Single or Multiple year licence, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string SingleorMultipleyearlicence = "sdds_singleormultipleyearlicence";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 200, Format: Text</summary>
        public const string SiteOSGrid = "sdds_siteosgrid";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Sitesubjecttoanycommitment = "sdds_sitesubjecttoanycommitment";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Sitevisitneeded_DONOTUSE = "sdds_sitevisitneeded";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 1000, Format: Text</summary>
        public const string Sites = "sdds_sites";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2000</summary>
        public const string SLAExtension = "sdds_slaextension";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Outcome, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string SLAStatus = "sdds_slastatus";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string Slatimer = "sdds_slatimer";
        /// <summary>Type: State, RequiredLevel: SystemRequired, DisplayName: Status, OptionSetType: State</summary>
        public const string Status = "statecode";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string SubjecttoOtherApplicationDetails = "sdds_subjecttootherapplicationdetails";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string SubjecttoOtherApplications = "sdds_subjecttootherapplications";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string SupplementaryInformation = "sdds_supplementaryinformation";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Assessment Category, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string SurveyAssessment = "sdds_surveyassessment";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No/NA, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Takenactiontopreventproblemsoutlinedabove = "sdds_takenactiontopreventproblemsoutlinedabove";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Taskcompleted = "sdds_taskcompleted";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Url</summary>
        public const string TbCheckUrl = "sdds_tbcheckurl";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
        public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string TotalBillableLastUpdatedOn = "sdds_totalbillable2_date";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
        public const string TotalBillable = "sdds_totalbillable";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
        public const string TotalBillable1 = "sdds_totalbillable2";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
        public const string TotalBillableState = "sdds_totalbillable2_state";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
        public const string TotalDuration = "sdds_totalduration";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateAndTime, DateTimeBehavior: UserLocal</summary>
        public const string TotalPausedLastUpdatedOn = "sdds_totalpaused_date";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: 0, MaxValue: 2147483647</summary>
        public const string TotalPaused = "sdds_totalpaused";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -2147483648, MaxValue: 2147483647</summary>
        public const string TotalPausedState = "sdds_totalpaused_state";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Usestandardactionsandmethod_DONOTUSE = "sdds_usestandardactionsandmethod";
        /// <summary>Type: Integer, RequiredLevel: None, MinValue: -1, MaxValue: 2147483647</summary>
        public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
        /// <summary>Type: BigInt, RequiredLevel: None, MinValue: -9223372036854775808, MaxValue: 9223372036854775807</summary>
        public const string VersionNumber = "versionnumber";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: TextArea</summary>
        public const string whatis_arethesurveylicencenumber = "sdds_whatisarethesurveylicencenumber";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string whatworkisoutstanding = "sdds_whatworkisoutstanding";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
        public const string Whenwillthehighestimpactoccur = "sdds_whenwillthehighestimpactoccur";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Whenwillthisbecomplete = "sdds_whenwillthisbecomplete";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 100, Format: Text</summary>
        public const string Whichspecieswaspreviousapplicationabout = "sdds_previousapplicationspecies";
        /// <summary>Type: Memo, RequiredLevel: None, MaxLength: 4000</summary>
        public const string Whydoyouneedalicence_DONOTUSE = "sdds_whydoyouneedalicence";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Reason for no permission, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Whynopermissionrequired = "sdds_whynopermissionrequired";
        /// <summary>Type: Virtual, RequiredLevel: None, DisplayName: Why no reasoned statement, OptionSetType: Picklist</summary>
        public const string Whynoreasonedstatement = "sdds_whynoreasonedstatement";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Wildlife_relatedconviction = "sdds_wildliferelatedconviction";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Yes/No/NA, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Willanyactivityfallinand_or_DONOTUSE = "sdds_willactivityfallinoradjacenttosite";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: With regard to SSSI, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string WithregardtoSSSI_DONOTUSE = "sdds_withregardtosssi";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string workaffectroostoflocalimportance = "sdds_workaffectroostoflocalimportance";
        /// <summary>Type: Boolean, RequiredLevel: None, True: 1, False: 0, DefaultValue: False</summary>
        public const string Workbeprotectedscheduledmonumentworship = "sdds_workbeprotectedscheduledmonumentworship";
        /// <summary>Type: String, RequiredLevel: None, MaxLength: 4000, Format: TextArea</summary>
        public const string Yes_Badgersitecommitment = "sdds_yesbadgersitecommitment";
        /// <summary>Type: Picklist, RequiredLevel: None, DisplayName: Monitoring Method/Techniques, OptionSetType: Picklist, DefaultFormValue: -1</summary>
        public const string Yes_Monitoringmethods_techniques = "sdds_yesmonitoringmethodstechniques";
        /// <summary>Type: DateTime, RequiredLevel: None, Format: DateOnly, DateTimeBehavior: DateOnly</summary>
        public const string Yes_Monitoringtimings = "sdds_yesmonitoringtimings";

        #endregion Attributes

        #region Relationships

        /// <summary>Parent: "ProcessStage" Child: "Application" Lookup: "DeprecatedStageId"</summary>
        public const string RelM1_ApplicationDeprecatedStageId = "processstage_sdds_application";
        /// <summary>Parent: "Customer" Child: "Application" Lookup: "Applicant"</summary>
        public const string RelM1_ApplicationApplicant = "sdds_application_applicantid_Contact";
        /// <summary>Parent: "ApplicationPurpose" Child: "Application" Lookup: "ConfirmApplicationPurpose"</summary>
        public const string RelM1_ApplicationConfirmApplicationPurpose = "sdds_application_applicationpurpose_sdds_";
        /// <summary>Parent: "User" Child: "Application" Lookup: "Adviser"</summary>
        public const string RelM1_ApplicationAdviser = "sdds_application_assessorid_SystemUser";
        /// <summary>Parent: "Organisation" Child: "Application" Lookup: "ConsentingAuthority_DONOTUSE"</summary>
        public const string RelM1_ApplicationConsentingAuthority_DONOTUSE = "sdds_application_consentingauthorityid_Ac";
        /// <summary>Parent: "Customer" Child: "Application" Lookup: "Ecologist"</summary>
        public const string RelM1_ApplicationEcologist = "sdds_application_ecologistid_Contact";
        /// <summary>Parent: "Organisation" Child: "Application" Lookup: "EcologistOrganisation"</summary>
        public const string RelM1_ApplicationEcologistOrganisation = "sdds_application_ecologistorganisationid_";
        /// <summary>Parent: "Organisation" Child: "Application" Lookup: "ApplicantOrganisation"</summary>
        public const string RelM1_ApplicationApplicantOrganisation = "sdds_application_organisationid_Account";
        /// <summary>Parent: "User" Child: "Application" Lookup: "Returnedby_DELETED"</summary>
        public const string RelM1_ApplicationReturnedby_DELETED = "sdds_application_returnedbyuserid_SystemU";
        /// <summary>Parent: "User" Child: "Application" Lookup: "IssuedBy"</summary>
        public const string RelM1_ApplicationIssuedBy = "sdds_application_sdds_issuedbyid_SystemUs";
        /// <summary>Parent: "User" Child: "Application" Lookup: "LeadAdviser"</summary>
        public const string RelM1_ApplicationLeadAdviser = "sdds_application_leadadvisor_SystemUser";
        /// <summary>Parent: "Currency" Child: "Application" Lookup: "Currency"</summary>
        public const string RelM1_ApplicationCurrency = "TransactionCurrency_sdds_application";
        /// <summary>Parent: "User" Child: "Application" Lookup: "Allocator"</summary>
        public const string RelM1_ApplicationAllocator = "sdds_application_advisormanager_SystemUse";
        /// <summary>Parent: "ApplicationType" Child: "Application" Lookup: "Applicationtype"</summary>
        public const string RelM1_ApplicationApplicationtype = "sdds_ApplicationTypes_sdds_applicationtyp";
        /// <summary>Parent: "Customer" Child: "Application" Lookup: "_1stReferee"</summary>
        public const string RelM1_Application1stReferee = "sdds_application_firstreferee_Contact";
        /// <summary>Parent: "Customer" Child: "Application" Lookup: "_2ndReferee"</summary>
        public const string RelM1_Application2ndReferee = "sdds_application_secondReferee_Contact";
        /// <summary>Parent: "Site" Child: "Application" Lookup: "EnterSitehereDONOTUSE"</summary>
        public const string RelM1_ApplicationEnterSitehereDONOTUSE = "sdds_application_sdds_siteid_sdds_site";
        /// <summary>Parent: "Customer" Child: "Application" Lookup: "BillingCustomer"</summary>
        public const string RelM1_ApplicationBillingCustomer = "sdds_application_billingcustomerid_contac";
        /// <summary>Parent: "Organisation" Child: "Application" Lookup: "BillingOrganisation"</summary>
        public const string RelM1_ApplicationBillingOrganisation = "sdds_application_billingorganisationid_ac";
        /// <summary>Parent: "Customer" Child: "Application" Lookup: "Alternativeapplicantcontact"</summary>
        public const string RelM1_ApplicationAlternativeapplicantcontact = "sdds_application_alternativeapplicantcont";
        /// <summary>Parent: "Customer" Child: "Application" Lookup: "AlternativeEcologistcontact"</summary>
        public const string RelM1_ApplicationAlternativeEcologistcontact = "sdds_application_alternativeecologistcont";
        /// <summary>Parent: "EarnedRecognisationLicence" Child: "Application" Lookup: "EarnedRecognitionLicence"</summary>
        public const string RelM1_ApplicationEarnedRecognitionLicence = "sdds_earnedrecognisationlicence_sdds_earn_appl";
        /// <summary>Parent: "AssessmentCategoryLogic" Child: "Application" Lookup: "AssessmentCategoryLogic"</summary>
        public const string RelM1_ApplicationAssessmentCategoryLogic = "sdds_sdds_application_assessmentcategoryLogici";
        /// <summary>Entity 1: "Customer" Entity 2: "Application"</summary>
        public const string RelMM_sdds_application_Contact_Authorisedpersons = "sdds_application_Contact_Authorisedpersons";
        /// <summary>Entity 1: "Site" Entity 2: "Application"</summary>
        public const string RelMM_sdds_application_sdds_site_sdds_site = "sdds_application_sdds_site_sdds_site";
        /// <summary>Parent: "Application" Child: "FieldSharing" Lookup: ""</summary>
        public const string Rel1M_sdds_application_PrincipalObjectAttributeAccesses = "sdds_application_PrincipalObjectAttributeAccesses";
        /// <summary>Parent: "Application" Child: "Applicationprocess" Lookup: ""</summary>
        public const string Rel1M_bpf_sdds_application_sdds_applicationprocess = "bpf_sdds_application_sdds_applicationprocess";
        /// <summary>Parent: "Application" Child: "LicensableAction" Lookup: ""</summary>
        public const string Rel1M_sdds_licensableaction_applicationid_sdds_ = "sdds_licensableaction_applicationid_sdds_";
        /// <summary>Parent: "Application" Child: "ChargeRequest" Lookup: ""</summary>
        public const string Rel1M_sdds_PaymentItem_sdds_applicationid_sdds_ = "sdds_PaymentItem_sdds_applicationid_sdds_";
        /// <summary>Parent: "Application" Child: "Licence" Lookup: ""</summary>
        public const string Rel1M_sdds_license_sdds_applicationid_sdds_appl = "sdds_license_sdds_applicationid_sdds_appl";
        /// <summary>Parent: "Application" Child: "QueueItem" Lookup: ""</summary>
        public const string Rel1M_sdds_application_QueueItems = "sdds_application_QueueItems";
        /// <summary>Parent: "Application" Child: "Consultation" Lookup: ""</summary>
        public const string Rel1M_sdds_Consultation_sdds_application_sdds_a = "sdds_Consultation_sdds_application_sdds_a";
        /// <summary>Parent: "Application" Child: "PaymentRequest" Lookup: ""</summary>
        public const string Rel1M_sdds_paymentrequest_applicationid_sdds_ap = "sdds_paymentrequest_applicationid_sdds_ap";
        /// <summary>Parent: "Application" Child: "Note" Lookup: ""</summary>
        public const string Rel1M_sdds_application_Annotations = "sdds_application_Annotations";
        /// <summary>Parent: "Application" Child: "AssessmentInterview" Lookup: ""</summary>
        public const string Rel1M_sdds_assessmentinterview_sdds_application = "sdds_assessmentinterview_sdds_application";
        /// <summary>Parent: "Application" Child: "SiteVisit" Lookup: ""</summary>
        public const string Rel1M_sdds_sitevisit_sdds_applicationid_sdds_ap = "sdds_sitevisit_sdds_applicationid_sdds_ap";
        /// <summary>Parent: "Application" Child: "Permission" Lookup: ""</summary>
        public const string Rel1M_sdds_planningconsent_sdds_applicationid_s = "sdds_planningconsent_sdds_applicationid_s";
        /// <summary>Parent: "Application" Child: "WithdrawApplication" Lookup: ""</summary>
        public const string Rel1M_sdds_withdrawapplication_sdds_application = "sdds_withdrawapplication_sdds_application";
        /// <summary>Parent: "Application" Child: "EcologistExperience" Lookup: ""</summary>
        public const string Rel1M_sdds_ecologistexperience_sdds_application = "sdds_ecologistexperience_sdds_application";
        /// <summary>Parent: "Application" Child: "DesignatedSite" Lookup: ""</summary>
        public const string Rel1M_sdds_designatedsites_sdds_applicationid_s = "sdds_designatedsites_sdds_applicationid_s";
        /// <summary>Parent: "Application" Child: "EmailLicence" Lookup: ""</summary>
        public const string Rel1M_sdds_emaillicence_sdds_applicationid_sdds = "sdds_emaillicence_sdds_applicationid_sdds";
        /// <summary>Parent: "Application" Child: "ComplianceCheck" Lookup: ""</summary>
        public const string Rel1M_sdds_compliancecheck_sdds_applicationid_s = "sdds_compliancecheck_sdds_applicationid_s";
        /// <summary>Parent: "Application" Child: "ApplicationReport" Lookup: ""</summary>
        public const string Rel1M_sdds_sdds_applicationreport_Application_sdds_a = "sdds_sdds_applicationreport_Application_sdds_a";
        /// <summary>Parent: "Application" Child: "ReactivateCase" Lookup: ""</summary>
        public const string Rel1M_sdds_sdds_reactivatecase_applicationid_sdds_ap = "sdds_sdds_reactivatecase_applicationid_sdds_ap";
        /// <summary>Parent: "Application" Child: "AccrediationLevelSpecieRoost" Lookup: "Application"</summary>
        public const string Rel1M_AccrediationLevelSpecieRoostApplication = "sdds_siteregistrationsepcie_sdds_applicat";
        /// <summary>Parent: "Application" Child: "Survey" Lookup: ""</summary>
        public const string Rel1M_sdds_sdds_survey_applicationid_sdds_applicatio = "sdds_sdds_survey_applicationid_sdds_applicatio";
        /// <summary>Entity 1: "Application" Entity 2: "Membership"</summary>
        public const string RelMM_sdds_membership_sdds_application_sdds_app = "sdds_membership_sdds_application_sdds_app";
        /// <summary>Entity 1: "Application" Entity 2: "Qualification"</summary>
        public const string RelMM_sdds_qualification_sdds_application_sdds_ = "sdds_qualification_sdds_application_sdds_";
        /// <summary>Entity 1: "Application" Entity 2: "KnowledgeBaseRecord"</summary>
        public const string RelMM_sdds_application_knowledgebaserecord = "sdds_application_knowledgebaserecord";
        /// <summary>Entity 1: "Application" Entity 2: "KnowledgeArticle"</summary>
        public const string RelMM_msdyn_sdds_application_knowledgearticle = "msdyn_sdds_application_knowledgearticle";

        #endregion Relationships

        #region OptionSets

        public enum Whyisalicenceneeded_OptionSet
        {
            Capture_Take = 452120000,
            Disturb = 452120001,
            Transport = 452120002,
            DamageBreedingSite = 452120003,
            DestroyBreedingSite = 452120004,
            DamageRestingPlace = 452120005,
            DestroyRestingPlace = 452120006
        }
        public enum AppAssessment_OptionSet
        {
            Normal = 452120000,
            Targeted = 452120001,
            FullManual = 452120002
        }
        public enum ApplicantCommunicationPreference_OptionSet
        {
            Email = 100000000,
            Post = 100000001,
            Telephone = 100000002
        }
        public enum ApplicantthesameasBillingCustomer_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum ApplicationCategory_OptionSet
        {
            Agriculture_Farming_Fishing_Forestry_NatureConservation = 100000000,
            Archeologicalinvestigation = 100000002,
            BarnConversion = 100000004,
            Commercial = 100000019,
            Communications = 100000017,
            EnergygenerationandEnergySupply = 100000016,
            FloodandCoastaldefences = 100000007,
            Healthandsafety = 100000009,
            Heritage_Historicalconservation = 100000011,
            Householderhomeimprovements = 100000013,
            Residentialhousingnothouseholderimprovements = 100000001,
            IndustrialorManufacturing = 100000003,
            Listedbuildings = 452120002,
            Quarryingandmining = 100000005,
            NationallySignificantInfrastructureProjects = 100000018,
            Publicbuildingsandpublicland = 100000008,
            RegisteredPlacesofworship = 100000006,
            Scheduledmonuments = 452120004,
            Tourismandleisure = 100000010,
            Transportincludingroads = 100000012,
            Traditionalfarmbuildingsinacountrysidestewardshipagreement = 452120003,
            Wastemanagement = 100000014,
            Waterbodieswatersupplyandtreatment = 100000015,
            Other = 452120001
        }
        public enum ApplicationStatus_OptionSet
        {
            Received = 1,
            AwaitingAllocation = 100000000,
            Allocatedforassessment = 100000001,
            UnderAssessment = 100000002,
            Granted = 100000004,
            Paused = 100000005,
            NotGranted = 100000008,
            Inactive = 2,
            Withdrawn = 100000006,
            Closed = 452120001,
            ModificationinProgress = 452120002
        }
        public enum AreMinimumrequirementsforhabitatmanagement_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum AreNBCRMsbeingproposedinabat_accessiblearea_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum ASRAssessment_OptionSet
        {
            Normal = 452120000,
            Targeted = 452120001,
            FullManual = 452120002
        }
        public enum Assessingteam_DONOTUSE_OptionSet
        {
            DeliveryTeam = 100000000,
            CentralTeam = 100000001
        }
        public enum AssessmentCategory_OptionSet
        {
            Normal = 452120000,
            Targeted = 452120001,
            FullManual = 452120002
        }
        public enum AssessmentOutcome_OptionSet
        {
            Grant = 100000000,
            NotGrant = 100000001
        }
        public enum Assessmenttype_OptionSet
        {
            SiteVisit = 100000000,
            TelephoneAssessment = 100000001,
            DeskAssessment = 100000002
        }
        public enum Badgerreasonforexemption_OptionSet
        {
            Preservingpublichealthandsafety = 452120000,
            Homeimprovementthroughhouseholderplanningpermissionorpermitteddevelopment = 452120001,
            Preventingdamagetolivestockcropstimberorproperty = 452120002,
            Scientificresearchoreducationalpurposes = 452120004,
            Conservationofprotectedspeciesincludingprotectionofbatroosts = 452120005,
            Conservescheduledmonumentslistedbuildingsplacesofworshiportraditionalfarmbuildings = 452120006,
            Other = 452120007
        }
        public enum buildingconsentrequired_DONOTUSE_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum CentralTeamSLAStatus_OptionSet
        {
            Granted = 100000000,
            NotGranted = 100000001
        }
        public enum Consentgranted_DONOTUSE_OptionSet
        {
            FullPlanningPermission = 100000000,
            DemolitionconsentunderBuildingAct1984 = 100000001,
            ListedBuildingConsent = 100000002,
            HighwaysActConsent = 100000003,
            MineralConsent = 100000004,
            MineralConsentReviewofMineralPlanningPermissionsubmittedtoMi = 100000005,
            OutlinePlanningPermission = 100000006,
            ConservationAreaConsent = 100000007,
            TreePreservationOrder = 100000008,
            UtilitiesConsent = 100000009,
            MineralConsentwithReviewofMineralPlanningPermission = 100000010,
            Otherconsenttype = 100000011
        }
        public enum Consentobtained_DONOTUSE_OptionSet
        {
            Yes = 100000000,
            No = 100000001,
            NotApplicable = 100000002
        }
        public enum Consentrequired_DONOTUSE_OptionSet
        {
            Planning_related = 100000000,
            DemolitionconsentunderBuildingAct1984 = 100000001,
            Othertypeofconsentrequired = 100000002,
            PermittedDevelopmentunderTownandCountryPlanningAct1990 = 100000003,
            Noconsentrequired = 100000004
        }
        public enum Consultedspecialist_DONOTUSE_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum DemolitionConsents_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum DiseaseriskOutcome_OptionSet
        {
            NoRisk = 100000003,
            Lowrisk = 100000000,
            Mediumrisk = 100000001,
            Highrisk = 100000002
        }
        public enum Domitigationandcompensationproposalsmeetreq_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum DocumentationSentTo_DONOTUSE_OptionSet
        {
            Applicant = 100000000,
            NamedEcologist = 100000001
        }
        public enum EcologistCommunicationPreference_OptionSet
        {
            Email = 100000000,
            Post = 100000001,
            Telephone = 100000002
        }
        public enum FCSoutcome_OptionSet
        {
            Granted = 100000000,
            NotGranted = 100000001
        }
        public enum FullPlanning_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum HascasebeenseenbyNE_DONOTUSE_OptionSet
        {
            Yes = 100000000,
            No = 100000001,
            NotSure = 100000002
        }
        public enum Haveyoucompletedthechargerequest_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum HaveyouConsultatedNEfor_DONOTUSE_OptionSet
        {
            Yes = 100000000,
            No = 100000001,
            NotSure = 100000002
        }
        public enum Haveyouemailedthedecisiondocument_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Haveyoureceivedownerpermission_OptionSet
        {
            Yes = 100000000,
            No = 100000001,
            NotApplicable = 100000002
        }
        public enum Impactondesignated_protectedsite_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum IROPIoutcome_OptionSet
        {
            Granted = 100000000,
            NotGranted = 100000001
        }
        public enum Isamainsettimpacted_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Isitalivedig_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Istheproposedactionsproportionate_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Isthisaformalapplication_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Isthisasubsequentdraft_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum IsthisanNSIP_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Isthisphased_multiplot_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Licencerequiredforproposedactivity_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum ListedBuildingConsent_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum ModificationWhyismodificationrequired_OptionSet
        {
            ChangeofJointLicensee = 452120000,
            ChangeofLicensee = 452120001,
            Increaseinnumberofbats_thatchangesroosttypeimpactedandrequiresadditionalmitigationand_orcompensation = 452120002,
            Addorremoveroosttypeorspecies = 452120003,
            AddorremoveLicenceAnnexes = 452120004,
            ChangeAccreditationLeveloftheRegisteredSite = 452120005,
            Modifycompensationthatwillreduceroostingopportunitiesforthespecies = 452120006,
            Reducemonitoringsurveys = 452120007,
            Modifycompensationduetounexpectedfinds = 452120008,
            Other = 452120009
        }
        public enum NEWLShasundertakenthefollowing_DONOTUSE_OptionSet
        {
            Draftedadditionallicenceconditionsshouldalicencebegranted = 452120000,
            Proposedalicencenotetoconfirmconsenting_assentingrequirementsforSSSIs = 452120001,
            CompletedanHRALSERapidScreeningToollowerriskcasesorHRALSEScreeningusingthefulltemplatehigherriskcases = 452120002,
            DraftedanewHRAAppropriateAssessment = 452120003,
            UpdatedapreviousHRAorreferencedashadowHRAAppropriateAssessment = 452120004
        }
        public enum Non_standardmitigationandcompensationmeasures_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum NSAoutcome_OptionSet
        {
            Granted = 100000000,
            NotGranted = 100000001
        }
        public enum On_NexttoDesignatedsite_OptionSet
        {
            Yes = 100000000,
            No = 100000001,
            NotApplicable = 100000002
        }
        public enum OtherConsent_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum OutlinePlanning_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Overallconsentoutcome_DonotUse_OptionSet
        {
            Granted = 100000000,
            NotGranted = 100000001
        }
        public enum PermittedDevelopmentRights_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum PlanningConsentAssessment_OptionSet
        {
            Normal = 452120000,
            Targeted = 452120001,
            FullManual = 452120002
        }
        public enum PostImpactPopulationmonitoring_OptionSet
        {
            YesImonitoringproposed = 452120000,
            Yesmonitoringnotrequired = 452120001,
            No = 452120002
        }
        public enum PreferredContact_OptionSet
        {
            Applicant = 100000000,
            NamedEcologist = 100000001
        }
        public enum PreviousApplicationOutcome_DONOTUSE_OptionSet
        {
            Granted = 100000000,
            NotGranted = 100000001,
            AdviceOnly = 100000002,
            Deferred = 100000003,
            Notyetknown = 100000004
        }
        public enum PreviousDAS_PSS_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum PriorAdviceforthisapplication_OptionSet
        {
            DAS_discretionaryadvice = 100000000,
            PSS_pre_submissionscreening = 100000001
        }
        public enum Priority_OptionSet
        {
            _1 = 100000002,
            _2 = 100000001,
            _3 = 100000000,
            _4 = 100000003
        }
        public enum Proposedactionscouldresolveproblem_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum ReasonforAreateamconsultation_DONOTUSE_OptionSet
        {
            PotentialimpactonSSSInotifiedfeaturesidentified = 452120000,
            CheckwhetherseparateSSSIconsentisrequired = 452120001,
            HRAduetopotentialimpactonSPA_SAC_Ramsarsites_adviceonlikelysignificanteffectsLSE = 452120002,
            HRARequestinformaladviceonadraftAppropriateAssessment = 452120003,
            HRAStatutoryconsultationofAppropriateAssessmentconclusions = 452120004
        }
        public enum Reasonforpause_OptionSet
        {
            Compliance_Enforcementinvestigation_notifyonly = 100000000,
            Awaitinginformation_notifyandactiontoresubmit_clarifyetc = 100000001,
            AwaitingConsulteeresponse_notifyonly = 100000002,
            Agreedreviseddeadline_notifyonly = 100000003,
            Unabletocontactapplicant_notifyonlyandactiontocontactus = 100000004,
            Other = 100000005
        }
        public enum Relatedtoanypreviouslylicensedmitigation_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum RequestType_OptionSet
        {
            New = 452120000,
            Resubmission = 452120001,
            Modification = 452120002
        }
        public enum ReservedMatters_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Returnreceived_DELETED_OptionSet
        {
            Yes = 100000000,
            No = 100000001,
            NotApplicable = 100000002
        }
        public enum Returnsatisfactory_DONOTUSE_OptionSet
        {
            Yes = 100000000,
            No = 100000001,
            NotApplicable = 100000002
        }
        public enum RiskLevel_OptionSet
        {
            LowRisk = 452120000,
            HighRisk = 452120001
        }
        public enum Satisfiedwithpersonsundertakingworks_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Secretaryofstate_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum Section106_278_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum SingleorMultipleyearlicence_OptionSet
        {
            Single = 100000000,
            Multiple = 100000001
        }
        public enum SLAStatus_OptionSet
        {
            Granted = 100000000,
            NotGranted = 100000001
        }
        public enum Status_OptionSet
        {
            Active = 0,
            Inactive = 1
        }
        public enum SubjecttoOtherApplications_OptionSet
        {
            Yes = 100000000,
            No = 100000001
        }
        public enum SurveyAssessment_OptionSet
        {
            Normal = 452120000,
            Targeted = 452120001,
            FullManual = 452120002
        }
        public enum Takenactiontopreventproblemsoutlinedabove_OptionSet
        {
            Yes = 100000000,
            No = 100000001,
            NotApplicable = 100000002
        }
        public enum Whynopermissionrequired_OptionSet
        {
            Permitteddevelopment = 452120000,
            Healthandsafety = 452120001,
            Other = 452120002
        }
        public enum Whynoreasonedstatement_OptionSet
        {
            homeimprovementsandsmallscalehousingdevelopments = 100000000,
            conserveandprotectlistedbuildingsscheduledmonumentsorplaceso = 100000001,
            maintainrepairimprovepublicbuildingsordeveloppublicland = 100000002
        }
        public enum Willanyactivityfallinand_or_DONOTUSE_OptionSet
        {
            Yes = 100000000,
            No = 100000001,
            NotApplicable = 100000002
        }
        public enum WithregardtoSSSI_DONOTUSE_OptionSet
        {
            N_A_thelicensableactivityisnotproposedwithinaSSSIsiteboundary = 452120000,
            SSSI_norelevantORNECshavebeenidentified = 452120001,
            SSSI_ORNECShavebeenidentifiedandtheapplicantistheowner_occupierorauthorisedperson = 452120002,
            SSSI_ORNECShavebeenidentifiedandtheapplicantisathirdpartyasactingalone = 452120003,
            SSSI_theapplicantisaPublicbodyorotherSection28Gauthority = 452120004
        }
        public enum Yes_Monitoringmethods_techniques_OptionSet
        {
            N_A_Noneproposed = 452120000,
            Batboxchecks_physicalinspections = 452120001,
            Emergencesurveys = 452120002,
            Acousticmonitoring = 452120003,
            ALBST = 452120004,
            DNAanalysisofdroppings = 452120005,
            Dawnre_entry = 452120006,
            Buildinginternals_externalsinspections = 452120007,
            Infraredimaging = 452120008,
            Thermalimaging = 452120009,
            Aerialinspectionsinvolvingtrees = 452120010,
            Environmentalconditionseglightingairflowhumidityandtemperature = 452120011
        }

        #endregion OptionSets
    }
}
