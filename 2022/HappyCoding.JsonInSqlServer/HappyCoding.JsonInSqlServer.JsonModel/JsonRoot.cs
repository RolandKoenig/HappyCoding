using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.JsonInSqlServer.JsonModel
{
    public class JsonRoot
    {
        public string TestKey1 { get; set; }

        public string TestKey2 { get; set; }

        public string TestKey3 { get; set; }

        public string TestKey4 { get; set; }

        public string TestKey5 { get; set; }

        public string TestKey6 { get; set; }

        public string TestKey7 { get; set; }

        public string TestKey8 { get; set; }

        public string TestKey9 { get; set; }

        public List<JsonChild> Childs { get; set; } = new List<JsonChild>();

        public static JsonRoot CreateByRandom(Random random)
        {
            var result = new JsonRoot();
            result.TestKey1 = random.Next(1, 100000).ToString("D10");
            result.TestKey2 = random.Next(1, 100000).ToString("D8");
            result.TestKey3 = random.Next(1, 100000).ToString("D8");
            result.TestKey4 = random.Next(1, 100000).ToString("D2");
            result.TestKey5 = random.Next(1, 100000).ToString("D10");
            result.TestKey6 = random.Next(1, 100000).ToString("D10");
            result.TestKey7 = random.Next(1, 100000).ToString("D8");
            result.TestKey8 = random.Next(1, 100000).ToString("D8");
            result.TestKey9 = random.Next(1, 100000).ToString("D10");

            var childCount = random.Next(5, 20);
            result.Childs = new List<JsonChild>(childCount);
            for (var loop = 0; loop < childCount; loop++)
            {
                result.Childs.Add(new JsonChild()
                {
                    TestKey1 = random.Next(1, 100000).ToString("D10"),
                    TestKey2 = random.Next(1, 100000).ToString("D8"),
                    TestKey3 = random.Next(1, 100000).ToString("D8"),
                    TestKey4 = random.Next(1, 100000).ToString("D2"),
                    TestKey5 = random.Next(1, 100000).ToString("D10"),
                    TestKey6 = random.Next(1, 100000).ToString("D10"),
                    TestKey7 = random.Next(1, 100000).ToString("D8"),
                    TestKey8 = random.Next(1, 100000).ToString("D8"),
                    TestKey9 = random.Next(1, 100000).ToString("D10"),
                });
            }

            return result;
        }
    }
}
