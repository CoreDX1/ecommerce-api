using Application.Common.Interfaces.Repositories;
using Domain.Entity;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(PostgresContext context)
        : base(context) { }

    public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetOrderWithDetailsAsync(int orderId)
    {
        throw new NotImplementedException();
    }
}
