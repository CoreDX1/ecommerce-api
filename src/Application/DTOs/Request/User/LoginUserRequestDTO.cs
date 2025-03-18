namespace Application.DTOs.Request.User;

public class LoginUserRequestDTO
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
