using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using OfiPro.Api.Tests.Helpers;

namespace OfiPro.Api.Tests;

public class RefreshTokenTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RefreshTokenTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RefreshToken_WithValidRefreshToken_ReturnsOkAndNewTokens()
    {
        var user = await TestAuthHelper.RegisterAndLoginClientAsync(_client, "test-refresh-valid");

        var request = new
        {
            refreshToken = user.RefreshToken
        };

        var response = await _client.PostAsJsonAsync("/api/auth/refresh-token", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();

        using var json = JsonDocument.Parse(content);

        Assert.True(json.RootElement.TryGetProperty("token", out var token));
        Assert.False(string.IsNullOrWhiteSpace(token.GetString()));

        Assert.True(json.RootElement.TryGetProperty("refreshToken", out var newRefreshToken));
        Assert.False(string.IsNullOrWhiteSpace(newRefreshToken.GetString()));

        Assert.NotEqual(user.RefreshToken, newRefreshToken.GetString());

        Assert.True(json.RootElement.TryGetProperty("refreshTokenExpiresAt", out _));
    }

    [Fact]
    public async Task RefreshToken_WithReusedRefreshToken_ReturnsBadRequest()
    {
        var user = await TestAuthHelper.RegisterAndLoginClientAsync(_client, "test-refresh-reuse");

        var request = new
        {
            refreshToken = user.RefreshToken
        };

        var firstResponse = await _client.PostAsJsonAsync("/api/auth/refresh-token", request);

        Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);

        var secondResponse = await _client.PostAsJsonAsync("/api/auth/refresh-token", request);

        Assert.Equal(HttpStatusCode.BadRequest, secondResponse.StatusCode);
    }

    [Fact]
    public async Task RevokeRefreshToken_WithValidRefreshToken_PreventsFutureRefresh()
    {
        var user = await TestAuthHelper.RegisterAndLoginClientAsync(_client, "test-refresh-revoke");

        var request = new
        {
            refreshToken = user.RefreshToken
        };

        var revokeResponse = await _client.PostAsJsonAsync("/api/auth/revoke-refresh-token", request);

        Assert.Equal(HttpStatusCode.OK, revokeResponse.StatusCode);

        var refreshResponse = await _client.PostAsJsonAsync("/api/auth/refresh-token", request);

        Assert.Equal(HttpStatusCode.BadRequest, refreshResponse.StatusCode);
    }

    [Fact]
    public async Task RefreshToken_WithInvalidRefreshToken_ReturnsBadRequest()
    {
        var request = new
        {
            refreshToken = "refresh-token-invalido"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/refresh-token", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}