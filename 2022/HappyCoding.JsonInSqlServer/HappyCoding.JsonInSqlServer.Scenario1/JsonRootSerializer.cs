using HappyCoding.JsonInSqlServer.JsonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HappyCoding.JsonInSqlServer.Scenario1
{
    public class JsonRootSerializer
    {
        private static IContractResolver s_contractResolver = new ReducePropertySizeContractResolver();
        private static JsonSerializerSettings s_serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = s_contractResolver
        };

        public static string SerializeToJson(JsonRoot rootObj, bool reducedPropertySize)
        {
            if (reducedPropertySize)
            {
                return JsonConvert.SerializeObject(rootObj, Formatting.None, s_serializerSettings);
            }
            else
            {
                return JsonConvert.SerializeObject(rootObj, Formatting.None);
            }
        }

        public static JsonRoot DeserializeFromValue(string value, bool reducedPropertySize)
        {
            if (reducedPropertySize)
            {
                return JsonConvert.DeserializeObject<JsonRoot>(value, s_serializerSettings);
            }
            else
            {
                return JsonConvert.DeserializeObject<JsonRoot>(value);
            }
        }
    }
}
