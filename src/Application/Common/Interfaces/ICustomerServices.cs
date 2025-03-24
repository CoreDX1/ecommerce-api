using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using Ardalis.Result;

namespace Application.Common.Interfaces;

public interface ICustomerServices
{
    /// <summary>
    /// Obtener todos los clientes
    /// </summary>
    /// <returns> Resultado con la lista de clientes </returns>
    Task<Result<IEnumerable<CustomerResponseDTO>>> GetAllCustomers();
    Task<Result<CustomerResponseDTO>> GetCustomerById(int customerId);
    Task<Result<CustomerResponseDTO>> CreateCustomer(CreateCustomerRequestDTO customer);
    Task<Result> UpdateCustomer(UpdateCustomerRequestDTO customer);
    Task<Result> DeleteCustomer(int customerId);
}
