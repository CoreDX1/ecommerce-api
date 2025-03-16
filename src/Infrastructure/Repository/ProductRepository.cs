using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(PostgresContext context)
        : base(context) { }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        return await _context
            .Products.Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }
}
