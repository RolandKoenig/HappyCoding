using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.JsonInSqlServer.JsonModel
{
    public class JsonRoot
    {
        [PropertyShortName("p1")]
        public string TestKey1 { get; set; }

        [PropertyShortName("p2")]
        public string TestKeyWithLongerName2 { get; set; }

        [PropertyShortName("p3")]
        public string TestKey3 { get; set; }

        [PropertyShortName("p4")]
        public string TestKey4 { get; set; }

        [PropertyShortName("p5")]
        public string TestKeyWithLongerName5 { get; set; }

        [PropertyShortName("p6")]
        public string TestKeyWithLongerName6 { get; set; }

        [PropertyShortName("p7")]
        public string TestKey7 { get; set; }

        [PropertyShortName("p8")]
        public string TestKeyWithLongerName8 { get; set; }

        [PropertyShortName("p9")]
        public string TestKey9 { get; set; }

        public List<JsonChild> Childs { get; set; } = new List<JsonChild>();

        public static JsonRoot CreateByRandom(Random random)
        {
            var result = new JsonRoot();
            result.TestKey1 = random.Next(1, 100000).ToString("D10");
            result.TestKeyWithLongerName2 = random.Next(1, 100000).ToString("D8");
            result.TestKey3 = random.Next(1, 100000).ToString("D8");
            result.TestKey4 = random.Next(1, 100000).ToString("D2");
            result.TestKeyWithLongerName5 = random.Next(1, 100000).ToString("D10");
            result.TestKeyWithLongerName6 = random.Next(1, 100000).ToString("D10");
            result.TestKey7 = random.Next(1, 100000).ToString("D8");
            result.TestKeyWithLongerName8 = random.Next(1, 100000).ToString("D8");
            result.TestKey9 = random.Next(1, 100000).ToString("D10");

            var childCount = random.Next(80, 120);
            result.Childs = new List<JsonChild>(childCount);
            for (var loop = 0; loop < childCount; loop++)
            {
                result.Childs.Add(new JsonChild()
                {
                    TestKey1 = random.Next(1, 100000).ToString("D10"),
                    TestKeyWithLongerName2 = random.Next(1, 100000).ToString("D8"),
                    TestKeyWithLongerName3 = random.Next(1, 100000).ToString("D8"),
                    TestKey4 = random.Next(1, 100000).ToString("D2"),
                    TestKey5 = random.Next(1, 100000).ToString("D10"),
                    TestKey6 = random.Next(1, 100000).ToString("D10"),
                    TestKeyWithLongerName7 = random.Next(1, 100000).ToString("D8"),
                    TestKeyWithLongerName8 = random.Next(1, 100000).ToString("D8"),
                    TestKeyWithLongerName9 = random.Next(1, 100000).ToString("D10"),
                });
            }

            return result;
        }
    }
}
