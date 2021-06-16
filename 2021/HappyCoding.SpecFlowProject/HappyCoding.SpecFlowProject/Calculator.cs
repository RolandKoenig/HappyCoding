using System;

namespace HappyCoding.SpecFlowProject
{
    public class Calculator
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public int Add()
        {
            return this.FirstNumber + this.SecondNumber;
        }

        public int Multiply()
        {
            return this.FirstNumber * this.SecondNumber;
        }
    }
}
