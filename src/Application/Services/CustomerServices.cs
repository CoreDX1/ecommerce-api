using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using Ardalis.Result;
using AutoMapper;
using Domain.Entity;

namespace Application.Services;

public class CustomerServices : ICustomerServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerServices(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CustomerResponseDTO>> CreateCustomer(CreateCustomerRequestDTO customer)
    {
        var customerDTo = _mapper.Map<Customer>(customer);
        customerDTo.RegistrationDate = DateTime.Now;

        await _unitOfWork.Customer.AddAsync(customerDTo);

        return Result.Success(
            _mapper.Map<CustomerResponseDTO>(customerDTo),
            "Customer created successfully"
        );
    }

    public Task<Result> DeleteCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<CustomerResponseDTO>>> GetAllCustomers()
    {
        var customers = await _unitOfWork.Customer.GetAllAsync();

        if (customers == null)
            return Result.NotFound("Customers not found");

        var customersDto = _mapper.Map<IEnumerable<CustomerResponseDTO>>(customers);

        return Result.Success(customersDto, "Customers retrieved successfully");
    }

    public async Task<Result<CustomerResponseDTO>> GetCustomerById(int customerId)
    {
        var customer = await _unitOfWork.Customer.GetByIdAsync(customerId);

        if (customer == null)
            return Result.NotFound("Customer not found");

        var customerResponse = _mapper.Map<CustomerResponseDTO>(customer);

        return Result.Success(customerResponse, "Customer retrieved successfully");
    }

    public Task<Result> UpdateCustomer(UpdateCustomerRequestDTO customer)
    {
        throw new NotImplementedException();
    }
}
