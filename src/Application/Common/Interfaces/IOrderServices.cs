using Application.DTOs.Request.Order;
using Application.DTOs.Response.Order;
using Ardalis.Result;

namespace Application.Common.Interfaces;

public interface IOrderServices
{
    Task<Result<OrderResponseDTO>> CreateOrderAsync(CreateOrderRequestDTO request);
    Task<Result<OrderResponseDTO>> GetOrderByIdAsync(int orderId);
    Task<Result<IEnumerable<OrderResponseDTO>>> GetOrdersByCustomerIdAsync(int customerId);
    Task<Result> UpdateOrderStatusAsync(UpdateOrderStatusRequestDTO request);
    // Podrían añadirse métodos para paginación, filtros por estado, etc.
    // Task<Result<PaginatedList<OrderResponseDTO>>> GetOrdersAsync(int page, int pageSize, string? statusFilter);
}
