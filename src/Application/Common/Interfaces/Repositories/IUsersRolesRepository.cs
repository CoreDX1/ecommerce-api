using Domain.Entity;

namespace Application.Interfaces.Repositories;

public interface IUsersRolesRepository : IRepository<UsersRole>
{
    Task<List<string>> GetRoles(int roleId);
}
