using MediatR;
using Palestras.Domain.Core.Bus;
using Palestras.Domain.Core.Commands;
using Palestras.Domain.Core.Notifications;
using Palestras.Domain.Interfaces;

namespace Palestras.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUnitOfWork _uow;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler) notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_uow.Commit()) return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "Ocorreu um problema ao salvar seus dados."));
            return false;
        }
    }
}