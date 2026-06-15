using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfiPro.Application.DTOs.User;

namespace OfiPro.Application.Interfaces;

public interface IUserService
{
    Task<UserProfileDto> GetProfileAsync(Guid userId);
    Task<UserProfileDto> UpdateProfileAsync(
        Guid userId,
        UpdateUserProfileDto request);
    Task<List<UserProfileDto>> GetAllAsync();
    Task<UserProfileDto> GetByIdAsync(Guid id);
    Task ActivateAsync(Guid id);
    Task DeactivateAsync(Guid id);
    Task DeleteAsync(Guid id);
}
