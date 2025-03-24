using Application.Common.Interfaces;
using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Customer;

[ApiController]
[Route("api/[controller]")]
public class Customer : ControllerBase
{
    private readonly ICustomerServices _customerServices;

    public Customer(ICustomerServices customerServices)
    {
        _customerServices = customerServices;
    }

    /// <summary>
    /// Get all customers
    /// </summary>
    /// <returns>A list of customers</returns>
    [HttpGet("all")] // GET: api/Customers
    public async Task<Result<IEnumerable<CustomerResponseDTO>>> GetAllCustomers()
    {
        return await _customerServices.GetAllCustomers();
    }

    /// <summary>
    /// Create a new customer
    /// </summary>
    /// <param name="customer">The customer to create</param>
    /// <returns>The created customer</returns>
    [HttpPost("create")] // POST: api/Customers
    public async Task<Result<CustomerResponseDTO>> CreateCustomer([FromBody] CreateCustomerRequestDTO customer)
    {
        return await _customerServices.CreateCustomer(customer);
    }

    /// <summary>
    /// Update a customer
    /// </summary>
    /// <param name="customer">The customer to update</param>
    /// <returns>The updated customer</returns>
    [HttpPut("{customerId}")] // PUT: api/Customers/5
    public async Task<Result<CustomerResponseDTO>> UpdateCustomer([FromBody] UpdateCustomerRequestDTO customer)
    {
        return await _customerServices.UpdateCustomer(customer);
    }

    /// <summary>
    /// Delete a customer
    /// </summary>
    /// <param name="customerId">The id of the customer to delete</param>
    /// <returns></returns>
    [HttpDelete("{customerId}")] // DELETE: api/Customers/5
    public async Task<Result> DeleteCustomer([FromRoute] int customerId)
    {
        return await _customerServices.DeleteCustomer(customerId);
    }
}
