using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.JsonInSqlServer.JsonModel;
using Newtonsoft.Json;

namespace HappyCoding.JsonInSqlServer.Scenario1
{
    public class JsonRootSerializer
    {
        public static string SerializeToJson(JsonRoot rootObj)
        {
            return JsonConvert.SerializeObject(rootObj, Formatting.None);
        }

        public static JsonRoot DeserializeFromValue(string value)
        {
            return JsonConvert.DeserializeObject<JsonRoot>(value);
        }

    }
}
