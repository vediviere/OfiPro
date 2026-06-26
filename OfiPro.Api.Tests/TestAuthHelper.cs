using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace OfiPro.Api.Tests.Helpers;

public static class TestAuthHelper
{
    public static async Task<(string Email, string Token)> RegisterAndLoginClientAsync(
        HttpClient client,
        string emailPrefix)
    {
        var email = $"{emailPrefix}-{Guid.NewGuid()}@ofipro.com";
        var password = "TestPassword123!";

        var registerRequest = new
        {
            name = "Test",
            lastName = "User",
            email,
            password,
            phone = "2220000099",
            state = "Puebla",
            city = "Puebla"
        };

        var registerResponse = await client.PostAsJsonAsync(
            "/api/auth/register",
            registerRequest);

        if (registerResponse.StatusCode != HttpStatusCode.OK)
        {
            throw new InvalidOperationException(
                $"Register failed with status code {registerResponse.StatusCode}");
        }

        var loginRequest = new
        {
            email,
            password
        };

        var loginResponse = await client.PostAsJsonAsync(
            "/api/auth/login",
            loginRequest);

        if (loginResponse.StatusCode != HttpStatusCode.OK)
        {
            throw new InvalidOperationException(
                $"Login failed with status code {loginResponse.StatusCode}");
        }

        var loginContent = await loginResponse.Content.ReadAsStringAsync();

        using var loginJson = JsonDocument.Parse(loginContent);

        var token = loginJson.RootElement
            .GetProperty("token")
            .GetString();

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException("Login response did not contain a valid token.");
        }

        return (email, token);
    }
}