using System.Net;
using System.Net.Http.Json;

namespace OfiPro.Api.Tests;

public class SecurityValidationTests : IClassFixture<OfiProWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SecurityValidationTests(OfiProWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_WithWeakPassword_ReturnsBadRequest()
    {
        var request = new
        {
            name = "Test",
            lastName = "WeakPassword",
            email = $"weak-password-{Guid.NewGuid()}@ofipro.com",
            password = "12345678",
            phone = "2220000044",
            state = "Puebla",
            city = "Puebla"
        };

        var response = await _client.PostAsJsonAsync(
            "/api/auth/register",
            request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithHtmlInName_ReturnsBadRequest()
    {
        var request = new
        {
            name = "<script>alert('xss')</script>",
            lastName = "HtmlTest",
            email = $"html-register-{Guid.NewGuid()}@ofipro.com",
            password = "TestPassword123!",
            phone = "2220000045",
            state = "Puebla",
            city = "Puebla"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}