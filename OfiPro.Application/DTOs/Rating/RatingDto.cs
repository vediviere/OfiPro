namespace OfiPro.Application.DTOs.Rating;

public class RatingDto
{
    public Guid RatingId { get; set; }
    public Guid ContractId { get; set; }
    public Guid RaterUserId { get; set; }
    public string RaterUserName { get; set; } = string.Empty;
    public Guid RatedUserId { get; set; }
    public string RatedUserName { get; set; } = string.Empty;
    public int Score { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}