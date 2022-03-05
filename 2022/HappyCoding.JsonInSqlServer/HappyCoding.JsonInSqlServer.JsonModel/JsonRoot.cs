using System;
using System.Collections.Generic;

namespace HappyCoding.JsonInSqlServer.JsonModel
{
    public class JsonRoot
    {
        public string TestKey1 { get; set; }

        public string TestKeyWithLongerName2 { get; set; }

        public string TestKey3 { get; set; }

        public string TestKey4 { get; set; }

        public string TestKeyWithLongerName5 { get; set; }

        public string TestKeyWithLongerName6 { get; set; }

        public string TestKey7 { get; set; }

        public string TestKeyWithLongerName8 { get; set; }

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
