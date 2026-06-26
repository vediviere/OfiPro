using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OfiPro.Api.Tests;

public class AuthenticatedEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthenticatedEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUserProfile_WithValidToken_ReturnsOk()
    {
        var uniqueEmail = $"test-profile-{Guid.NewGuid()}@ofipro.com";
        var password = "TestPassword123!";

        var registerRequest = new
        {
            name = "Test",
            lastName = "Profile",
            email = uniqueEmail,
            password,
            phone = "2220000001",
            state = "Puebla",
            city = "Puebla"
        };

        var registerResponse = await _client.PostAsJsonAsync(
            "/api/auth/register",
            registerRequest);

        Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

        var loginRequest = new
        {
            email = uniqueEmail,
            password
        };

        var loginResponse = await _client.PostAsJsonAsync(
            "/api/auth/login",
            loginRequest);

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        var loginContent = await loginResponse.Content.ReadAsStringAsync();

        using var loginJson = JsonDocument.Parse(loginContent);

        var token = loginJson.RootElement
            .GetProperty("token")
            .GetString();

        Assert.False(string.IsNullOrWhiteSpace(token));

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var profileResponse = await _client.GetAsync("/api/users/profile");

        Assert.Equal(HttpStatusCode.OK, profileResponse.StatusCode);

        var profileContent = await profileResponse.Content.ReadAsStringAsync();

        using var profileJson = JsonDocument.Parse(profileContent);

        Assert.True(profileJson.RootElement.TryGetProperty("email", out var email));
        Assert.Equal(uniqueEmail, email.GetString());
    }
}