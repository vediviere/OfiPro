using OfiPro.Domain.Enums;

namespace OfiPro.Domain.Entities;

public class UserRole
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public UserRoleType Role { get; set; }
}
