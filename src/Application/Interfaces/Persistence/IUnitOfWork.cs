using Application.Interfaces.Repositories;

namespace Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Product { get; }
    ICustomerRepository Customer { get; }

    IGenericRepository<TEntity> Repository<TEntity>()
        where TEntity : class;
}
