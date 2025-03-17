using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository, IDisposable
{
    public ProductRepository(PostgresContext context)
        : base(context) { }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        return await _context.Products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }

    public Task<IEnumerable<Product>> GetProductsByFilter()
    {
        throw new NotImplementedException();
    }
}
