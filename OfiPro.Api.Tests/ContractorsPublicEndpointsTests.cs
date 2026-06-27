using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OfiPro.Api.Tests;

public class ContractorsPublicEndpointsTests : IClassFixture<OfiProWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ContractorsPublicEndpointsTests(OfiProWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetContractors_WithoutToken_ReturnsOkAndPaginatedResponse()
    {
        var response = await _client.GetAsync(
            "/api/contractors?pageNumber=1&pageSize=5&sortBy=createdAt&sortDirection=desc");

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

    [Fact]
    public async Task GetContractors_WithFiltersWithoutToken_ReturnsOkAndPaginatedResponse()
    {
        var response = await _client.GetAsync(
            "/api/contractors?specialty=plomer%C3%ADa&pageNumber=1&pageSize=5&sortBy=experience&sortDirection=desc");

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