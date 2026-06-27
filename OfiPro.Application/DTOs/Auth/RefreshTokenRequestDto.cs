using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Auth;

public class RefreshTokenRequestDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}