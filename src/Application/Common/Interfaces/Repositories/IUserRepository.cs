using Application.DTOs.Request.User;
using Domain.Entity;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> RegisterUser(RegisterUserRequestDTO createUser);
    Task<User> AuthenticateAsync(LoginUserRequestDTO loginUser);
    // Complex logic like registration/authentication is handled in the service layer
    
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByUsernameAsync(string username);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> UpdatePasswordAsync(int userId, string newPasswordHash);
    Task<bool> UpdateUserProfileAsync(User user);
    Task<bool> DeactivateUserAsync(int userId);
    Task<bool> ReactivateUserAsync(int userId);
    Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
}
