using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using HappyCoding.WpfWithMaterialDesign.Util;

namespace HappyCoding.WpfWithMaterialDesign.Data
{
    public class TestDataRow : PropertyChangedBase
    {
        [ReadOnly(true)]
        public int ID { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public uint Age { get; set; }

        public static IEnumerable<TestDataRow> CreateTestData()
        {
            var faker = new Faker<TestDataRow>()
                .RuleFor(row => row.ID, gen => gen.IndexFaker)
                .RuleFor(row => row.Name, gen => gen.Name.FullName())
                .RuleFor(row => row.Age, gen => gen.Random.UInt(5, 55));
            return faker.Generate(150);
        }
    }

}
