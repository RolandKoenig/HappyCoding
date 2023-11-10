namespace HappyCoding.TestingWithContainers.SystemTests.TestSetup;

[CollectionDefinition(nameof(TestEnvironmentCollection))]
public class TestEnvironmentCollection : ICollectionFixture<TestEnvironmentFixture> { }
