using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace HappyCoding.SpecFlowProject.Specs
{
    [Binding]
    public class CustomArgumentTransformer
    {
        [StepArgumentTransformation(@"(\d+) days from current date")]
        public DateTime DayAdderTransformer(int days)
        {
            return DateTime.Today.AddDays(days);
        }
    }
}
