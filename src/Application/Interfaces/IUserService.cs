using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Ardalis.Result;
using Domain.Entity;

namespace Application.Interfaces;

public interface IUserService : IGenericServiceAsync<User, UserResponseDTO>
{
    public Task<Result<UserResponseDTO>> RegisterUser(CreateUserRequestDTO createUser);
    public Task<Result<UserResponseDTO>> LoginUser(LoginUserRequestDTO loginUser);
}
