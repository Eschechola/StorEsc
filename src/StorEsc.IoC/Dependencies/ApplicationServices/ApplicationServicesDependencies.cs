﻿using Microsoft.Extensions.DependencyInjection;
using StorEsc.Application.Interfaces;
using StorEsc.Application.Services;

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

        return services;
    }
}