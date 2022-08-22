using Microsoft.Extensions.DependencyInjection;
using StorEsc.Infrastructure.Interfaces.Repositories;
using StorEsc.Infrastructure.Repositories;

namespace StorEsc.IoC.Dependencies.Repositories;

public static class RepositoriesDependencies
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    } 
}