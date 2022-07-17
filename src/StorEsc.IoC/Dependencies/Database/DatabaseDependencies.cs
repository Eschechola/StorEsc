using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StorEsc.Infrastructure.Context;

namespace StorEsc.IoC.Dependencies.Database;

public static class DatabaseDependencies
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["Databases:ConnectionStrings:SqlServer"];

        services.AddDbContext<StorEscContext>(options =>
            options
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        return services;
    }
}