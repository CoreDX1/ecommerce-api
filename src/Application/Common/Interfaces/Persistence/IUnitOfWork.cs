using Application.Interfaces.Repositories;

namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Product { get; }
    IUsersRolesRepository UsersRoles { get; }
    ICustomerRepository Customer { get; }
    IUserRepository User { get; }

    IRepository<TEntity> Repository<TEntity>()
        where TEntity : class;
}
