using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using Ardalis.Result;
using Domain.Entity;

namespace Application.Interfaces;

public interface ICustomerServices
{
    Task<Result<List<CustomerResponseDTO>>> GetAllCustomers();
    Task<Result<CustomerResponseDTO>> GetCustomerById(int customerId);
    Task<Result<CustomerResponseDTO>> CreateCustomer(CreateCustomerRequestDTO customer);
    Task<Result> UpdateCustomer(UpdateCustomerRequestDTO customer);
    Task<Result> DeleteCustomer(int customerId);
}
