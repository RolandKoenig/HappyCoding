using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using HappyCoding.WpfWithMaterialDesign.Util;
using PropertyTools.DataAnnotations;

namespace HappyCoding.WpfWithMaterialDesign.Data
{
    public class TestDataRow : PropertyChangedBase
    {
        private const string CAT_TEST_DATA = "Test Data";

        private string _name = string.Empty;
        private string _address = string.Empty;
        private string _company = string.Empty;
        private uint _age = 0;
        private uint _countCars = 0;

        [ReadOnly(true)]
        [Browsable(false)]
        public int ID { get; set; } = 0;

        [Category(CAT_TEST_DATA)]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.RaisePropertyChanged(nameof(this.Name));
                }
            }
        }

        [Category(CAT_TEST_DATA)]
        public string Address 
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    this.RaisePropertyChanged(nameof(this.Address));
                }
            }
        }

        [Category(CAT_TEST_DATA)]
        public string Company
        {
            get => _company;
            set
            {
                if (_company != value)
                {
                    _company = value;
                    this.RaisePropertyChanged(nameof(this.Company));
                }
            }
        }

        [Category(CAT_TEST_DATA)]
        [Slidable(0, 120)]
        public uint Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    this.RaisePropertyChanged(nameof(this.Age));
                }
            }
        }

        [Category(CAT_TEST_DATA)]
        [Spinnable(1, 2)]
        public uint CountCars
        {
            get => _countCars;
            set
            {
                if (_countCars != value)
                {
                    _countCars = value;
                    this.RaisePropertyChanged(nameof(this.CountCars));
                }
            }
        }

        public static IEnumerable<TestDataRow> CreateTestData(int countRows)
        {
            var faker = new Faker<TestDataRow>()
                .RuleFor(row => row.ID, gen => gen.IndexFaker)
                .RuleFor(row => row.Name, gen => gen.Name.FullName())
                .RuleFor(row => row.Address, gen => gen.Address.FullAddress())
                .RuleFor(row => row.Company, gen => gen.Company.CompanyName())
                .RuleFor(row => row.Age, gen => gen.Random.UInt(5, 55))
                .RuleFor(row => row.CountCars, gen => gen.Random.UInt(0, 2));
            return faker.Generate(countRows);
        }
    }

}
