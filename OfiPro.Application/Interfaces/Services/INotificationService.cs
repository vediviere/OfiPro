using OfiPro.Application.DTOs.Notification;

namespace OfiPro.Application.Interfaces.Services;

public interface INotificationService
{
    Task CreateAsync(CreateNotificationDto request);
    Task<List<NotificationDto>> GetMyNotificationsAsync(Guid userId);
    Task<List<NotificationDto>> GetMyUnreadNotificationsAsync(Guid userId);
    Task<int> GetMyUnreadCountAsync(Guid userId);
    Task MarkAsReadAsync(Guid userId, Guid notificationId);
    Task MarkAllAsReadAsync(Guid userId);
    Task DeleteAsync(Guid userId, Guid notificationId);
}