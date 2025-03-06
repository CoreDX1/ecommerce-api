using Application.Interface;

namespace Application.Services;

public class User : IUserService
{
    public string GetUserName()
    {
        return "Christian";
    }
}
