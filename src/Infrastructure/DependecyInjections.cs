using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using TanvirArjel.EFCore.GenericRepository;

namespace Infrastructure;

public static class DependecyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddGenericRepository<PostgresContext>();
        return services;
    }
}
