using Domain.Entity;

namespace Domain.Interfaces;

public interface IProductRepository : IGenericRepository<Product>, IDisposable
{
    public Task<IEnumerable<Product>> GetProductByName(string name);
    public Task<IEnumerable<Product>> GetProductsByFilter();

    public Task<IEnumerable<Product>> GetByPaginationAsync(int page, int recordsPerPage);
    public Task<IEnumerable<Product>> GetBySortingAsync(string sortColumn, bool isDescending);
}
