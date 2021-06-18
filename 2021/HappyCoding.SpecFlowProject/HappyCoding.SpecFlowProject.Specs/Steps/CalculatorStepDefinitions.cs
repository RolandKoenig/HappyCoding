using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HappyCoding.SpecFlowProject.Specs.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly ScenarioContext _scenarioContext;
        private readonly Calculator _calculator;
        private int _result;

        public CalculatorStepDefinitions(ScenarioContext scenarioContext, Calculator calculator)
        {
            _calculator = calculator;
            _scenarioContext = scenarioContext;
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            _calculator.FirstNumber = number;
        }

        [Given("the first number is again (.*)")]
        public void GivenTheFirstNumberIsAgain(int number)
        {
            _calculator.FirstNumber = number;
        }

        [Given(@"the values from the following table")]
        public void GivenTheValuesFromTheFollowingTable(Table table)
        {
            _calculator.FirstNumber = table.Rows[0].GetInt32("Value1");
            _calculator.SecondNumber = table.Rows[0].GetInt32("Value2");
        }


        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            _calculator.SecondNumber = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            _result = _calculator.Add();
        }

        [When("the two numbers are multiplied")]
        public void WhenTheTwoNumbersAreMultiplied()
        {
            _result = _calculator.Multiply();
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            Assert.AreEqual(_result, result, "result");
        }
    }
}
