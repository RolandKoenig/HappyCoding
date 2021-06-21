Feature: DateTimeCalculations

Scenario: Dummy scenario which utilizes a CustomArgumentTransformer for DateTime
	Given The date from today
	When We add 5 more days 
	Then We get a date 5 days from current date