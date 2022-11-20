using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HappyCoding.YamlParsing.Cases.ObjectWithDictionary;

public class ObjectWithDictionaryParser : CaseBase
{
    public override async Task ParseAsync()
    {
        base.WriteCaseStart("ObjectWithDictionary");
        
        var fullYamlString = await base.GetEmbeddedResourceStringAsync("ObjectWithDictionary.yaml");

        base.WriteYaml(fullYamlString);
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        var deserializedModel = deserializer.Deserialize<ObjectWithDictionaryModel>(fullYamlString);

        base.WriteCaseEnd(deserializedModel);
    }
}