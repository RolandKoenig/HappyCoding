using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HappyCoding.YamlParsing.Cases.ObjectWithMultilineStrings;

public class ObjectWithMultilineStringsParser : CaseBase
{
    public override async Task ParseAsync()
    {
        base.WriteCaseStart("ObjectWithMultilineStrings");
        
        var fullYamlString = await base.GetEmbeddedResourceStringAsync("ObjectWithMultilineStrings.yaml");

        base.WriteYaml(fullYamlString);
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        var deserializedModel = deserializer.Deserialize<ObjectWithMultilineStringsModel>(fullYamlString);

        base.WriteCaseEnd(deserializedModel);
    }
}