using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Ardalis.Result;
using Domain.Entity;

namespace Application.Interfaces;

public interface IUserService : IGenericServiceAsync<User, UserResponseDTO>
{
    Task<Result<UserResponseDTO>> RegisterUser(CreateUserRequestDTO createUser);
    Task<Result<UserResponseDTO>> LoginUser(LoginUserRequestDTO loginUser);
}
