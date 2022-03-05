using HappyCoding.JsonInSqlServer.JsonDotNetUtil;
using HappyCoding.JsonInSqlServer.JsonModel;
using Newtonsoft.Json;

namespace HappyCoding.JsonInSqlServer.Scenario1
{
    public class JsonRootSerializer
    {
        private static readonly JsonSerializerSettings s_serializerSettings;

        static JsonRootSerializer()
        {
            var contractResolver = new ReducePropertySizeContractResolver();
            contractResolver.MapPropertyForType<JsonRoot>()
                .MapPropertyName(x => x.TestKey1, "p1")
                .MapPropertyName(x => x.TestKeyWithLongerName2, "p2")
                .MapPropertyName(x => x.TestKey3, "p3")
                .MapPropertyName(x => x.TestKey4, "p4")
                .MapPropertyName(x => x.TestKeyWithLongerName5, "p5")
                .MapPropertyName(x => x.TestKeyWithLongerName6, "p6")
                .MapPropertyName(x => x.TestKey7, "p7")
                .MapPropertyName(x => x.TestKeyWithLongerName8, "p8")
                .MapPropertyName(x => x.TestKey9, "p9")
                .MapPropertyName(x => x.Childs, "c1");
            contractResolver.MapPropertyForType<JsonChild>()
                .MapPropertyName(x => x.TestKey1, "p1")
                .MapPropertyName(x => x.TestKeyWithLongerName2, "p2")
                .MapPropertyName(x => x.TestKeyWithLongerName3, "p3")
                .MapPropertyName(x => x.TestKey4, "p4")
                .MapPropertyName(x => x.TestKey5, "p5")
                .MapPropertyName(x => x.TestKey6, "p6")
                .MapPropertyName(x => x.TestKeyWithLongerName7, "p7")
                .MapPropertyName(x => x.TestKeyWithLongerName8, "p8")
                .MapPropertyName(x => x.TestKeyWithLongerName9, "p9");

            s_serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            };
        }

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
