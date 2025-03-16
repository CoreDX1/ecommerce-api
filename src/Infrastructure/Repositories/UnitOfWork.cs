using Domain.Interfaces;
using Domain.Interfaces.Persistence;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgresContext _context;

    public IProductRepository Product { get; }

    public ICustomerRepository Customer { get; }

    IProductRepository IUnitOfWork.Product => throw new NotImplementedException();

    ICustomerRepository IUnitOfWork.Customer => throw new NotImplementedException();

    // public IGenericRepository<TEntity> Repository<TEntity>()
    //     where TEntity : class
    // {
    //     return new GenericRepository<TEntity>(_context);
    // }


    public UnitOfWork(
        PostgresContext context,
        IProductRepository productRepository,
        ICustomerRepository customerRepository
    )
    {
        _context = context;
        Product = productRepository;
        Customer = customerRepository;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
