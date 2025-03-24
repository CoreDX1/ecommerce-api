using Domain.Entity;

namespace Application.Interfaces.Repositories;

public interface IUsersRolesRepository : IRepository<UsersRole>
{
    Task<IEnumerable<string>> GetRoles(User user);
}
