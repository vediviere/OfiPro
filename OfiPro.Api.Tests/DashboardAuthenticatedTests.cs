using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using OfiPro.Api.Tests.Helpers;

namespace OfiPro.Api.Tests;

public class DashboardAuthenticatedTests : IClassFixture<OfiProWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DashboardAuthenticatedTests(OfiProWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetDashboardModes_WithValidToken_ReturnsOkAndClientMode()
    {
        var user = await TestAuthHelper.RegisterAndLoginClientAsync(
            _client,
            "test-dashboard");

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", user.Token);

        var response = await _client.GetAsync("/api/dashboard/modes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();

        using var json = JsonDocument.Parse(content);

        Assert.True(json.RootElement.TryGetProperty("userId", out _));
        Assert.True(json.RootElement.TryGetProperty("canUseClientMode", out var canUseClientMode));
        Assert.True(canUseClientMode.GetBoolean());

        Assert.True(json.RootElement.TryGetProperty("canUseContractorMode", out _));
        Assert.True(json.RootElement.TryGetProperty("canUseAdminMode", out _));
        Assert.True(json.RootElement.TryGetProperty("availableModes", out _));
        Assert.True(json.RootElement.TryGetProperty("defaultMode", out _));
    }
}