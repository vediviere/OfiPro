using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.Common.Pagination;
using OfiPro.Application.DTOs.Invitation;
using OfiPro.Application.Interfaces.Services;
using System.Security.Claims;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class InvitationsController : ControllerBase
{
    private readonly IInvitationService _invitationService;

    public InvitationsController(IInvitationService invitationService)
    {
        _invitationService = invitationService;
    }

    [HttpPost("projects/{projectId:guid}/invitations")]
    public async Task<IActionResult> Create(Guid projectId, CreateInvitationDto request)
    {
        var userId = GetUserId();

        var invitation = await _invitationService.CreateAsync(userId, projectId, request);

        return Ok(invitation);
    }

    [HttpGet("invitations/sent")]
    public async Task<IActionResult> GetSent([FromQuery] PaginationQueryDto query)
    {
        var userId = GetUserId();

        var invitations = await _invitationService.GetSentAsync(userId, query);

        return Ok(invitations);
    }

    [HttpGet("invitations/received")]
    public async Task<IActionResult> GetReceived([FromQuery] PaginationQueryDto query)
    {
        var userId = GetUserId();

        var invitations = await _invitationService.GetReceivedAsync(userId, query);

        return Ok(invitations);
    }

    [HttpPatch("invitations/{invitationId:guid}/accept")]
    public async Task<IActionResult> Accept(Guid invitationId)
    {
        var userId = GetUserId();

        await _invitationService.AcceptAsync(userId, invitationId);

        return Ok(new { message = "Invitación aceptada." });
    }

    [HttpPatch("invitations/{invitationId:guid}/reject")]
    public async Task<IActionResult> Reject(Guid invitationId)
    {
        var userId = GetUserId();

        await _invitationService.RejectAsync(userId, invitationId);

        return Ok(new { message = "Invitación rechazada." });
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