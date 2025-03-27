namespace Application.DTOs.Request.User;

public class RegisterUserRequestDTO
{
    public string Username { get; set; } = null!;
    public string UserLastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}
