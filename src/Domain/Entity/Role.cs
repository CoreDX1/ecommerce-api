﻿namespace Domain.Entity;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UsersRole> UsersRoles { get; set; } = [];
}
