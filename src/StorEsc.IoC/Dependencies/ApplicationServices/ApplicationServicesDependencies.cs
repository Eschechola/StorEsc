﻿using Microsoft.Extensions.DependencyInjection;
using StorEsc.ApplicationServices.Interfaces;
using StorEsc.ApplicationServices.Services;

namespace StorEsc.IoC.Dependencies.ApplicationServices;

public static class ApplicationServicesDependencies
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthApplicationService, AuthApplicationService>();
        services.AddScoped<IWalletApplicationService, WalletApplicationService>();
        services.AddScoped<IRechargeApplicationService, RechargeApplicationService>();
        services.AddScoped<IProductApplicationService, ProductApplicationService>();
        services.AddScoped<IAdministratorApplicationService, AdministratorApplicationService>();
        services.AddScoped<IVoucherApplicationService, VoucherApplicationService>();

        return services;
    }
}