namespace OfiPro.Application.DTOs.Rating;

public class PublicReceivedRatingDto
{
    public string RaterUserName { get; set; } = string.Empty;
    public int Score { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}