using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Rating;

public class CreateRatingDto
{
    [Required]
    [Range(1, 5)]
    public int Score { get; set; }

    [StringLength(1000)]
    public string Comment { get; set; } = string.Empty;
}