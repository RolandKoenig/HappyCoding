using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HappyCoding.YamlParsing.Cases.SimpleChildObjects;

public class SimpleChildObjectsParser : CaseBase
{
    public override async Task ParseAsync()
    {
        base.WriteCaseStart("SimpleChildObjects");
        
        var fullYamlString = await base.GetEmbeddedResourceStringAsync("SimpleChildObjects.yml");

        base.WriteYaml(fullYamlString);
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        var deserializedModel = deserializer.Deserialize<SimpleChildObjectsModel>(fullYamlString);

        base.WriteCaseEnd(deserializedModel);
    }
}