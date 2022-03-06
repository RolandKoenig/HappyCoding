using System.IO;
using System.IO.Compression;
using System.Text;
using HappyCoding.JsonInSqlServer.JsonDotNetUtil;
using HappyCoding.JsonInSqlServer.JsonModel;
using Newtonsoft.Json;

namespace HappyCoding.JsonInSqlServer.Scenario2
{
    public class JsonRootSerializer
    {
        private static readonly JsonSerializerSettings s_serializerSettings;
        private static readonly JsonSerializer s_serializer;
        private static readonly JsonSerializer s_serializerWithReduceProperties;

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

            s_serializerSettings = new JsonSerializerSettings();
            s_serializerSettings.ContractResolver = contractResolver;

            s_serializer = new JsonSerializer();
            s_serializerWithReduceProperties = new JsonSerializer();
            s_serializerWithReduceProperties.ContractResolver = contractResolver;
        }

        public static byte[] SerializeToJson(JsonRoot rootObj, bool reducedPropertySize)
        {
            if (reducedPropertySize)
            {
                return Zip(JsonConvert.SerializeObject(rootObj, Formatting.None, s_serializerSettings));
            }
            else
            {
                return Zip(JsonConvert.SerializeObject(rootObj, Formatting.None));
            }
        }

        public static JsonRoot DeserializeFromValue(byte[] value, bool reducedPropertySize)
        {
            if (reducedPropertySize)
            {
                using var jsonReader = new JsonTextReader(UnZip(value));
                return s_serializer.Deserialize<JsonRoot>(jsonReader);
            }
            else
            {
                using var jsonReader = new JsonTextReader(UnZip(value));
                return s_serializerWithReduceProperties.Deserialize<JsonRoot>(jsonReader);
            }
        }

        private static byte[] Zip(string value)
        {
            // Transform string into byte[]  
            var byteArray = Encoding.UTF8.GetBytes(value);

            // Prepare for compress
            using var ms = new MemoryStream((int)(byteArray.Length * 0.6));
            using var sw = new GZipStream(ms, CompressionLevel.Optimal);

            // Compress
            sw.Write(byteArray, 0, byteArray.Length);
            sw.Close();

            // Transform byte[] zip data to string
            return ms.ToArray();
        }

        private static StreamReader UnZip(byte[] value)
        {
            // Prepare for decompress
            var ms = new MemoryStream(value);
            var sr = new GZipStream(ms, CompressionMode.Decompress);
            return new StreamReader(sr);
        }
    }
}
