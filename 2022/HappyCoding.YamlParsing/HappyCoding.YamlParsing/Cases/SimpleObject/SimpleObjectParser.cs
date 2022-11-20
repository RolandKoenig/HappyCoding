using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HappyCoding.YamlParsing.Cases.SimpleObject;

public class SimpleObjectParser : CaseBase
{
    public override async Task ParseAsync()
    {
        base.WriteCaseStart("SimpleObject");
        
        var fullYamlString = await base.GetEmbeddedResourceStringAsync("SimpleObject.yaml");

        base.WriteYaml(fullYamlString);
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        var deserializedModel = deserializer.Deserialize<SimpleObjectModel>(fullYamlString);

        base.WriteCaseEnd(deserializedModel);
    }
}