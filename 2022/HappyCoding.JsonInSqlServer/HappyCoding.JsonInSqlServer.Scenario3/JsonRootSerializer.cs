using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using HappyCoding.JsonInSqlServer.JsonModel;

namespace HappyCoding.JsonInSqlServer.Scenario3
{
    public class JsonRootSerializer
    {
        public static byte[] SerializeToJson(JsonRoot rootObj, bool reducedPropertySize)
        {
            return Zip(JsonSerializer.SerializeToUtf8Bytes(rootObj));
        }

        public static JsonRoot DeserializeFromValue(byte[] value, bool reducedPropertySize)
        {
            return JsonSerializer.Deserialize<JsonRoot>(UnZip(value));
        }

        private static byte[] Zip(byte[] byteArray)
        {
            // Prepare for compress
            using var ms = new MemoryStream();
            using var sw = new GZipStream(ms, CompressionLevel.Optimal);

            // Compress
            sw.Write(byteArray, 0, byteArray.Length);
            sw.Close();

            // Transform byte[] zip data to string
            return ms.ToArray();
        }

        private static byte[] UnZip(byte[] value)
        {
            // Prepare for decompress
            using var ms = new MemoryStream(value);
            using var sr = new GZipStream(ms, CompressionMode.Decompress);

            // Decompress
            var byteBuffer = new byte[50000];
            var readLength = sr.Read(byteBuffer, 0, byteBuffer.Length);

            var result = new byte[readLength];
            Array.Copy(byteBuffer, result, readLength);
            return result;
        }
    }
}
