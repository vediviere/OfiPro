using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.Common;
using OfiPro.Application.DTOs.Contract;
using OfiPro.Application.Interfaces.Services;
using System.Security.Claims;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet("mine")]
    public async Task<IActionResult> GetMyContracts([FromQuery] PaginationQueryDto query)
    {
        var userId = GetUserId();

        var contracts = await _contractService.GetMyContractsAsync(userId, query);

        return Ok(contracts);
    }

    [HttpGet("{contractId}")]
    public async Task<IActionResult> GetById(Guid contractId)
    {
        var userId = GetUserId();

        var contract = await _contractService.GetByIdAsync(contractId, userId);

        return Ok(contract);
    }

    [HttpPatch("{contractId}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid contractId,
        UpdateContractStatusDto request)
    {
        var userId = GetUserId();

        await _contractService.UpdateStatusAsync(contractId, userId, request);

        return Ok(new
        {
            message = "Estado de contratación actualizado correctamente."
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