using HappyCoding.BinarySerializers.Model;
using HappyCoding.BinarySerializers.ProtobufModel;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using MemoryPack;
using MessagePack;
using MessagePack.Resolvers;
using ProtoBuf;
using System.Text.Json;

namespace HappyCoding.BinarySerializers;

public static class SerializationScenarios
{
    public static byte[] SerializeWithMemoryPack(ExportData exportData)
    {
        return MemoryPackSerializer.Serialize(exportData);
    }

    public static byte[] SerializeWithProtobuf(ExportData exportData)
    {
        using var memoryStream = new MemoryStream();
        Serializer.Serialize(memoryStream, exportData);
        return memoryStream.ToArray();
    }

    public static byte[] SerializeWithGoogleProtobuf(ExportData exportData)
    {
        var protobufMessage = new ExportDataMessage
        {
            Version = exportData.Version,
            ExportZeitstempel = Timestamp.FromDateTime(exportData.ExportZeitstempel.ToUniversalTime())
        };

        foreach (var currentRow in exportData.Rows)
        {
            protobufMessage.Rows.Add(new ExportDataRowMessage
            {
                Kategorie = currentRow.Kategorie,
                Thema = currentRow.Thema,
                Datum = currentRow.Datum,
                TagTyp = currentRow.TagTyp,
                ZeilenTyp = currentRow.ZeilenTyp,
                Zeitaufwand = currentRow.Zeitaufwand,
                Abgerechnet = currentRow.Abgerechnet,
                BillingMultiplier = currentRow.BillingMultiplier,
                Kommentar = currentRow.Kommentar
            });
        }
        
        return protobufMessage.ToByteArray();
    }

    public static byte[] SerializeWithSystemTextJson(ExportData exportData)
    {
        return JsonSerializer.SerializeToUtf8Bytes(exportData);
    }

    public static byte[] SerializeWithMessagePack(ExportData exportData)
    {
        var options = MessagePackSerializerOptions.Standard.WithResolver(ContractlessStandardResolver.Instance);
        return MessagePackSerializer.Serialize(exportData, options);
    }
}