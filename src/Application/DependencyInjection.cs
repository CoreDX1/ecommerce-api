using System.Reflection;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add services
        services.AddScoped<IUserService, User>();
        services.AddScoped<IProductsServices, ProductsServices>();
        services.AddScoped<ICustomerServices, CustomerServices>();

        // Add AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
