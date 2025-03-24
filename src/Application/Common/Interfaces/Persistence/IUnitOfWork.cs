using Application.Interfaces.Repositories;

namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    IUsersRolesRepository UsersRolesRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IUserRepository UserRepository { get; }

    IRepository<TEntity> Repository<TEntity>()
        where TEntity : class;
}
