using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.DTOs.Request.Order;
using Application.DTOs.Response.Order;
using Ardalis.Result;
using AutoMapper;
using Domain.Common.Constants;
using Domain.Entity;
using FluentValidation;

namespace Application.Services;

public class OrderServices : IOrderServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateOrderRequestDTO> _createOrderValidator;
    private readonly IValidator<UpdateOrderStatusRequestDTO> _updateStatusValidator;
    private readonly IValidatorServices _validatorServices;

    // Podrías inyectar IProductRepository directamente si prefieres no usar el genérico para el stock.
    // private readonly IProductRepository _productRepository;

    // Define constantes para estados si no están en el dominio
    private const string OrderStatusPending = "Pending";
    private const string OrderStatusCancelled = "Cancelled";
    private const string OrderStatusShipped = "Shipped";

    // ... otros estados

    public OrderServices(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateOrderRequestDTO> createOrderValidator,
        IValidator<UpdateOrderStatusRequestDTO> updateStatusValidator,
        IValidatorServices validatorServices
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createOrderValidator = createOrderValidator;
        _updateStatusValidator = updateStatusValidator;
        _validatorServices = validatorServices;
    }

    public async Task<Result<OrderResponseDTO>> CreateOrderAsync(CreateOrderRequestDTO request)
    {
        // Validar el request
        var validationResult = await _createOrderValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return _validatorServices.GetInvalidResult<OrderResponseDTO>(validationResult);

        // 1. Verificar Cliente
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
            return Result.NotFound($"Customer with ID {request.CustomerId} not found.");

        var orderItems = new List<OrderItem>();
        decimal totalOrderAmount = 0;

        // Usar el repositorio genérico para productos
        var productRepository = _unitOfWork.Repository<Product>();

        // 2. Procesar Items y verificar Productos/Stock
        foreach (var itemRequest in request.Items)
        {
            var product = await productRepository.GetByIdAsync(itemRequest.ProductId);
            if (product == null)
                return Result.NotFound($"Product with ID {itemRequest.ProductId} not found.");

            if (product.StockQuantity < itemRequest.Quantity)
                return Result.Conflict(
                    $"Insufficient stock for Product ID {itemRequest.ProductId}. Available: {product.StockQuantity}, Requested: {itemRequest.Quantity}."
                );

            // Crear OrderItem
            var orderItem = new OrderItem
            {
                ProductId = product.ProductId,
                Quantity = itemRequest.Quantity,
                UnitPrice = product.Price, // Guardar el precio al momento de la orden
                Subtotal = product.Price * itemRequest.Quantity,
            };
            orderItems.Add(orderItem);
            totalOrderAmount += orderItem.Subtotal.Value; // Asumiendo que Subtotal no es null aquí

            // 3. Actualizar Stock (¡Importante!)
            product.StockQuantity -= itemRequest.Quantity;
            product.LastUpdated = DateTime.UtcNow;
            await productRepository.UpdateAsync(product); // Marcar producto como modificado
        }

        // 4. Crear la Orden
        var newOrder = new Order
        {
            CustomerId = request.CustomerId,
            OrderDate = DateTime.UtcNow,
            ShippingAddress = request.ShippingAddress ?? customer.Address, // Usar dirección del cliente si no se provee
            OrderStatus = OrderStatusPending, // Estado inicial
            TotalAmount = totalOrderAmount,
            OrderItems = orderItems, // Asignar la colección de items creados
        };

        // 5. Guardar todo transaccionalmente
        try
        {
            await _unitOfWork.OrderRepository.AddAsync(newOrder); // Añadir la orden (EF Core debería añadir los OrderItems por cascada si está configurado)
            // int changes = await _unitOfWork.SaveChangesAsync();

            // if (changes == 0) // Verificar si algo se guardó
            // {
            //     return Result.Error("Failed to create the order. No changes were saved to the database.");
            // }

            // 6. Mapear y devolver la respuesta
            // Recargar la orden con detalles para la respuesta es una buena práctica
            var createdOrderWithDetails = await _unitOfWork.OrderRepository.GetOrderWithDetailsAsync(newOrder.OrderId);
            if (createdOrderWithDetails == null)
                return Result.Error("Order created but could not be retrieved with details."); // Error inesperado

            var responseDto = _mapper.Map<OrderResponseDTO>(createdOrderWithDetails);
            return Result.Created(responseDto, ReplyMessages.Success.Save);
        }
        catch (Exception ex)
        {
            // Loggear el error (ex)
            // Considerar: ¿Se debería intentar revertir el stock si SaveChanges falla? Es complejo.
            // Una opción es usar patrones más avanzados como Sagas si la consistencia distribuida es crítica.
            return Result.Error($"An error occurred while creating the order: {ex.Message}");
        }
    }

    public async Task<Result<OrderResponseDTO>> GetOrderByIdAsync(int orderId)
    {
        var order = await _unitOfWork.OrderRepository.GetOrderWithDetailsAsync(orderId);
        if (order == null)
            return Result.NotFound($"Order with ID {orderId} not found.");

        var responseDto = _mapper.Map<OrderResponseDTO>(order);
        return Result.Success(responseDto, ReplyMessages.Success.Query);
    }

    public async Task<Result<IEnumerable<OrderResponseDTO>>> GetOrdersByCustomerIdAsync(int customerId)
    {
        var orders = await _unitOfWork.OrderRepository.GetOrdersByCustomerIdAsync(customerId);
        // No es NotFound si está vacío, es Success con lista vacía
        var responseDtos = _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        return Result.Success(responseDtos, ReplyMessages.Success.Query);
    }

    public async Task<Result> UpdateOrderStatusAsync(UpdateOrderStatusRequestDTO request)
    {
        var validationResult = await _updateStatusValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return _validatorServices.GetInvalidResult(validationResult);

        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId);
        if (order == null)
            return Result.NotFound($"Order with ID {request.OrderId} not found.");

        // VALIDACIÓN DE TRANSICIÓN DE ESTADO (Ejemplo simple)
        // Podría ser más complejo (ej: no se puede cancelar si ya está enviado)
        if (order.OrderStatus == OrderStatusCancelled || order.OrderStatus == OrderStatusShipped)
        {
            return Result.Conflict($"Cannot update status for an order that is already '{order.OrderStatus}'.");
        }
        // Aquí podrías añadir más lógica de qué transiciones son válidas

        order.OrderStatus = request.NewStatus;

        try
        {
            await _unitOfWork.OrderRepository.UpdateAsync(order); // Marcar como modificado
            return Result.Success();
        }
        catch (Exception ex)
        {
            // Loggear ex
            return Result.Error($"An error occurred while updating order status: {ex.Message}");
        }
    }
}
