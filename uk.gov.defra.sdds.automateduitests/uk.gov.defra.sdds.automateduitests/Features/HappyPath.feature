Feature: Happy Path

In order to provide excellent customer service
As a customer service
I want to arrange a follow up phone call with new customers

Scenario: Verify User can generate Licence

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
And I will complete the Assessment stage checklist with Licence granted outcome 
When I click on generate document
Then I will validate the url of the opened Licence document
And I will validate generate licence page

