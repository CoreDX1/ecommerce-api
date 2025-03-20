using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Application.Interfaces;
using Application.Interfaces.Persistence;
using Ardalis.Result;
using AutoMapper;
using Domain.Entity;

namespace Application.Services;

public class UserServices : GenericServiceAsync<User, UserResponseDTO>, IUserService
{
    public UserServices(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }

    public async Task<Result<UserResponseDTO>> RegisterUser(CreateUserRequestDTO createUser)
    {
        User user = await _unitOfWork.User.RegisterUser(createUser);

        if (user == null)
            return Result.NotFound("User not found");

        var userResponse = _mapper.Map<UserResponseDTO>(user);

        return Result.Success(userResponse, "User created successfully");
    }

    public async Task<Result<UserResponseDTO>> LoginUser(LoginUserRequestDTO loginUser)
    {
        User user = await _unitOfWork.User.LoginUser(loginUser);

        if (user == null)
            return Result.NotFound("User not found");

        var userResponse = _mapper.Map<UserResponseDTO>(user);

        return Result.Success(userResponse, "User created successfully");
    }
}
