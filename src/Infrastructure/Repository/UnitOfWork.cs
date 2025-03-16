using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgresContext _context;

    public IProductRepository Product { get; }

    public ICustomerRepository Customer { get; }

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
