using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace HappyCoding.SpecFlowProject.Specs.Steps
{
    [Binding]
    public sealed class OtherCalculatorDefinitions
    {
        private Calculator _calculator;

        public OtherCalculatorDefinitions(Calculator calculator)
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
