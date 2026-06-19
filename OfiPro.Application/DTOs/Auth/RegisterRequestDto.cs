using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Auth;

public class RegisterRequestDto
{
    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [StringLength(80)]
    public string State { get; set; } = string.Empty;

    [StringLength(80)]
    public string City { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;
}