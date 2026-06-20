using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.User;
using OfiPro.Application.Interfaces.Services;
using System.Security.Claims;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = GetUserId();

        var profile = await _userService.GetProfileAsync(userId);

        return Ok(profile);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateUserProfileDto request)
    {
        var userId = GetUserId();

        var profile = await _userService.UpdateProfileAsync(userId, request);

        return Ok(profile);
    }

    [HttpPatch("activate-contractor")]
    public async Task<IActionResult> ActivateContractor()
    {
        var userId = GetUserId();

        await _userService.ActivateContractorAsync(userId);

        return Ok(new
        {
            message = "Perfil de contratista activado."
        });
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        return Ok(user);
    }

    [HttpPatch("{id}/activate")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Activate(Guid id)
    {
        await _userService.ActivateAsync(id);

        return Ok(new
        {
            message = "Usuario activado."
        });
    }

    [HttpPatch("{id}/deactivate")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        await _userService.DeactivateAsync(id);

        return Ok(new
        {
            message = "Usuario desactivado."
        });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteAsync(id);

        return Ok(new
        {
            message = "Usuario eliminado."
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