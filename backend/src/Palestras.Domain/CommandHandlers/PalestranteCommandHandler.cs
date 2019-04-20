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

            var palestrante = new Palestrante(Guid.NewGuid(), message.Name, message.Email, message.BirthDate);

            if (_palestranteRepository.GetByEmail(palestrante.Email) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "O e-mail informado já está em uso."));
                return Task.CompletedTask;
            }

            _palestranteRepository.Add(palestrante);

            if (Commit())
                Bus.RaiseEvent(new PalestranteRegisteredEvent(palestrante.Id, palestrante.Name, palestrante.Email,
                    palestrante.BirthDate));

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

            var palestrante = new Palestrante(message.Id, message.Name, message.Email, message.BirthDate);
            var existingPalestrante = _palestranteRepository.GetByEmail(palestrante.Email);

            if (existingPalestrante != null && existingPalestrante.Id != palestrante.Id)
                if (!existingPalestrante.Equals(palestrante))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "O e-mail informado já está em uso."));
                    return Task.CompletedTask;
                }

            _palestranteRepository.Update(palestrante);

            if (Commit())
                Bus.RaiseEvent(new PalestranteUpdatedEvent(palestrante.Id, palestrante.Name, palestrante.Email,
                    palestrante.BirthDate));

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _palestranteRepository.Dispose();
        }
    }
}