Feature: Happy Path

In order to provide excellent customer service
As a customer service
I want to arrange a follow up phone call with new customers

Scenario: 1Verify User can generate Licence1

Given the User is logged into Licence power app
And I go to Applications home screen
And I click on New (plus sign) to add a new application
And I select Application Type as A24 BAdger 
And I will populate the remaining "General" Tab details
And I click on Save 
And I will check priority is not null

Scenario: 2Verify User can generate Licence

Given the User is logged into Licence power app
And I go to Applications home screen
And I click on New (plus sign) to add a new application
And I select Application Type as A24 BAdger 
And I will populate the remaining "General" Tab details
And I click on Save 
And I wiil progress from Application recieved to Assessment stage
And I will populate application details tab
And I will populate an existing site and new permission
And I will populate the conservation section
And I will populate and complete disease risk assessment section
And I will populate and complete the ecologist experience section
And I will populate and complete the technical assessment section
And I will populate and complete the decision section
And I will complete the Assessment stage checklist with Licence Grant outcome 
When I click on generate document
Then I will validate the url of the opened Licence document
Then I will validate generate 'Grant' licence email page


Scenario: 3Verify User can generate email for refused application

Given the User is logged into Licence power app
And I go to Applications home screen
And I click on New (plus sign) to add a new application
And I select Application Type as A24 BAdger 
And I will populate the remaining "General" Tab details
And I click on Save 
And I wiil progress from Application recieved to Assessment stage
And I will populate application details tab
And I will populate an existing site and new permission
And I will populate the conservation section
And I will populate and complete disease risk assessment section
And I will populate and complete the ecologist experience section
And I will populate and complete the technical assessment section
And I will populate and complete the decision section
And I will complete the Assessment stage checklist with Licence Not Grant outcome 
When I click on generate document
Then I will validate generate 'Not Grant' licence email page

#Scenario: 4Verify User can generate Licence
#
#Given the User is logged into Licence power app
#And I go to Applications home screen
#And I click on New (plus sign) to add a new application
#And I select Application Type as A24 BAdger 
#And I will populate the remaining "General" Tab details
#And I click on Save 
#And I pause the application



Scenario: 5Verify User can generate Licence

Given the User is logged into Licence power app
And I go to Applications home screen
And I click on New (plus sign) to add a new application
And I select Application Type as A24 BAdger 
And I will populate the remaining "General" Tab details
And I click on Save 
And I wiil progress from Application recieved to Assessment stage
And I pause the application
And I will resume the application
And I will validate the application is Resumed


Scenario: 6Verify User can generate Licence

Given the User is logged into Licence power app
And I go to Applications home screen
And I click on New (plus sign) to add a new application
And I select Application Type as A24 BAdger 
And I will populate the remaining "General" Tab details
And I click on Save 
And I wiil progress from Application recieved to Assessment stage
And I will withdraw the application
Then I will verify the case status is "Withdrawn"



Scenario: 7Verify User can do a return

Given the User is logged into Licence power app
And I go to Applications home screen
And I click on New (plus sign) to add a new application
And I select Application Type as A24 BAdger 
And I will populate the remaining "General" Tab details
And I click on Save 
And I wiil progress from Application recieved to Assessment stage
And I will populate application details tab
And I will populate an existing site and new permission
And I will populate the conservation section
And I will populate and complete disease risk assessment section
And I will populate and complete the ecologist experience section
And I will populate and complete the technical assessment section
And I will populate and complete the decision section
And I will complete the Assessment stage checklist with Licence Grant outcome 
And I will click on decision tab
And I will populate the general tab of return
And I will populate the A24 badger tab of return
#When I click on generate document
#Then I will validate the url of the opened Licence document
#Then I will validate generate 'Grant' licence email page

#And I will search for the licence number

