using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.DTOs.Notification;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Enums;

namespace OfiPro.Api.BackgroundServices;

public class ProjectExpirationBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProjectExpirationBackgroundService> _logger;

    public ProjectExpirationBackgroundService(IServiceScopeFactory scopeFactory, IConfiguration configuration,    ILogger<ProjectExpirationBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _configuration = configuration;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ExpireProjectsAsync(stoppingToken);

        var checkIntervalHours = _configuration.GetValue(
            "ProjectExpiration:CheckIntervalHours",
            24);

        using var timer = new PeriodicTimer(TimeSpan.FromHours(checkIntervalHours));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await ExpireProjectsAsync(stoppingToken);
        }
    }

    private async Task ExpireProjectsAsync(CancellationToken stoppingToken)
    {
        var expirationDays = _configuration.GetValue(
            "ProjectExpiration:ExpirationDays",
            30);

        var expirationLimitUtc = DateTime.UtcNow.AddDays(-expirationDays);

        using var scope = _scopeFactory.CreateScope();

        var projectRepository = scope.ServiceProvider
            .GetRequiredService<IProjectRepository>();

        var notificationService = scope.ServiceProvider
            .GetRequiredService<INotificationService>();

        var projectsToExpire = await projectRepository
            .GetPublishedProjectsToExpireAsync(expirationLimitUtc);

        if (projectsToExpire.Count == 0)
        {
            _logger.LogInformation(
                "Project expiration job executed. Expired projects: 0");

            return;
        }

        var expiredProjectsCount = await projectRepository
            .ExpirePublishedProjectsAsync(expirationLimitUtc);

        foreach (var project in projectsToExpire)
        {
            await notificationService.CreateAsync(new CreateNotificationDto
            {
                UserId = project.CreatedByUserId,
                Type = NotificationType.ProjectExpired,
                Title = "Tu proyecto expiró",
                Message = $"Tu proyecto \"{project.Title}\" expiró automáticamente porque superó el tiempo de publicación.",
                RelatedEntityType = "Project",
                RelatedEntityId = project.Id
            });
        }

        _logger.LogInformation(
            "Project expiration job executed. Expired projects: {ExpiredProjectsCount}",
            expiredProjectsCount);
    }
}