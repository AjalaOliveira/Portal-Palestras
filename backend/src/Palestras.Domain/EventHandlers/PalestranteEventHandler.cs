using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Palestras.Domain.Events.Palestrante;

namespace Palestras.Domain.EventHandlers
{
    public class PalestranteEventHandler :
        INotificationHandler<PalestranteRegisteredEvent>,
        INotificationHandler<PalestranteUpdatedEvent>,
        INotificationHandler<PalestranteRemovedEvent>
    {
        public Task Handle(PalestranteRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(PalestranteRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }

        public Task Handle(PalestranteUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }
    }
}