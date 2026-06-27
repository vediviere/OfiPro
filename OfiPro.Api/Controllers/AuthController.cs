using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.Auth;
using OfiPro.Application.Interfaces.Services;
using Microsoft.AspNetCore.RateLimiting;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api/auth")]
[EnableRateLimiting("AuthPolicy")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var response = await _authService.RegisterAsync(request);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
    {
        var response = await _authService.RefreshTokenAsync(request);

        return Ok(response);
    }

    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken(RefreshTokenRequestDto request)
    {
        await _authService.RevokeRefreshTokenAsync(request);

        return Ok(new { message = "Refresh token revocado." });
    }
}