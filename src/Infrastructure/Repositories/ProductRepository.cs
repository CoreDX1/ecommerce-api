using Application.DTOs.Request.Product;
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

    public async Task<IEnumerable<Product>> GetProductsByFilter(FilterProductRequestDTO filter)
    {
        // IQueryable<Product> query = _context.Products;

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

        // Filtrado por nombre
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(p => p.Name.ToLower().Contains(filter.Name)); // Búsqueda insensible a mayúsculas
        }

        // Filtrado por precio
        if (filter.Price.HasValue)
        {
            query = query.Where(p => p.Price == filter.Price.Value);
        }

        // Filtrado por categoría
        // if (!string.IsNullOrEmpty(filter.Category))
        // {
        //     query = query.Where(p => p.Category.Contains(filter.Category, System.StringComparison.OrdinalIgnoreCase)); // Búsqueda insensible a mayúsculas
        // }

        // Ordenamiento
        if (!string.IsNullOrEmpty(filter.OrderBy))
        {
            switch (filter.OrderBy.ToLower())
            {
                case "name":
                    query = filter.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                    break;
                case "price":
                    query = filter.IsDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                    break;
                case "category":
                    query = filter.IsDescending ? query.OrderByDescending(p => p.Category) : query.OrderBy(p => p.Category);
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
            query = query.Skip((filter.Page - 1) * filter.RecordsPerPage).Take(filter.RecordsPerPage);
        }

        return await query.ToListAsync();
    }
}
