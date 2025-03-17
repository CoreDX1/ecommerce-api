using Domain.Entity;

namespace Domain.Interfaces;

public interface IProductRepository : IGenericRepository<Product>, IDisposable
{
    public Task<IEnumerable<Product>> GetProductByName(string name);
    public Task<IEnumerable<Product>> GetProductsByFilter();
}
