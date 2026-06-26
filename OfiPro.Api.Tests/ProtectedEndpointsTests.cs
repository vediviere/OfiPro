using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OfiPro.Api.Tests;

public class ProtectedEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProtectedEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetNotifications_WithoutToken_ReturnsUnauthorized()
    {
        var response = await _client.GetAsync(
            "/api/notifications?pageNumber=1&pageSize=5");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}