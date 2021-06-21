using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace HappyCoding.SpecFlowProject.Specs.Steps
{
    [Binding]
    public sealed class OtherCalculatorStepDefinitions
    {
        private Calculator _calculator;

        public OtherCalculatorStepDefinitions(Calculator calculator)
        {
            _calculator = calculator;
        }

        [Given("the second number is (.*) from other StepDefinition file")]
        public void GivenTheSecondNumberIs(int number)
        {
            _calculator.SecondNumber = number;
        }
    }
}
