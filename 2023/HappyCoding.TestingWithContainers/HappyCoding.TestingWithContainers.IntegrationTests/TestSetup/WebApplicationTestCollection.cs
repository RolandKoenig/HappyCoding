namespace HappyCoding.TestingWithContainers.IntegrationTests.TestSetup;

[CollectionDefinition(nameof(WebApplicationTestCollection))]
public class WebApplicationTestCollection : ICollectionFixture<WebApplicationFixture>
{
}