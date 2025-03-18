namespace Application.DTOs.Response.User;

public class UserResponseDTO
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsActive { get; set; }

    public string? VerificationToken { get; set; }

    public string? PasswordResetToken { get; set; }
}
