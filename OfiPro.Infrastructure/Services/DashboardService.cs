using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Dashboard;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IUserRepository _userRepository;

    public DashboardService(IDashboardRepository dashboardRepository, IUserRepository userRepository)
    {
        _dashboardRepository = dashboardRepository;
        _userRepository = userRepository;
    }

    public async Task<ClientDashboardSummaryDto> GetClientSummaryAsync(Guid userId)
    {
        var isClient = await _userRepository.HasRoleAsync(
            userId,
            UserRoleType.Cliente);

        if (!isClient)
        {
            throw new ForbiddenException("Solo usuarios con rol Cliente pueden consultar este resumen.");
        }

        return await _dashboardRepository.GetClientSummaryAsync(userId);
    }

    public async Task<ContractorDashboardSummaryDto> GetContractorSummaryAsync(Guid userId)
    {
        var isContractor = await _userRepository.HasRoleAsync(
            userId,
            UserRoleType.Contratista);

        if (!isContractor)
        {
            throw new ForbiddenException("Solo usuarios con rol Contratista pueden consultar este resumen.");
        }

        return await _dashboardRepository.GetContractorSummaryAsync(userId);
    }

    public async Task<DashboardModesDto> GetAvailableModesAsync(Guid userId)
    {
        var canUseClientMode = await _userRepository.HasRoleAsync(
            userId,
            UserRoleType.Cliente);

        var canUseContractorMode = await _userRepository.HasRoleAsync(
            userId,
            UserRoleType.Contratista);

        var canUseAdminMode = await _userRepository.HasRoleAsync(
            userId,
            UserRoleType.Administrador);

        var availableModes = new List<string>();

        if (canUseClientMode)
        {
            availableModes.Add(UserRoleType.Cliente.ToString());
        }

        if (canUseContractorMode)
        {
            availableModes.Add(UserRoleType.Contratista.ToString());
        }

        if (canUseAdminMode)
        {
            availableModes.Add(UserRoleType.Administrador.ToString());
        }

        var defaultMode = GetDefaultMode(canUseClientMode, canUseContractorMode, canUseAdminMode);

        return new DashboardModesDto
        {
            UserId = userId,
            CanUseClientMode = canUseClientMode,
            CanUseContractorMode = canUseContractorMode,
            CanUseAdminMode = canUseAdminMode,
            AvailableModes = availableModes,
            DefaultMode = defaultMode
        };
    }

    public async Task<DashboardUserContextDto> GetUserContextAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null || user.DeletedAt != null)
        {
            throw new NotFoundException("Usuario no encontrado.");
        }

        var modes = await GetAvailableModesAsync(userId);

        return new DashboardUserContextDto
        {
            UserId = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            FullName = $"{user.Name} {user.LastName}".Trim(),
            Email = user.Email,
            Modes = modes
        };
    }

    public async Task<AdminDashboardSummaryDto> GetAdminSummaryAsync(Guid userId)
    {
        var isAdmin = await _userRepository.HasRoleAsync(
            userId,
            UserRoleType.Administrador);

        if (!isAdmin)
        {
            throw new ForbiddenException("Solo usuarios con rol Administrador pueden consultar este resumen.");
        }

        return await _dashboardRepository.GetAdminSummaryAsync();
    }

    private static string GetDefaultMode(bool canUseClientMode, bool canUseContractorMode, bool canUseAdminMode)
    {
        if (canUseClientMode)
        {
            return UserRoleType.Cliente.ToString();
        }

        if (canUseContractorMode)
        {
            return UserRoleType.Contratista.ToString();
        }

        if (canUseAdminMode)
        {
            return UserRoleType.Administrador.ToString();
        }

        return string.Empty;
    }
}