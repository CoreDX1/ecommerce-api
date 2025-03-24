using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Ardalis.Result;
using Domain.Entity;

namespace Application.Common.Interfaces;

public interface IUserService : IGenericServiceAsync<User, UserResponseDTO>
{
    Task<Result<UserResponseDTO>> RegisterAsync(CreateUserRequestDTO createUser);
    Task<Result<UserResponseDTO>> LoginAsync(LoginUserRequestDTO loginUser);
}
