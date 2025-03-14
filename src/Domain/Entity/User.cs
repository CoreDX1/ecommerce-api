namespace Domain.Entity;

public partial class User
{
    public int UserId { get; set; }

    public int? CustomerId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? RegistrationDate { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool? IsActive { get; set; }

    public bool? EmailVerified { get; set; }

    public string? VerificationToken { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? PasswordResetExpiry { get; set; }

    public virtual Customer? Customer { get; set; }
}
