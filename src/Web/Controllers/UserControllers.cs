using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Application.Interfaces;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserControllers : ControllerBase
{
    private readonly IUserService _userService;

    public UserControllers(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<Result<UserResponseDTO>> RegisterUser([FromBody] CreateUserRequestDTO createUser)
    {
        return await _userService.RegisterUser(createUser);
    }

    [HttpPost("login")]
    public async Task<Result<UserResponseDTO>> LoginUser([FromBody] LoginUserRequestDTO loginUser)
    {
        return await _userService.LoginUser(loginUser);
    }
}
