using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;

namespace HappyCoding.EFCoreFeatures.Util;

internal static class JsonModelSerializer
{
    private static JsonSerializer s_serializer = new JsonSerializer()
    {
        Formatting = Formatting.None,
        NullValueHandling = NullValueHandling.Ignore
    };

    public static byte[] SerializeJsonModel<T>(T jsonModel)
    {
        using var stringWriter = new StringWriter();
        s_serializer.Serialize(stringWriter, jsonModel);

        return Zip(stringWriter.ToString());
    }

    public static T DeserializeJsonModel<T>(byte[] rawBytes)
        where T : class
    {
        using var jsonReader = new JsonTextReader(UnZip(rawBytes));

        var result = s_serializer.Deserialize<T>(jsonReader);
        if (result == null)
        {
            throw new InvalidOperationException("Unable to deserialize given byte array!");
        }

        return result;
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
