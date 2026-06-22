using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.Rating;
using OfiPro.Application.Interfaces.Services;
using System.Security.Claims;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class RatingsController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingsController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpPost("contracts/{contractId:guid}/ratings")]
    public async Task<IActionResult> Create(Guid contractId, CreateRatingDto request)
    {
        var userId = GetUserId();

        var rating = await _ratingService.CreateAsync(userId, contractId, request);

        return Ok(rating);
    }

    [HttpGet("contracts/{contractId:guid}/ratings")]
    public async Task<IActionResult> GetByContract(Guid contractId)
    {
        var userId = GetUserId();

        var ratings = await _ratingService.GetByContractIdAsync(userId, contractId);

        return Ok(ratings);
    }

    [HttpGet("users/{userId:guid}/reputation")]
    public async Task<IActionResult> GetUserReputation(Guid userId)
    {
        var reputation = await _ratingService.GetUserReputationAsync(userId);

        return Ok(reputation);
    }

    [HttpGet("users/{userId:guid}/ratings/received")]
    public async Task<IActionResult> GetReceivedByUser(Guid userId)
    {
        var ratings = await _ratingService.GetReceivedByUserIdAsync(userId);

        return Ok(ratings);
    }

    [HttpGet("users/{userId:guid}/ratings/public")]
    public async Task<IActionResult> GetPublicReceivedByUser(Guid userId)
    {
        var ratings = await _ratingService.GetPublicReceivedByUserIdAsync(userId);

        return Ok(ratings);
    }

    [HttpGet("users/{userId:guid}/reputation/public")]
    public async Task<IActionResult> GetPublicUserReputation(Guid userId)
    {
        var reputation = await _ratingService.GetPublicUserReputationAsync(userId);

        return Ok(reputation);
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