using Microsoft.Extensions.DependencyInjection;
using StorEsc.Infrastructure.ExternalServices.Interfaces;
using StorEsc.Infrastructure.ExternalServices.Services;

namespace StorEsc.IoC.Dependencies.ExternalServices;

public static class ExternalServicesDependencies
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentExternalService, PaymentExternalService>();
        
        return services;
    }
}