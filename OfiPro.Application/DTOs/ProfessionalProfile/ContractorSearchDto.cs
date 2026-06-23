namespace OfiPro.Application.DTOs.ProfessionalProfile;

public class ContractorSearchDto
{
    public Guid UserId { get; set; }
    public Guid ProfessionalProfileId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string MainSpecialty { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int YearsExperience { get; set; }
    public double AverageScore { get; set; }
    public int TotalRatings { get; set; }
}