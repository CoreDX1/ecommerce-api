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
        services.AddScoped<ICustomerServices, CustomerServices>();

        // Generic Services
        services.AddScoped(typeof(IReadServiceAsync<,>), typeof(ReadServiceAsync<,>));
        services.AddScoped(typeof(IGenericServiceAsync<,>), typeof(GenericServiceAsync<,>));

        // Asset Generic
        services.AddScoped(typeof(IProductServices), typeof(ProductServices));

        // Add AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
