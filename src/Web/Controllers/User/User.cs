using Application.Common.Interfaces;
using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.User;

[ApiController]
[Route("api/[controller]")]
public class User : ControllerBase
{
    private readonly IUserService _userService;

    public User(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="createUser">The user to register</param>
    /// <returns> The created user </returns>
    [HttpPost("register")]
    public async Task<Result<UserResponseDTO>> RegisterUser([FromBody] RegisterUserRequestDTO createUser)
    {
        return await _userService.RegisterAsync(createUser);
    }

    /// <summary>
    /// Login a user
    /// </summary>
    /// <param name="loginUser">The user to login</param>
    /// <returns> The logged user </returns>
    [HttpPost("login")]
    public async Task<Result<UserResponseDTO>> LoginUser([FromBody] LoginUserRequestDTO loginUser)
    {
        return await _userService.LoginAsync(loginUser);
    }
}
