namespace OfiPro.Application.DTOs.ProfessionalProfile;

public class ProfessionalProfileDto
{
    public Guid ProfessionalProfileId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string MainSpecialty { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int YearsExperience { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}