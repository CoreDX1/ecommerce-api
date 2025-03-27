using Application.DTOs.Request.Customer;
using Application.DTOs.Request.Product;
using Application.DTOs.Request.User;
using Application.DTOs.Response.Customer;
using Application.DTOs.Response.Product;
using Application.DTOs.Response.User;
using AutoMapper;
using Domain.Entity;

namespace Application.Common.Mapping;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<CreateCustomerRequestDTO, Customer>();
        CreateMap<Customer, CustomerResponseDTO>();
        CreateMap<UpdateCustomerRequestDTO, Customer>();
        CreateMap<Customer, CustomerResponseDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Product, ProductResponseDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
        CreateMap<UpdateProductRequestDTO, Product>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
        CreateMap<CreateProductRequestDTO, Product>();

        CreateMap<RegisterUserRequestDTO, User>();
        CreateMap<User, UserResponseDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
    }
}
