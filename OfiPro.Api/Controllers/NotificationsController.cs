using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.Common.Pagination;
using OfiPro.Application.Interfaces.Services;
using System.Security.Claims;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api/notifications")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyNotifications([FromQuery] PaginationQueryDto query)
    {
        var userId = GetUserId();

        var notifications = await _notificationService.GetMyNotificationsAsync(userId, query);

        return Ok(notifications);
    }

    [HttpGet("unread")]
    public async Task<IActionResult> GetMyUnreadNotifications()
    {
        var userId = GetUserId();

        var notifications = await _notificationService.GetMyUnreadNotificationsAsync(userId);

        return Ok(notifications);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetMyUnreadCount()
    {
        var userId = GetUserId();

        var unreadCount = await _notificationService.GetMyUnreadCountAsync(userId);

        return Ok(new
        {
            unreadCount
        });
    }

    [HttpPatch("{notificationId:guid}/read")]
    public async Task<IActionResult> MarkAsRead(Guid notificationId)
    {
        var userId = GetUserId();

        await _notificationService.MarkAsReadAsync(userId, notificationId);

        return Ok(new
        {
            message = "Notificación marcada como leída."
        });
    }

    [HttpPatch("read-all")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var userId = GetUserId();

        await _notificationService.MarkAllAsReadAsync(userId);

        return Ok(new
        {
            message = "Todas las notificaciones fueron marcadas como leídas."
        });
    }

    [HttpDelete("{notificationId:guid}")]
    public async Task<IActionResult> Delete(Guid notificationId)
    {
        var userId = GetUserId();

        await _notificationService.DeleteAsync(userId, notificationId);

        return NoContent();
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Token inválido.");
        }

        return userId;
    }
}