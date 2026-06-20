using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.Evidence;
using OfiPro.Application.Interfaces.Services;
using System.Security.Claims;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class EvidencesController : ControllerBase
{
    private readonly IEvidenceService _evidenceService;

    public EvidencesController(IEvidenceService evidenceService)
    {
        _evidenceService = evidenceService;
    }

    [HttpPost("contracts/{contractId:guid}/evidences")]
    public async Task<IActionResult> Create(
        Guid contractId,
        CreateEvidenceDto request)
    {
        var userId = GetUserId();

        var evidence = await _evidenceService.CreateAsync(
            userId,
            contractId,
            request);

        return Ok(evidence);
    }

    [HttpGet("contracts/{contractId:guid}/evidences")]
    public async Task<IActionResult> GetByContract(Guid contractId)
    {
        var userId = GetUserId();

        var evidences = await _evidenceService.GetByContractIdAsync(
            userId,
            contractId);

        return Ok(evidences);
    }

    [HttpDelete("evidences/{evidenceId:guid}")]
    public async Task<IActionResult> Delete(Guid evidenceId)
    {
        var userId = GetUserId();

        await _evidenceService.DeleteAsync(
            userId,
            evidenceId);

        return NoContent();
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