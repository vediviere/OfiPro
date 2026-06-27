using System.ComponentModel.DataAnnotations;
using OfiPro.Application.Common.Validation;

namespace OfiPro.Application.DTOs.Rating;

public class CreateRatingDto
{
    [Required]
    [Range(1, 5)]
    public int Score { get; set; }

    [StringLength(1000)]
    [NoHtml]
    public string Comment { get; set; } = string.Empty;
}