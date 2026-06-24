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

    public async Task<List<Notification>> GetByUserIdAsync(Guid userId, int pageNumber, int pageSize, string sortBy, string sortDirection)
    {
        var query = _context.Notifications
            .Where(x => x.UserId == userId && x.DeletedAt == null);

        query = ApplyNotificationSorting(query, sortBy, sortDirection);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    private static IQueryable<Notification> ApplyNotificationSorting(IQueryable<Notification> query, string sortBy, string sortDirection)
    {
        var descending = string.Equals(
            sortDirection,
            "desc",
            StringComparison.OrdinalIgnoreCase);

        return sortBy.Trim().ToLowerInvariant() switch
        {
            "title" => descending
                ? query.OrderByDescending(x => x.Title)
                : query.OrderBy(x => x.Title),

            "type" => descending
                ? query.OrderByDescending(x => x.Type)
                : query.OrderBy(x => x.Type),

            "isread" => descending
                ? query.OrderByDescending(x => x.IsRead)
                : query.OrderBy(x => x.IsRead),

            _ => descending
                ? query.OrderByDescending(x => x.CreatedAt)
                : query.OrderBy(x => x.CreatedAt)
        };
    }

    public async Task<int> CountByUserIdAsync(Guid userId)
    {
        return await _context.Notifications
            .CountAsync(x => x.UserId == userId && x.DeletedAt == null);
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