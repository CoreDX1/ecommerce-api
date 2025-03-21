using Application.DTOs.Request.Product;
using Application.Interfaces.Repositories;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(PostgresContext context)
        : base(context) { }

    public async Task<IEnumerable<Product>> GetByPaginationAsync(int page, int recordsPerPage)
    {
        var query = await GetAllProducts();

        query = query.Skip(page * recordsPerPage).Take(recordsPerPage);

        return query;
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
        var query = await GetAllProducts();
        return query.Where(p => p.Name.ToLower().Contains(name));
    }

    public async Task<IEnumerable<Product>> GetProductsByFilter(FilterProductRequestDTO filter)
    {
        IEnumerable<Product> query = await GetAllProducts();

        // Filtrado por nombre
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(p => p.Name.ToLower().Contains(filter.Name)); // Búsqueda insensible a mayúsculas
        }

        // Filtrado por precio
        if (filter.Price < 0)
            query = query.Where(p => p.Price == filter.Price);

        // Ordenamiento
        if (!string.IsNullOrEmpty(filter.OrderBy))
        {
            switch (filter.OrderBy.ToLower())
            {
                case "name":
                    query = filter.IsDescending
                        ? query.OrderByDescending(p => p.Name)
                        : query.OrderBy(p => p.Name);
                    break;
                case "price":
                    query = filter.IsDescending
                        ? query.OrderByDescending(p => p.Price)
                        : query.OrderBy(p => p.Price);
                    break;
                case "category":
                    query = filter.IsDescending
                        ? query.OrderByDescending(p => p.Category)
                        : query.OrderBy(p => p.Category);
                    break;
                // Puedes agregar más criterios de ordenamiento aquí
                default:
                    // Puedes definir un ordenamiento por defecto o lanzar una excepción
                    query = query.OrderBy(p => p.CategoryId); // Ejemplo de ordenamiento por defecto por ID
                    break;
            }
        }
        else
        {
            // Si no se especifica un ordenamiento, puedes definir uno por defecto
            query = query.OrderBy(p => p.ProductId);
        }

        // Paginación
        if (filter.Page > 0 && filter.RecordsPerPage > 0)
        {
            query = query
                .Skip((filter.Page - 1) * filter.RecordsPerPage)
                .Take(filter.RecordsPerPage);
        }

        return query;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        IQueryable<Product> query = _context
            .Products.Include(p => p.Category)
            .Select(p => new Product()
            {
                CategoryName = p.Category.Name,
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
            });

        return await query.ToListAsync();
    }
}
