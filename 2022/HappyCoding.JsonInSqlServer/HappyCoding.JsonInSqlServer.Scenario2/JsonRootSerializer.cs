using System.IO;
using System.IO.Compression;
using System.Text;
using HappyCoding.JsonInSqlServer.JsonModel;
using Newtonsoft.Json;

namespace HappyCoding.JsonInSqlServer.Scenario2
{
    public class JsonRootSerializer
    {
        public static byte[] SerializeToJson(JsonRoot rootObj)
        {
            return Zip(JsonConvert.SerializeObject(rootObj, Formatting.None));
        }

        public static JsonRoot DeserializeFromValue(byte[] value)
        {
            return JsonConvert.DeserializeObject<JsonRoot>(UnZip(value));
        }

        private static byte[] Zip(string value)
        {
            // Transform string into byte[]  
            var byteArray = Encoding.UTF8.GetBytes(value);

            // Prepare for compress
            using var ms = new MemoryStream();
            using var sw = new GZipStream(ms, CompressionLevel.Optimal);

            // Compress
            sw.Write(byteArray, 0, byteArray.Length);
            sw.Close();

            // Transform byte[] zip data to string
            return ms.ToArray();
        }

        private static string UnZip(byte[] value)
        {
            // Prepare for decompress
            using var ms = new MemoryStream(value);
            using var sr = new GZipStream(ms, CompressionMode.Decompress);

            // Decompress
            var byteBuffer = new byte[50000];
            var readLength = sr.Read(byteBuffer, 0, byteBuffer.Length);
            return Encoding.UTF8.GetString(byteBuffer, 0, readLength);
        }
    }
}
