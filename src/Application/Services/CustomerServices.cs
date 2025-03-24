using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using Ardalis.Result;
using AutoMapper;
using Domain.Common.Constants;
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

        await _unitOfWork.CustomerRepository.AddAsync(customerDTo);

        return Result.Success(
            _mapper.Map<CustomerResponseDTO>(customerDTo),
            ReplyMessage.Success.Save
        );
    }

    public Task<Result> DeleteCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<CustomerResponseDTO>>> GetAllCustomers()
    {
        var customers = await _unitOfWork.CustomerRepository.GetAllAsync();

        if (customers == null)
            return Result.NotFound(ReplyMessage.Error.NotFound);

        var customersDto = _mapper.Map<IEnumerable<CustomerResponseDTO>>(customers);

        return Result.Success(customersDto, ReplyMessage.Success.Query);
    }

    public async Task<Result<CustomerResponseDTO>> GetCustomerById(int customerId)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);

        if (customer == null)
            return Result.NotFound(ReplyMessage.Error.NotFound);

        var customerResponse = _mapper.Map<CustomerResponseDTO>(customer);

        return Result.Success(customerResponse, ReplyMessage.Success.Query);
    }

    public Task<Result> UpdateCustomer(UpdateCustomerRequestDTO customer)
    {
        throw new NotImplementedException();
    }
}
