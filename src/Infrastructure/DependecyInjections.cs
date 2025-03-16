using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TanvirArjel.EFCore.GenericRepository;

namespace Infrastructure;

public static class DependecyInjections
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddGenericRepository<PostgresContext>();
        services.AddDbContext<PostgresContext>(
            options => options.UseNpgsql(connectionString),
            ServiceLifetime.Scoped
        );
        return services;
    }
}
