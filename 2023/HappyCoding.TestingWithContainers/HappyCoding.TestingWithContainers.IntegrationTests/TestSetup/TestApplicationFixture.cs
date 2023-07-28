using Microsoft.AspNetCore.Mvc.Testing;

namespace HappyCoding.TestingWithContainers.IntegrationTests.TestSetup;

public class TestApplicationFixture : WebApplicationFactory<Program>
{

}

[CollectionDefinition(nameof(TestEnvironmentCollection))]
public class TestEnvironmentCollection : ICollectionFixture<TestApplicationFixture> { }