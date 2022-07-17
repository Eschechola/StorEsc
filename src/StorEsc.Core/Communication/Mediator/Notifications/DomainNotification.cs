using StorEsc.Core.Communication.Mediator.Enums;

namespace StorEsc.Core.Communication.Mediator.Notifications;

public class DomainNotification : Notification
{
    public string Message { get; private set; }
    public dynamic Data { get; private set; }
    public DomainNotificationType Type { get; private set; }

    public DomainNotification(
        string message,
        DomainNotificationType type)
    {
        Message = message;
        Type = type;
    }
    
    public DomainNotification(
        string message,
        dynamic data,
        DomainNotificationType type)
    {
        Message = message;
        Data = data;
        Type = type;
    }

    public bool HasData()
        => Data != null;
}