using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HappyCoding.YamlParsing.Cases.YamlWithReferences;

public class YamlWithReferencesParser : CaseBase
{
    public override async Task ParseAsync()
    {
        base.WriteCaseStart("YamlWithReferences");
        
        var fullYamlString = await base.GetEmbeddedResourceStringAsync("YamlWithReferences.yml");

        base.WriteYaml(fullYamlString);
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        var deserializedModel = deserializer.Deserialize<YamlWithReferencesModel>(fullYamlString);

        base.WriteCaseEnd(deserializedModel);
    }
}