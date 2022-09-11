using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorEsc.Api.ViewModels;
using StorEsc.Core.Communication.Mediator.Enums;
using StorEsc.Core.Communication.Mediator.Handlers;
using StorEsc.Core.Communication.Mediator.Notifications;

namespace StorEsc.Api.Controllers;

public abstract class BaseController : Controller
{
    private readonly DomainNotificationHandler _domainNotificationHandler;

    public BaseController()
    {
    }

    protected BaseController(INotificationHandler<DomainNotification> domainNotificationHandler)
    {
        _domainNotificationHandler = domainNotificationHandler as DomainNotificationHandler;
    }
    
    protected bool HasNotifications()
        => _domainNotificationHandler.HasNotifications();
    
    protected ObjectResult Created(dynamic responseObject)
        => StatusCode(201, responseObject);

    protected ObjectResult InternalServerError()
        => StatusCode(500, new ResultViewModel
        {
            Message = "An internal server error has been ocurred, please try again.",
            Success = false,
            Data = new { }
        });
    
    protected string NotificationMessage()
    {
        var notification = _domainNotificationHandler
            .Notifications
            .FirstOrDefault();

        return notification.Message;
    }
    
    protected ObjectResult Result()
    {
        var notification = _domainNotificationHandler
            .Notifications
            .FirstOrDefault();

        if (notification.HasData())
            return StatusCode(GetStatusCodeByNotificationType(notification.Type),
                new ResultViewModel
                {
                    Message = notification.Message,
                    Success = false,
                    Data = notification.Data
                });

        return StatusCode(GetStatusCodeByNotificationType(notification.Type),
            new ResultViewModel
            {
                Message = notification.Message,
                Success = false,
                Data = new { }
            });
    }

    private int GetStatusCodeByNotificationType(DomainNotificationType errorType)
    {
        return errorType switch
        {
            DomainNotificationType.EmailAndOrPasswordMismatch
                => 401,
            
            DomainNotificationType.CustomerAlreadyExists 
                or DomainNotificationType.CustomerDataIsInvalid
                or DomainNotificationType.SellerAlreadyExists
                or DomainNotificationType.SellerDataIsInvalid
                or DomainNotificationType.ProductDataIsInvalid
                => 400,
            
            DomainNotificationType.PaymentRefused
                => 402,
            
            DomainNotificationType.InternalServerError
                => 500,

            (_) => 500,
        };
    }

}