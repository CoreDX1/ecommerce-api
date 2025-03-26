using Domain.Entity;

namespace Application.Common.Interfaces.Repositories;

public interface IUsersRolesRepository : IRepository<UsersRole>
{
    Task<IEnumerable<string>> GetRoles(int userId);
}
