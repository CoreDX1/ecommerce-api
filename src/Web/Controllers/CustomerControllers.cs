using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using Application.Interfaces;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerControllers : ControllerBase
{
    private readonly ICustomerServices _customerServices;

    public CustomerControllers(ICustomerServices customerServices)
    {
        _customerServices = customerServices;
    }

    [HttpGet("all")]
    public async Task<Result<List<CustomerResponseDTO>>> GetAllCustomers()
    {
        return await _customerServices.GetAllCustomers();
    }

    [HttpPost("create")]
    public async Task<Result<CustomerResponseDTO>> CreateCustomer([FromBody] CreateCustomerRequestDTO customer)
    {
        return await _customerServices.CreateCustomer(customer);
    }

    [HttpPut("{customerId}")]
    public async Task<Result<CustomerResponseDTO>> UpdateCustomer([FromBody] UpdateCustomerRequestDTO customer)
    {
        return await _customerServices.UpdateCustomer(customer);
    }

    [HttpDelete("{customerId}")]
    public async Task<Result> DeleteCustomer([FromRoute] int customerId)
    {
        return await _customerServices.DeleteCustomer(customerId);
    }
}
