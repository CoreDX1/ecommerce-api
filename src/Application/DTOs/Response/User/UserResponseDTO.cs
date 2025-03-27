namespace Application.DTOs.Response.User;

public class UserResponseDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public string? VerificationToken { get; set; }
}
