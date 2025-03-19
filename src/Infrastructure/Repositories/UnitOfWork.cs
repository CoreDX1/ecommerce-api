using Application.Interfaces.Persistence;
using Application.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgresContext _context;

    public IProductRepository Product { get; }

    public ICustomerRepository Customer { get; }

    public IUserRepository User { get; }

    public IRepository<TEntity> Repository<TEntity>()
        where TEntity : class
    {
        return new Repository<TEntity>(_context);
    }

    public UnitOfWork(PostgresContext context, IProductRepository productRepository, ICustomerRepository customerRepository, IUserRepository userRepository)
    {
        _context = context;
        Product = productRepository;
        Customer = customerRepository;
        User = userRepository;
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
