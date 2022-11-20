using HappyCoding.YamlParsing.Cases;
using HappyCoding.YamlParsing.Cases.SimpleObject;
using HappyCoding.YamlParsing.Cases.SimpleChildObjects;

// Define cases
var cases = new CaseBase[]
{
    new SimpleObjectParser(),
    new SimpleChildObjectsParser()
};

// Process all cases
foreach (var actCase in cases)
{
    await actCase.ParseAsync();
}