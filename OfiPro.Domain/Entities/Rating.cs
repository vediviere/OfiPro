namespace OfiPro.Domain.Entities;

public class Rating
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public Guid EvaluatorUserId { get; set; }
    public User EvaluatorUser { get; set; } = null!;
    public Guid EvaluatedUserId { get; set; }
    public User EvaluatedUser { get; set; } = null!;
    public int Quality { get; set; }
    public int Punctuality { get; set; }
    public int Communication { get; set; }
    public int Professionalism { get; set; }
    public int CostBenefit { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
