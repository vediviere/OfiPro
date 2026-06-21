using Microsoft.EntityFrameworkCore;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Entities;
using OfiPro.Infrastructure.Persistence;

namespace OfiPro.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
    }

    public async Task<List<Notification>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Notifications
            .Where(x => x.UserId == userId && x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Notification>> GetUnreadByUserIdAsync(Guid userId)
    {
        return await _context.Notifications
            .Where(x => x.UserId == userId && x.IsRead == false && x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Notification?> GetByIdAsync(Guid notificationId)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(x => x.Id == notificationId && x.DeletedAt == null);
    }

    public async Task<int> CountUnreadByUserIdAsync(Guid userId)
    {
        return await _context.Notifications
            .CountAsync(x => x.UserId == userId && x.IsRead == false && x.DeletedAt == null);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}