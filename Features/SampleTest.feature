Feature: SampleTest


Scenario:Test1_Launch Browser and Open PHPTravels Website

	Given Chrome Browser is Launched
	When I go to PHPTravels Page
	Then I see PHPTravels Page is Opened
	And  I Close the browser 

Scenario Outline:Test2_Create an Instant Demo Request Form

	Given Chrome Browser is Launched
	And I go to PHPTravels Page
	When I Try to create an Instant Demo Request with '<FirstName>' 'first_name' '<LastName>' 'last_name' '<BusinessName>' 'business_name' '<Email>' 'email'
	Then I see Submit 'demo' button is disabled
	And  I Close the browser 

	Examples: 
	| FirstName | LastName | BusinessName | Email               |
	| abc       | efg      | Automation   | abc.123@getnada.com |

Scenario:Test3_Collect all the Links in the Page and Navigate to specific page
	
	Given Chrome Browser is Launched
	And I go to PHPTravels Page
	When I Collect all 'a' Links on the Page
	Then I filter 'Order' and click on the link
	And I see Plans and Prices page displayed
	And I click on 'Docs' link on the child window
	Then I Close the browser 


