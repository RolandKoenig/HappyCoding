using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.JsonInSqlServer.JsonModel
{
    public class JsonChild
    {
        [PropertyShortName("p1")]
        public string TestKey1 { get; set; }

        [PropertyShortName("p2")]
        public string TestKeyWithLongerName2 { get; set; }

        [PropertyShortName("p3")]
        public string TestKeyWithLongerName3 { get; set; }

        [PropertyShortName("p4")]
        public string TestKey4 { get; set; }

        [PropertyShortName("p5")]
        public string TestKey5 { get; set; }

        [PropertyShortName("p6")]
        public string TestKey6 { get; set; }

        [PropertyShortName("p7")]
        public string TestKeyWithLongerName7 { get; set; }

        [PropertyShortName("p8")]
        public string TestKeyWithLongerName8 { get; set; }

        [PropertyShortName("p9")]
        public string TestKeyWithLongerName9 { get; set; }
    }
}
