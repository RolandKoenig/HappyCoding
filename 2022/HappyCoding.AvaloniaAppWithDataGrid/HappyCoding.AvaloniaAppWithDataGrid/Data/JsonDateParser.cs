using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HappyCoding.AvaloniaAppWithDataGrid.Data;
internal class JsonDateParser : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var strValue = reader.GetString();
        if(string.IsNullOrEmpty(strValue)){ return DateTime.MinValue; }

        return DateTime.Parse(strValue);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue("dd-MM-yyyy");
    }
}
