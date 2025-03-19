using Application.DTOs.Request.User;
using Domain.Entity;

namespace Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> RegisterUser(CreateUserRequestDTO createUser);
    Task<User> LoginUser(LoginUserRequestDTO loginUser);
}
