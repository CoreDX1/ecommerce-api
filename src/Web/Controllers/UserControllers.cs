using Application.Interface;
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

    [HttpGet]
    [Route("name")]
    public string GetUserName()
    {
        return _userService.GetUserName();
    }
}
