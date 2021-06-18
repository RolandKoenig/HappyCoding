Feature: Calculator
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](Testing.SpecFlowProject.Specs2/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

Scenario: Add two numbers again
	Given the first number is again 50
	And the second number is 120
	When the two numbers are added
	Then the result should be 170

Scenario: Multiply two numbers
	Given the first number is 10
	And the second number is 10
	When the two numbers are multiplied
	Then the result should be 100

Scenario: Add Values from a table
    Given the values from the following table
	| Value1 | Value2 |
	| 30     | 20     |
	When the two numbers are added
	Then the result should be 50

Scenario Outline: Some add examples
	Given the first number is <Value1>
	And the second number is <Value2>
	When the two numbers are added
	Then the result should be <Result>
	Examples:
		| Value1 | Value2 | Result |
		| 10     | 20     | 30     |
		| 50     | 70     | 120    |
		| 30     | 30     | 60     |

Scenario: Add two numbers with step from other StepDefinitions file
	Given the first number is again 50
	And the second number is 120 from other StepDefinition file
	When the two numbers are added
	Then the result should be 170