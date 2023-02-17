using Microsoft.Extensions.DependencyInjection;
using StorEsc.DomainServices.Interfaces;
using StorEsc.DomainServices.Services;

namespace StorEsc.IoC.Dependencies.DomainServices;

public static class DomainServicesDependencies
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerDomainService, CustomerDomainService>();
        services.AddScoped<ISellerDomainService, SellerDomainService>();
        services.AddScoped<IWalletDomainService, WalletDomainService>();
        services.AddScoped<IPaymentDomainService, PaymentDomainService>();
        services.AddScoped<IRechargeDomainService, RechargeDomainService>();
        services.AddScoped<IProductDomainService, ProductDomainService>();
        services.AddScoped<IAdministratorDomainService, AdministratorDomainService>();

        return services;
    }
}