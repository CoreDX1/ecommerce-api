namespace Application.DTOs.Request.User;

public class LoginUserRequestDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
