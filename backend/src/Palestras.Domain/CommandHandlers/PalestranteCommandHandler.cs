using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Palestras.Domain.Commands.Palestrante;
using Palestras.Domain.Core.Bus;
using Palestras.Domain.Core.Notifications;
using Palestras.Domain.Events.Palestrante;
using Palestras.Domain.Interfaces;
using Palestras.Domain.Models;

namespace Palestras.Domain.CommandHandlers
{
    public class PalestranteCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewPalestranteCommand>,
        IRequestHandler<UpdatePalestranteCommand>,
        IRequestHandler<RemovePalestranteCommand>
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IMediatorHandler Bus;

        public PalestranteCommandHandler(IPalestranteRepository palestranteRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _palestranteRepository = palestranteRepository;
            Bus = bus;
        }

        public Task Handle(RegisterNewPalestranteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var palestrante = new Palestrante(Guid.NewGuid(), message.Nome, message.MiniBio, message.Url);

            var existingpalestrante = _palestranteRepository.GetByNome(palestrante.Nome);

            if (existingpalestrante != null && existingpalestrante.Id != palestrante.Id)
                if (!existingpalestrante.Equals(palestrante))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "Palestrante já cadastrado!"));
                    return Task.CompletedTask;
                }


            _palestranteRepository.Add(palestrante);

            if (Commit())
                Bus.RaiseEvent(new PalestranteRegisteredEvent(palestrante.Id, palestrante.Nome, palestrante.MiniBio, palestrante.Url));

            return Task.CompletedTask;
        }

        public Task Handle(RemovePalestranteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            _palestranteRepository.Remove(message.Id);

            if (Commit()) Bus.RaiseEvent(new PalestranteRemovedEvent(message.Id));

            return Task.CompletedTask;
        }

        public Task Handle(UpdatePalestranteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var palestrante = new Palestrante(message.Id, message.Nome, message.MiniBio, message.Url);
            var existingpalestrante = _palestranteRepository.GetByNome(palestrante.Nome);

            if (existingpalestrante != null && existingpalestrante.Id != palestrante.Id)
                if (existingpalestrante.Id != palestrante.Id)
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "Palestrante já cadastrado!"));
                    return Task.CompletedTask;
                }

            _palestranteRepository.Update(palestrante);

            if (Commit())
                Bus.RaiseEvent(new PalestranteUpdatedEvent(palestrante.Id, palestrante.Nome, palestrante.MiniBio, palestrante.Url));

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _palestranteRepository.Dispose();
        }
    }
}