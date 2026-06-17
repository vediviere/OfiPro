using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.Proposal;
using OfiPro.Application.Interfaces;
using System.Security.Claims;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProposalsController : ControllerBase
{
    private readonly IProposalService _proposalService;

    public ProposalsController(IProposalService proposalService)
    {
        _proposalService = proposalService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProposalDto request)
    {
        var userId = GetUserId();

        var proposal = await _proposalService.CreateAsync(userId, request);

        return Ok(proposal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProposalDto request)
    {
        var userId = GetUserId();

        var proposal = await _proposalService.UpdateAsync(userId, id, request);

        return Ok(proposal);
    }

    [HttpGet("my-proposals")]
    public async Task<IActionResult> GetMyProposals()
    {
        var userId = GetUserId();

        var proposals = await _proposalService.GetMyProposalsAsync(userId);

        return Ok(proposals);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var proposal = await _proposalService.GetByIdAsync(id);

        return Ok(proposal);
    }

    [HttpGet("requirement/{projectRequirementId}")]
    public async Task<IActionResult> GetByProjectRequirement(Guid projectRequirementId)
    {
        var proposals = await _proposalService.GetByProjectRequirementAsync(projectRequirementId);

        return Ok(proposals);
    }

    [HttpPatch("{id}/accept")]
    public async Task<IActionResult> Accept(Guid id)
    {
        await _proposalService.AcceptAsync(id);

        return Ok(new
        {
            message = "Propuesta aceptada."
        });
    }

    [HttpPatch("{id}/reject")]
    public async Task<IActionResult> Reject(Guid id)
    {
        await _proposalService.RejectAsync(id);

        return Ok(new
        {
            message = "Propuesta rechazada."
        });
    }

    [HttpPatch("{id}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid id)
    {
        var userId = GetUserId();

        await _proposalService.WithdrawAsync(userId, id);

        return Ok(new
        {
            message = "Propuesta retirada."
        });
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