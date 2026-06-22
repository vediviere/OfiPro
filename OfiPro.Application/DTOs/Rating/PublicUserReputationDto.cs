namespace OfiPro.Application.DTOs.Rating;

public class PublicUserReputationDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public double AverageScore { get; set; }
    public int TotalRatings { get; set; }
    public DateTime? LastRatingAt { get; set; }
    public List<PublicReceivedRatingDto> Ratings { get; set; } = [];
}