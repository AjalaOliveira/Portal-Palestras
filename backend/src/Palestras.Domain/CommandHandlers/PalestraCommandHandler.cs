using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Palestras.Domain.Commands.Palestra;
using Palestras.Domain.Core.Bus;
using Palestras.Domain.Core.Notifications;
using Palestras.Domain.Events.Palestra;
using Palestras.Domain.Interfaces;
using Palestras.Domain.Models;

namespace Palestras.Domain.CommandHandlers
{
    public class PalestraCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewPalestraCommand>,
        IRequestHandler<UpdatePalestraCommand>,
        IRequestHandler<RemovePalestraCommand>
    {
        private readonly IPalestraRepository _palestraRepository;
        private readonly IMediatorHandler Bus;

        public PalestraCommandHandler(IPalestraRepository palestraRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _palestraRepository = palestraRepository;
            Bus = bus;
        }

        public Task Handle(RegisterNewPalestraCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var palestra = new Palestra(Guid.NewGuid(), message.Titulo, message.Email, message.BirthDate);

            if (_palestraRepository.GetByEmail(palestra.Email) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "O e-mail informado já está em uso."));
                return Task.CompletedTask;
            }

            _palestraRepository.Add(palestra);

            if (Commit()) Bus.RaiseEvent(new PalestraRegisteredEvent(palestra.Id, palestra.Titulo, palestra.Email, palestra.BirthDate));

            return Task.CompletedTask;
        }

        public Task Handle(RemovePalestraCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            _palestraRepository.Remove(message.Id);

            if (Commit()) Bus.RaiseEvent(new PalestraRemovedEvent(message.Id));

            return Task.CompletedTask;
        }

        public Task Handle(UpdatePalestraCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.CompletedTask;
            }

            var palestra = new Palestra(message.Id, message.Titulo, message.Email, message.BirthDate);
            var existingPalestra = _palestraRepository.GetByEmail(palestra.Email);

            if (existingPalestra != null && existingPalestra.Id != palestra.Id)
                if (!existingPalestra.Equals(palestra))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "O e-mail informado já está em uso."));
                    return Task.CompletedTask;
                }

            _palestraRepository.Update(palestra);

            if (Commit()) Bus.RaiseEvent(new PalestraUpdatedEvent(palestra.Id, palestra.Titulo, palestra.Email, palestra.BirthDate));

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _palestraRepository.Dispose();
        }
    }
}