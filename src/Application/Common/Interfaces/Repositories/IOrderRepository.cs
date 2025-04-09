using Domain.Entity;

namespace Application.Common.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order> // Hereda las operaciones básicas
{
    // Métodos específicos para obtener órdenes con detalles (items, cliente)
    Task<Order?> GetOrderWithDetailsAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
    // Otros métodos específicos si son necesarios (ej: GetOrdersByStatusAsync)
}
