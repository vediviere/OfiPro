using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OfiPro.Api.Tests;

public class PaginatedProjectsTests : IClassFixture<OfiProWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PaginatedProjectsTests(OfiProWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProjects_WithValidToken_ReturnsOkAndPaginatedResponse()
    {
        var uniqueEmail = $"test-projects-{Guid.NewGuid()}@ofipro.com";
        var password = "TestPassword123!";

        var registerRequest = new
        {
            name = "Test",
            lastName = "Projects",
            email = uniqueEmail,
            password,
            phone = "2220000003",
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

        var response = await _client.GetAsync(
            "/api/projects?pageNumber=1&pageSize=5&sortBy=createdAt&sortDirection=desc");

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