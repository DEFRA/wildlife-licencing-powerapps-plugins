Feature: A01-happypath

A short summary of the feature

Scenario: A01-Verify the priority not empty
 Given the User is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as A01 Badger
 And I will populate the remaining "General" Tab details for A01 Badger
 And I click on Save
 And I will check priority is not null

 Scenario: A01-Verify User can withdraw an application
 Given the User is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as A01 Badger
 And I will populate the remaining "General" Tab details for A01 Badger
 And I click on Save
 And I wiil progress from Application recieved to Assessment stage
 And I will withdraw the application
 Then I will verify the case status is "Withdrawn"
