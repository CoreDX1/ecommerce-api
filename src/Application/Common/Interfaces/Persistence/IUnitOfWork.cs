using Application.Common.Interfaces.Repositories;

namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    IUsersRolesRepository UsersRolesRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IUserRepository UserRepository { get; }
    IOrderRepository OrderRepository { get; }

    IRepository<TEntity> Repository<TEntity>()
        where TEntity : class;

    // Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
