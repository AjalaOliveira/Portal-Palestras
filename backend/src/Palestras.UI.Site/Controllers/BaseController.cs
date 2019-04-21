using MediatR;
using Microsoft.AspNetCore.Mvc;
using Palestras.Domain.Core.Notifications;

namespace Palestras.UI.Site.Controllers
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;

        public BaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler) notifications;
        }

        public bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }
    }
}