using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Auth;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;
}