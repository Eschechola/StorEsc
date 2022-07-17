using MediatR;

namespace StorEsc.Core.Communication.Mediator.Notifications;

public abstract class Notification : INotification
{
    public Guid Hash { get; private set; }
    public DateTime PublishedAt { get; private set; }

    public Notification()
    {
        Hash = Guid.NewGuid();
        PublishedAt = DateTime.Now;
    }
}