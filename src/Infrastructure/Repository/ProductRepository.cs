using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(PostgresContext context)
        : base(context) { }
}
