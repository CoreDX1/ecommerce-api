using Application.DTOs.Request.User;
using Domain.Entity;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> RegisterUser(RegisterUserRequestDTO createUser);
    Task<User> AuthenticateAsync(LoginUserRequestDTO loginUser);
}
