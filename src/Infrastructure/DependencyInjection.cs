using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        // services.AddGenericRepository<PostgresContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUsersRolesRepository, UsersRolesRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>(); // <-- Add this line

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddDbContext<PostgresContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);
        return services;
    }
}
