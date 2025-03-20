using Application.Interfaces.Repositories;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsersRolesRepository : Repository<UsersRole>, IUsersRolesRepository
{
    public UsersRolesRepository(PostgresContext context)
        : base(context) { }

    public async Task<List<string>> GetRoles(int roleId)
    {
        var roles = await _context
            .UsersRoles.Where(ur => ur.UserId == roleId)
            .Include(ur => ur.Role)
            .Select(ur => ur.Role.RoleName)
            .ToListAsync();

        if (roles == null)
        {
            return [];
        }

        return roles;
    }
}
