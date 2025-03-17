using Application.Interfaces.Repositories;
using Domain.Entity;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(PostgresContext context)
        : base(context) { }
}
