using Application.Interfaces.Repositories;
using Domain.Entity;
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

    public async Task<IEnumerable<Product>> GetByPaginationAsync(int page, int recordsPerPage)
    {
        IQueryable<Product> query = _context.Products;

        query = query.Skip(page * recordsPerPage).Take(recordsPerPage);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetBySortingAsync(string sortColumn, bool isDescending)
    {
        IQueryable<Product> query = _context.Products;

        switch (sortColumn)
        {
            case "Name":
                query = query.OrderBy(p => p.Name);
                break;
            case "Price":
                query = query.OrderBy(p => p.Price);
                break;
            case "Category":
                query = query.OrderBy(p => p.Category);
                break;
            default:
                query = query.OrderBy(p => p.Name);
                break;
        }

        if (!isDescending)
            query = query.OrderByDescending(p => p.Name);

        return await query.ToListAsync();
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
