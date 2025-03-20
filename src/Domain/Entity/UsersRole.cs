namespace Domain.Entity;

public partial class UsersRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;
}
