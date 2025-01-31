Feature: Feedback

In order to provide excellent customer service
As a customer service
I want to arrange a follow up phone call with new customers

Scenario: 1 Verify User can submit a feedback
 Given the SuperUser is logged into Licence power app
 And I go to Feedback home screen
 #And I navigate to Feedback Application and select Application
 And I click on New (plus sign) to add a new application
 And I select satisfied rating
 #And I will enter text on 'How can we improve this service?' field
 And I click on Save feedback
 #And i will validate the created feedback