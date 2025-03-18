namespace Application.DTOs.Request.User;

public class CreateUserRequestDTO
{
    public int? CustomerId { get; set; }

    public string Username { get; set; } = null!;

    public string UserLastName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? RegistrationDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? PasswordResetExpiry { get; set; }
}
