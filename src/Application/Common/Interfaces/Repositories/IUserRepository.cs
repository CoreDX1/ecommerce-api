using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Domain.Entity;

namespace Application.Common.Interfaces.Repositories;

public interface IUserServices : IGenericServiceAsync<User, UserResponseDTO>
{
    Task<User> RegisterUser(CreateUserRequestDTO createUser);
    Task<User> AuthenticateAsync(LoginUserRequestDTO loginUser);
}
