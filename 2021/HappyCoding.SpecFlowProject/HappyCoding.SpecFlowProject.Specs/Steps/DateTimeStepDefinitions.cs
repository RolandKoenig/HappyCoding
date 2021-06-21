using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace HappyCoding.SpecFlowProject.Specs.Steps
{
    [Binding]
    public sealed class DateTimeStepDefinitions
    {
        private DateTime _dateValue;

        [Given(@"The date from today")]
        public void GivenTheDateFromToday()
        {
            _dateValue = DateTime.Today;
        }
        
        [When(@"We add (.*) more days")]
        public void WhenWeAddMoreDays(int days)
        {
            _dateValue = _dateValue.AddDays(days);
        }
        
        [Then(@"We get a date (.*)")]
        public void ThenWeGetADateDaysFromCurrentDate(DateTime compareDateTime)
        {
            Assert.AreEqual(
                _dateValue,
                compareDateTime);
        }

    }
}
