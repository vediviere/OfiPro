using System.ComponentModel.DataAnnotations;
using OfiPro.Application.Common.Validation;

namespace OfiPro.Application.DTOs.Auth;

public class RegisterRequestDto
{
    [Required]
    [StringLength(80)]
    [NoHtml]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    [NoHtml]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    [NoHtml]
    public string Phone { get; set; } = string.Empty;

    [StringLength(80)]
    [NoHtml]
    public string State { get; set; } = string.Empty;

    [StringLength(80)]
    [NoHtml]
    public string City { get; set; } = string.Empty;

    [Required]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [RegularExpression(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
    ErrorMessage = "La contraseña debe incluir mayúscula, minúscula, número y carácter especial.")]
    public string Password { get; set; } = string.Empty;
}