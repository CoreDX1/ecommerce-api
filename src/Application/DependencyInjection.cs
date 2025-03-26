using System.Reflection;
using Application.Common.Interfaces;
using Application.Configuration;
using Application.DTOs.Request.Product;
using Application.DTOs.Request.User;
using Application.Services;
using Application.Validations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services
        services.AddScoped<IUserService, UserServices>();
        services.AddScoped<ICustomerServices, CustomerServices>();
        services.AddScoped<IValidatorServices, ValidatorServices>();
        services.AddScoped<IUserRolesService, UserRolesService>();

        // Generic Services
        services.AddScoped(typeof(IReadServiceAsync<,>), typeof(ReadServiceAsync<,>));
        services.AddScoped(typeof(IGenericServiceAsync<,>), typeof(GenericServiceAsync<,>));

        // Asset Generic
        services.AddScoped(typeof(IProductServices), typeof(ProductServices));

        // Add AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Add FluentValidation
        services.AddScoped<IValidator<LoginUserRequestDTO>, LoginUserRequestValidations>();
        services.AddScoped<IValidator<CreateProductRequestDTO>, CreateProductValidations>();

        services.AddScoped<IValidator<UpdateProductRequestDTO>, UpdateProductValidations>();

        services.Configure<JwtConfig>(configuration.GetSection("Jwt"));

        return services;
    }
}
