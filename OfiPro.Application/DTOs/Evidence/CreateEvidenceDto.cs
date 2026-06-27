using OfiPro.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using OfiPro.Application.Common.Validation;

namespace OfiPro.Application.DTOs.Evidence;

public class CreateEvidenceDto : IValidatableObject
{
    [Required]
    public EvidenceType? EvidenceType { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    [NoHtml]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    [Url]
    public string FileUrl { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string FileType { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EvidenceType.HasValue &&
            !Enum.IsDefined(typeof(EvidenceType), EvidenceType.Value))
        {
            yield return new ValidationResult(
                "EvidenceType no permitido. Valores permitidos: 1 = Antes, 2 = Durante, 3 = Despues.",
                new[] { nameof(EvidenceType) });
        }

        var allowedFileTypes = new[]
        {
            "image/jpeg",
            "image/png",
            "application/pdf"
        };

        if (!string.IsNullOrWhiteSpace(FileType) &&
            !allowedFileTypes.Contains(FileType.Trim().ToLowerInvariant()))
        {
            yield return new ValidationResult(
                "FileType no permitido. Valores permitidos: image/jpeg, image/png, application/pdf.",
                new[] { nameof(FileType) });
        }
    }
}