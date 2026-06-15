using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.User;
using OfiPro.Application.Interfaces;
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
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new
            {
                message = "Token inválido."
            });
        }

        var profile = await _userService.GetProfileAsync(userId);

        return Ok(profile);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateUserProfileDto request)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new
            {
                message = "Token inválido."
            });
        }

        var profile = await _userService.UpdateProfileAsync(userId, request);

        return Ok(profile);
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
}