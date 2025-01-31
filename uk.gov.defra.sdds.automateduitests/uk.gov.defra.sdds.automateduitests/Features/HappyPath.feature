Feature: Happy Path
In order to provide excellent customer service
As a customer service
I want to arrange a follow up phone call with new customers
#Scenario: 1 Verify the priority not empty
# Given the SuperUser is logged into Licence power app
#  And I go to Admin home screen
#  And the user select the Allocators role
#  And 'sdds test user' is added to the selected role
#  Given the SuperUser is logged into Licence power app
#  And I go to Admin home screen
#  And 'sdds test user' is removed from the Allocators role
# Scenario Outline: 8Clean up
#  Given the SuperUser is logged into Licence power app
#  And I go to Admin home screen
#  And '<UserName>' is removed from the <UserRoles> role
#
# Examples: 
# | UserRoles             | UserName       |
# | Allocators            | sdds test user |
# | Income Enabling       | sdds test user |
# | Lead Wildlife Adviser | sdds test user |
# | Non NEWLS colleagues  | sdds test user |
# | Senior Adviser        | sdds test user |
# | Super Users           | sdds test user |
# | Wildlife Adviser      | sdds test user |
# | Wildlife Adviser      | sdds test user | 

Scenario Outline: 1 Verify that A24 application priority field is not empty using different RBAC roles
 Given the SuperUser is logged into Licence power app
 And I go to Admin home screen
 And the user select the <UserRoles> role
 And '<UserName>' is added to the selected role
 Given the User is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as A24 BAdger
 And I will populate the remaining "General" Tab details for A24
 And I click on Save
 And I will check priority is not null
 Given the SuperUser is logged into Licence power app
 And I go to Admin home screen
 And '<UserName>' is removed from the <UserRoles> role

 Examples: 
 | UserRoles             | UserName       |
 | Allocators            | sdds test user |
 | Lead Wildlife Adviser | sdds test user |
 | Senior Adviser        | sdds test user |
 | Super Users           | sdds test user |
 | Wildlife Adviser      | sdds test user |
 | Support Adviser      | sdds test user |
 

Scenario Outline:2 Verify User can generate A24 and A01 Licence
Given the SuperUser is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as <formtype> BAdger
 And I will populate the remaining "General" Tab details for <formtype>
 And I click on Save
 And I will progress from Application recieved to Assessment stage
 And I will populate application details tab
 And I will populate an existing site and new permission
 And I will add Licensable action for <formtype>
 And I add Licensable methods for <formtype>
 And I will populate the conservation section
 And I will populate and complete the ecologist experience section
 And I will populate and complete the technical assessment section for <formtype>
 And I will populate and complete the decision section
 And I will complete the Assessment stage checklist with Licence Grant outcome
 When I click on generate document
 Then I will validate the url of the opened Licence document
 Then I will validate generate 'Grant' licence email page

 Examples: 
 | formtype |
 | A24      |
 | A01      |
 | A25      |
 #| A26      |



Scenario Outline: 3 Verify User can generate email for refused A24 application
Given the SuperUser is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as <formtype> BAdger
 And I will populate the remaining "General" Tab details for <formtype>
 And I click on Save
 And I will progress from Application recieved to Assessment stage
 And I will populate application details tab
 And I will populate an existing site and new permission
 And I will add Licensable action for <formtype>
 And I add Licensable methods for <formtype>
 And I will populate the conservation section
 And I will populate and complete the ecologist experience section
 And I will populate and complete the technical assessment section for <formtype>
 And I will populate and complete the decision section
 And I will complete the Assessment stage checklist with Licence Not Grant outcome
 When I click on generate document
 Then I will validate generate 'Not Grant' licence email page

  Examples: 
 | formtype |
 | A24      |
 | A01      |
 #| A26      |
  | A25      |

Scenario Outline: 4 Verify User can pause and resume A24 application
Given the SuperUser is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as <formtype> BAdger
 And I will populate the remaining "General" Tab details for <formtype>
 And I click on Save
 And I will progress from Application recieved to Assessment stage
 And I pause the application
 And I will resume the application
 And I will validate the application is Resumed

  Examples: 
 | formtype |
 | A24      |
 | A01      |
 #| A26      |
  | A25      |


Scenario Outline: 5 Verify User can withdraw an A24 and A01 application
Given the SuperUser is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as <formtype> BAdger
 And I will populate the remaining "General" Tab details for <formtype>
 And I click on Save
 And I will progress from Application recieved to Assessment stage
 And I will withdraw the application
 Then I will verify the case status is "Withdrawn"

  Examples: 
 | formtype |
 | A24      |
 | A01      |
 #| A26      |
  | A25      |

 Scenario Outline: 6 Verify User can do a return of Action for A24 and A01
 Given the SuperUser is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as <formtype> BAdger
 And I will populate the remaining "General" Tab details for <formtype>
 And I click on Save
 And I will progress from Application recieved to Assessment stage
 And I will populate application details tab
 And I will populate an existing site and new permission
 And I will add Licensable action for <formtype>
 And I add Licensable methods for <formtype>
 And I will populate the conservation section
 And I will populate and complete the ecologist experience section
 And I will populate and complete the technical assessment section for <formtype>
 And I will populate and complete the decision section
 And I will complete the Assessment stage checklist with Licence Grant outcome
 And I will click on decision tab
 And I will populate the general tab of return
 And I will populate the <formtype> badger tab of return

  Examples: 
 | formtype |
 | A24      |
 | A01      |
 #| A26      |
  | A25      |

 Scenario Outline:7 Verify User can trigger compliance
Given the SuperUser is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as <formtype> BAdger
 And I will populate the remaining "General" Tab details for <formtype>
 And I click on Save
 And I will progress from Application recieved to Assessment stage
 And I will populate application details tab
 And I will populate an existing site and new permission
 And I will add Licensable action for <formtype>
 And I add Licensable methods for <formtype>
 And I will populate the conservation section
 And I will populate and complete the ecologist experience section
 And I will populate and complete the technical assessment section for <formtype>
 And I will populate and complete the decision section
 And I will complete the Assessment stage checklist with Licence Grant outcome
 And I will click on decision tab to add a new compliance
 And I will complete the Compliance check entry BPF

  Examples: 
 | formtype |
 | A24      |
 | A01      |
 #| A26      |
 | A25      |





 Scenario Outline: 8 Verify User can create charge request for A01 and A24
Given the SuperUser is logged into Licence power app
 And I go to Applications home screen
 And I click on New (plus sign) to add a new application
 And I select Application Type as <formtype> BAdger
 And I will populate the remaining "General" Tab details for <formtype>
 And I click on Save
 And I will progress from Application recieved to Assessment stage
 And I will populate application details tab
 And I will populate an existing site and new permission
 And I will add Licensable action for <formtype>
 And I add Licensable methods for <formtype>
 And I will populate the conservation section
 #And I will populate and complete disease risk assessment section for <formtype>
 And I will populate and complete the ecologist experience section
 And I will populate and complete the technical assessment section for <formtype>
 And I will populate and complete the decision section
 And I will complete the Assessment stage checklist with Licence Grant outcome
 When I click on generate document
 Then I will validate the url of the opened Licence document
 Then I will validate generate 'Grant' licence email page
 And I will create a charge request
 #Then I will validate completed charge request notification text
  
 Examples: 
 | formtype |
 | A24      |
 | A01      |
 #| A26      |
  | A25      |
