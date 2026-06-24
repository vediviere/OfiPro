using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface INotificationRepository
{
    Task AddAsync(Notification notification);
    Task<List<Notification>> GetByUserIdAsync(Guid userId, int pageNumber, int pageSize, string sortBy, string sortDirection);
    Task<int> CountByUserIdAsync(Guid userId);
    Task<List<Notification>> GetUnreadByUserIdAsync(Guid userId);
    Task<Notification?> GetByIdAsync(Guid notificationId);
    Task<int> CountUnreadByUserIdAsync(Guid userId);
    Task SaveChangesAsync();
}