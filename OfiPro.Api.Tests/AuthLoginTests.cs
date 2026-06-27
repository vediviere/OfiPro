using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OfiPro.Api.Tests;

public class AuthLoginTests : IClassFixture<OfiProWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthLoginTests(OfiProWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsBadRequest()
    {
        var request = new
        {
            email = "cliente@ofipro.com",
            password = "password_incorrecto"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/login", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsOkAndToken()
    {
        var uniqueEmail = $"test-client-{Guid.NewGuid()}@ofipro.com";
        var password = "TestPassword123!";

        var registerRequest = new
        {
            name = "Test",
            lastName = "Client",
            email = uniqueEmail,
            password,
            phone = "2220000000",
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

        var content = await loginResponse.Content.ReadAsStringAsync();

        using var json = JsonDocument.Parse(content);

        Assert.True(json.RootElement.TryGetProperty("token", out var token));
        Assert.False(string.IsNullOrWhiteSpace(token.GetString()));

        Assert.True(json.RootElement.TryGetProperty("email", out var email));
        Assert.Equal(uniqueEmail, email.GetString());
    }
}