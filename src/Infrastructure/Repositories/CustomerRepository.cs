using Application.Common.Interfaces.Repositories;
using Domain.Entity;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(PostgresContext context)
        : base(context) { }
}
