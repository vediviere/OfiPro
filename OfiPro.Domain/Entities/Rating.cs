namespace OfiPro.Domain.Entities;

public class Rating
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public Contract Contract { get; set; } = null!;
    public Guid RaterUserId { get; set; }
    public User RaterUser { get; set; } = null!;
    public Guid RatedUserId { get; set; }
    public User RatedUser { get; set; } = null!;
    public int Score { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}