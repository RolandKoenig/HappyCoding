namespace HappyCoding.AspNetTestWithPlaywright.Tests.Util;

[CollectionDefinition(nameof(TestEnvironmentCollection))]
public class TestEnvironmentCollection : ICollectionFixture<WebHostServerFixture<Startup>> { }