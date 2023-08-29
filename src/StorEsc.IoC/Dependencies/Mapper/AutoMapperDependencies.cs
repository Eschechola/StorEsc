﻿using Microsoft.Extensions.DependencyInjection;
using StorEsc.IoC.Dependencies.Mapper.Profiles;

namespace StorEsc.IoC.Dependencies.Mapper;

public static class AutoMapperDependencies
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AdministratorProfile));
        services.AddAutoMapper(typeof(CustomerProfile));
        services.AddAutoMapper(typeof(WalletProfile));
        services.AddAutoMapper(typeof(OrderProfile));
        services.AddAutoMapper(typeof(ProductProfile));
        services.AddAutoMapper(typeof(VoucherProfile));
        services.AddAutoMapper(typeof(RechargeProfile));
        services.AddAutoMapper(typeof(CreditCardProfile));

        return services;
    }
}