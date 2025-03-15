using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapping;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<CreateCustomerRequestDTO, Customer>();
        CreateMap<Customer, CustomerResponseDTO>();
    }
}
