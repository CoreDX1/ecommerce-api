using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using Application.Interface;
using Ardalis.Result;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
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
    public async Task<Result<CustomerResponseDTO>> CreateCustomer(
        [FromBody] CreateCustomerRequestDTO customer
    )
    {
        return await _customerServices.CreateCustomer(customer);
    }
}
