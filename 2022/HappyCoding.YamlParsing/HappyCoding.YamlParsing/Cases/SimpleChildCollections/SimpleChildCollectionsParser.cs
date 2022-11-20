using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HappyCoding.YamlParsing.Cases.SimpleChildCollections;

public class SimpleChildCollectionsParser : CaseBase
{
    public override async Task ParseAsync()
    {
        base.WriteCaseStart("SimpleChildCollections");
        
        var fullYamlString = await base.GetEmbeddedResourceStringAsync("SimpleChildCollections.yaml");

        base.WriteYaml(fullYamlString);
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        var deserializedModel = deserializer.Deserialize<SimpleChildCollectionsModel>(fullYamlString);

        base.WriteCaseEnd(deserializedModel);
    }
}