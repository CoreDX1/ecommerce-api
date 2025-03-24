using Application.DTOs.Request.Product;
using Domain.Entity;

namespace Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<IEnumerable<Product>> GetProductByName(string name);
    Task<IEnumerable<Product>> GetProductsByFilter(FilterProductRequestDTO filter);

    Task<IEnumerable<Product>> GetByPaginationAsync(int page, int recordsPerPage);
    Task<IEnumerable<Product>> GetBySortingAsync(string sortColumn, bool isDescending);
}
