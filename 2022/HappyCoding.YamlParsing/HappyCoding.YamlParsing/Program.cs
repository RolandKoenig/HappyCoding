using HappyCoding.YamlParsing.Cases;
using HappyCoding.YamlParsing.Cases.ObjectWithMultilineStrings;
using HappyCoding.YamlParsing.Cases.SimpleChildCollections;
using HappyCoding.YamlParsing.Cases.SimpleChildObjects;
using HappyCoding.YamlParsing.Cases.SimpleObject;

// Define cases
var cases = new CaseBase[]
{
    new SimpleObjectParser(),
    new SimpleChildObjectsParser(),
    new SimpleChildCollectionsParser(),
    new ObjectWithMultilineStringsParser()
};

// Process all cases
foreach (var actCase in cases)
{
    await actCase.ParseAsync();
}