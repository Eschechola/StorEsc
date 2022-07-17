using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.Core.Communication.Mediator.Interfaces;

public interface IMediatorHandler
{
    Task PublishNotificationAsync<N>(N notification) 
        where N : Notification;
}