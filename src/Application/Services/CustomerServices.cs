using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using Application.Interface;
using Ardalis.Result;
using AutoMapper;
using Domain.Entity;
using TanvirArjel.EFCore.GenericRepository;

namespace Application.Services;

public class CustomerServices : ICustomerServices
{
    private readonly IRepository _repository;
    private readonly IMapper Mapper;

    public CustomerServices(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        Mapper = mapper;
    }

    public async Task<Result<CustomerResponseDTO>> CreateCustomer(CreateCustomerRequestDTO customer)
    {
        var customerDTo = Mapper.Map<Customer>(customer);
        customerDTo.RegistrationDate = DateTime.Now;

        await _repository.AddAsync(customerDTo);
        await _repository.SaveChangesAsync();

        return Result.Success(
            Mapper.Map<CustomerResponseDTO>(customerDTo),
            "Customer created successfully"
        );
    }

    public Task<Result> DeleteCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<CustomerResponseDTO>>> GetAllCustomers()
    {
        var customers = await _repository.GetListAsync<Customer>();

        if (customers == null)
        {
            return Result.NotFound("Customers not found");
        }

        var customersDto = Mapper.Map<List<CustomerResponseDTO>>(customers);

        return Result.Success(customersDto, "Customers retrieved successfully");
    }

    public async Task<Result<CustomerResponseDTO>> GetCustomerById(int customerId)
    {
        return await _repository.GetByIdAsync<CustomerResponseDTO>(customerId);
    }

    public Task<Result> UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }
}
