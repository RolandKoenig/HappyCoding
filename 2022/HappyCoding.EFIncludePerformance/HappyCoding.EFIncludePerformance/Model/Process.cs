using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.EFIncludePerformance.Model
{
    public class Process
    {
        public string ID { get; init; }

        public DateTimeOffset CreateTimestamp { get; init; }

        public int Field1 { get; init; }

        public int Field2 { get; init; }

        public int Field3 { get; init; }
        
        public string Field4 { get; init; }

        public string Field5 { get; init; }

        public  int Field6 { get; set; }

        public List<ProcessActivity> Activities { get; init; }
    }
}
