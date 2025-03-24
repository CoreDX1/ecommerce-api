using System.Reflection;
using Application.Common.Interfaces;
using Application.Configuration;
using Application.DTOs.Request.User;
using Application.Interfaces;
using Application.Services;
using Application.Validations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Add services
        services.AddScoped<IUserService, UserServices>();
        services.AddScoped<ICustomerServices, CustomerServices>();

        // Generic Services
        services.AddScoped(typeof(IReadServiceAsync<,>), typeof(ReadServiceAsync<,>));
        services.AddScoped(typeof(IGenericServiceAsync<,>), typeof(GenericServiceAsync<,>));

        // Asset Generic
        services.AddScoped(typeof(IProductServices), typeof(ProductServices));

        // Add AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Add FluentValidation
        services.AddScoped<IValidator<LoginUserRequestDTO>, LoginUserRequestValidations>();

        services.Configure<JwtConfig>(configuration.GetSection("Jwt"));

        return services;
    }
}
