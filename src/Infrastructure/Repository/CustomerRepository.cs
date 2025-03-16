using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(PostgresContext context)
        : base(context) { }
}
