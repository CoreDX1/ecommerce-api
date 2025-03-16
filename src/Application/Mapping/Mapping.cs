using Application.DTOs.Request.Customer;
using Application.DTOs.Response.Customer;
using Application.DTOs.Response.Product;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapping;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<CreateCustomerRequestDTO, Customer>();
        CreateMap<Customer, CustomerResponseDTO>();

        CreateMap<Product, ProductResponseDTO>();
        CreateMap<UpdateCustomerRequestDTO, Customer>();
        CreateMap<Customer, CustomerResponseDTO>();
    }
}
