using OfiPro.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Notification;

public class CreateNotificationDto : IValidatableObject
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public NotificationType? Type { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(500, MinimumLength = 3)]
    public string Message { get; set; } = string.Empty;

    [StringLength(80)]
    public string? RelatedEntityType { get; set; }

    public Guid? RelatedEntityId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (UserId == Guid.Empty)
        {
            yield return new ValidationResult(
                "UserId es obligatorio.",
                new[] { nameof(UserId) });
        }

        if (Type.HasValue && !Enum.IsDefined(typeof(NotificationType), Type.Value))
        {
            yield return new ValidationResult(
                "Type no permitido.",
                new[] { nameof(Type) });
        }

        if (!string.IsNullOrWhiteSpace(RelatedEntityType) && RelatedEntityId is null)
        {
            yield return new ValidationResult(
                "Si RelatedEntityType tiene valor, RelatedEntityId también debe tener valor.",
                new[] { nameof(RelatedEntityId) });
        }

        if (RelatedEntityId.HasValue && string.IsNullOrWhiteSpace(RelatedEntityType))
        {
            yield return new ValidationResult(
                "Si RelatedEntityId tiene valor, RelatedEntityType también debe tener valor.",
                new[] { nameof(RelatedEntityType) });
        }
    }
}