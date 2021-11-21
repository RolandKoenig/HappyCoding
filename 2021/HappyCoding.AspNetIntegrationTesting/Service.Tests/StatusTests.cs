using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Service.Api;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.Tests
{
    public class StatusTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public StatusTests(WebApplicationFactory<Program> fixture)
        {
            _httpClient = fixture.CreateClient();
        }

        [Fact]
        public async Task DoSomething()
        {
            var httpResult = await _httpClient.GetAsync("api/status");
            Assert.True(httpResult.IsSuccessStatusCode);
        }
    }
}
