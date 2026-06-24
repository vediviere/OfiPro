using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.ProfessionalProfile;
using OfiPro.Application.Interfaces.Services;
using System.Security.Claims;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class ProfessionalProfilesController : ControllerBase
{
    private readonly IProfessionalProfileService _professionalProfileService;

    public ProfessionalProfilesController(IProfessionalProfileService professionalProfileService)
    {
        _professionalProfileService = professionalProfileService;
    }

    [HttpPost("professional-profiles")]
    public async Task<IActionResult> Create(CreateProfessionalProfileDto request)
    {
        var userId = GetUserId();

        var profile = await _professionalProfileService.CreateAsync(userId, request);

        return Ok(profile);
    }

    [HttpGet("professional-profiles/me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = GetUserId();

        var profile = await _professionalProfileService.GetMyProfileAsync(userId);

        return Ok(profile);
    }

    [HttpPut("professional-profiles/me")]
    public async Task<IActionResult> Update(UpdateProfessionalProfileDto request)
    {
        var userId = GetUserId();

        var profile = await _professionalProfileService.UpdateAsync(userId, request);

        return Ok(profile);
    }

    [HttpGet("contractors")]
    public async Task<IActionResult> SearchContractors([FromQuery] ContractorSearchQueryDto query)
    {
        var contractors = await _professionalProfileService.SearchContractorsAsync(query);

        return Ok(contractors);
    }

    [HttpGet("contractors/{userId:guid}")]
    public async Task<IActionResult> GetPublicContractorProfile(Guid userId)
    {
        var contractor = await _professionalProfileService.GetPublicContractorProfileAsync(userId);

        return Ok(contractor);
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