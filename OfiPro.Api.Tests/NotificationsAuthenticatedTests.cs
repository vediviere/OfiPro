using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using OfiPro.Api.Tests.Helpers;

namespace OfiPro.Api.Tests;

public class NotificationsAuthenticatedTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public NotificationsAuthenticatedTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetNotifications_WithValidToken_ReturnsOkAndPaginatedResponse()
    {
        var user = await TestAuthHelper.RegisterAndLoginClientAsync(
            _client,
            "test-notifications");

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", user.Token);

        var response = await _client.GetAsync(
            "/api/notifications?pageNumber=1&pageSize=5&sortBy=createdAt&sortDirection=desc");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();

        using var json = JsonDocument.Parse(content);

        Assert.True(json.RootElement.TryGetProperty("items", out _));
        Assert.True(json.RootElement.TryGetProperty("pageNumber", out _));
        Assert.True(json.RootElement.TryGetProperty("pageSize", out _));
        Assert.True(json.RootElement.TryGetProperty("totalItems", out _));
        Assert.True(json.RootElement.TryGetProperty("totalPages", out _));
        Assert.True(json.RootElement.TryGetProperty("hasPreviousPage", out _));
        Assert.True(json.RootElement.TryGetProperty("hasNextPage", out _));
    }
}