using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace HappyCoding.SpecFlowProject.Specs.Hooks
{
    [Binding]
    public class GeneralHook
    {
        [AfterScenario]
        public void AfterScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var featureTitle = featureContext.FeatureInfo.Title;
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;

            Console.WriteLine($"Feature: {featureTitle}, Scenario: {scenarioTitle}");
        }
    }
}
