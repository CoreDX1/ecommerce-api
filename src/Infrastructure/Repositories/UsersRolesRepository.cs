using Application.Common.Interfaces.Repositories;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsersRolesRepository : Repository<UsersRole>, IUsersRolesRepository
{
    public UsersRolesRepository(PostgresContext context)
        : base(context) { }

    public async Task<IEnumerable<string>> GetRoles(int userId)
    {
        return await _context.UsersRoles.Where(ur => ur.UserId == userId).Include(ur => ur.Role).Select(ur => ur.Role.RoleName).ToListAsync();
    }
}
