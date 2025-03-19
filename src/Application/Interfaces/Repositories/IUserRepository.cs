using Application.DTOs.Request.User;
using Domain.Entity;

namespace Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<User> RegisterUser(CreateUserRequestDTO createUser);
    public Task<User> LoginUser(LoginUserRequestDTO loginUser);
}
