using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Palestras.Domain.Events.Palestra;

namespace Palestras.Domain.EventHandlers
{
    public class PalestraEventHandler :
        INotificationHandler<PalestraRegisteredEvent>,
        INotificationHandler<PalestraRemovedEvent>,
        INotificationHandler<PalestraUpdatedEvent>
    {
        public Task Handle(PalestraRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(PalestraRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(PalestraUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}