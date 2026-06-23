using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.Interfaces.Services;
using System.Security.Claims;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("client/summary")]
    public async Task<IActionResult> GetClientSummary()
    {
        var userId = GetUserId();

        var summary = await _dashboardService.GetClientSummaryAsync(userId);

        return Ok(summary);
    }

    [HttpGet("contractor/summary")]
    public async Task<IActionResult> GetContractorSummary()
    {
        var userId = GetUserId();

        var summary = await _dashboardService.GetContractorSummaryAsync(userId);

        return Ok(summary);
    }

    [HttpGet("modes")]
    public async Task<IActionResult> GetAvailableModes()
    {
        var userId = GetUserId();

        var modes = await _dashboardService.GetAvailableModesAsync(userId);

        return Ok(modes);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetUserContext()
    {
        var userId = GetUserId();

        var context = await _dashboardService.GetUserContextAsync(userId);

        return Ok(context);
    }

    [HttpGet("admin/summary")]
    public async Task<IActionResult> GetAdminSummary()
    {
        var userId = GetUserId();

        var summary = await _dashboardService.GetAdminSummaryAsync(userId);

        return Ok(summary);
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Token inválido.");
        }

        return userId;
    }
}