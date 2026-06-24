using OfiPro.Application.Common;
using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Common;
using OfiPro.Application.DTOs.Notification;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;

namespace OfiPro.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;

    public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
    }

    public async Task CreateAsync(CreateNotificationDto request)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null || user.DeletedAt is not null)
        {
            throw new NotFoundException("Usuario destinatario no encontrado.");
        }

        if (request.Type is null)
        {
            throw new BadRequestException("El tipo de notificación es obligatorio.");
        }

        if (!string.IsNullOrWhiteSpace(request.RelatedEntityType) && request.RelatedEntityId is null)
        {
            throw new BadRequestException("Si se especifica una entidad relacionada, también se debe enviar su Id.");
        }

        if (request.RelatedEntityId.HasValue && string.IsNullOrWhiteSpace(request.RelatedEntityType))
        {
            throw new BadRequestException("Si se especifica el Id de una entidad relacionada, también se debe enviar el tipo de entidad.");
        }

        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Type = request.Type.Value,
            Title = request.Title.Trim(),
            Message = request.Message.Trim(),
            RelatedEntityType = string.IsNullOrWhiteSpace(request.RelatedEntityType)
                ? null
                : request.RelatedEntityType.Trim(),
            RelatedEntityId = request.RelatedEntityId,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        await _notificationRepository.AddAsync(notification);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task<PaginatedResponseDto<NotificationDto>> GetMyNotificationsAsync(Guid userId, PaginationQueryDto request)
    {
        var notifications = await _notificationRepository.GetByUserIdAsync(
            userId,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection);

        var totalItems = await _notificationRepository.CountByUserIdAsync(userId);

        var notificationDtos = notifications
            .Select(MapToDto)
            .ToList();

        return new PaginatedResponseDto<NotificationDto>(
            notificationDtos,
            request.PageNumber,
            request.PageSize,
            totalItems);
    }

    public async Task<List<NotificationDto>> GetMyUnreadNotificationsAsync(Guid userId)
    {
        var notifications = await _notificationRepository.GetUnreadByUserIdAsync(userId);

        return notifications
            .Select(MapToDto)
            .ToList();
    }

    public async Task<int> GetMyUnreadCountAsync(Guid userId)
    {
        return await _notificationRepository.CountUnreadByUserIdAsync(userId);
    }

    public async Task MarkAsReadAsync(Guid userId, Guid notificationId)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId);

        if (notification is null)
        {
            throw new NotFoundException("Notificación no encontrada.");
        }

        ValidateNotificationBelongsToUser(notification, userId);

        if (notification.IsRead)
        {
            return;
        }

        notification.IsRead = true;
        notification.ReadAt = DateTime.UtcNow;

        await _notificationRepository.SaveChangesAsync();
    }

    public async Task MarkAllAsReadAsync(Guid userId)
    {
        var notifications = await _notificationRepository.GetUnreadByUserIdAsync(userId);

        if (!notifications.Any())
        {
            return;
        }

        foreach (var notification in notifications)
        {
            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
        }

        await _notificationRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid userId, Guid notificationId)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId);

        if (notification is null)
        {
            throw new NotFoundException("Notificación no encontrada.");
        }

        ValidateNotificationBelongsToUser(notification, userId);

        notification.DeletedAt = DateTime.UtcNow;

        await _notificationRepository.SaveChangesAsync();
    }

    private static void ValidateNotificationBelongsToUser(Notification notification, Guid userId)
    {
        if (notification.UserId != userId)
        {
            throw new ForbiddenException("No tienes permiso para modificar esta notificación.");
        }
    }

    private static NotificationDto MapToDto(Notification notification)
    {
        return new NotificationDto
        {
            NotificationId = notification.Id,
            UserId = notification.UserId,
            Type = notification.Type,
            Title = notification.Title,
            Message = notification.Message,
            RelatedEntityType = notification.RelatedEntityType,
            RelatedEntityId = notification.RelatedEntityId,
            IsRead = notification.IsRead,
            CreatedAt = notification.CreatedAt,
            ReadAt = notification.ReadAt
        };
    }
}