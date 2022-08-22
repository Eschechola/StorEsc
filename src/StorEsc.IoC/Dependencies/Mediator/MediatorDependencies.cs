using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StorEsc.Core.Communication.Mediator.Facades;
using StorEsc.Core.Communication.Mediator.Handlers;
using StorEsc.Core.Communication.Mediator.Interfaces;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.IoC.Dependencies.Mediator;

public static class MediatorDependencies
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddScoped<IDomainNotificationFacade, DomainNotificationFacade>();
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        return services;
    }
}