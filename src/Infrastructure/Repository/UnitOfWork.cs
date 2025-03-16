using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    public IProductRepository ProductRepository { get; }

    public ICustomerRepository CustomerRepository { get; }

    private readonly PostgresContext _context;

    public UnitOfWork(
        IProductRepository productRepository,
        ICustomerRepository customerRepository,
        PostgresContext context
    )
    {
        ProductRepository = productRepository;
        CustomerRepository = customerRepository;
        _context = context;
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
