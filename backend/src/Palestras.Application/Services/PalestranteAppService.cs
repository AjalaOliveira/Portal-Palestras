using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Palestras.Application.EventSourcedNormalizers;
using Palestras.Application.Interfaces;
using Palestras.Application.ViewModels;
using Palestras.Domain.Commands.Palestrante;
using Palestras.Domain.Core.Bus;
using Palestras.Domain.Interfaces;
using Palestras.Infra.Data.Repository.EventSourcing;

namespace Palestras.Application.Services
{
    public class PalestranteAppService : IPalestranteAppService
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        public PalestranteAppService(IMapper mapper,
            IPalestranteRepository palestranteRepository,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _palestranteRepository = palestranteRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<PalestranteViewModel> GetAll()
        {
            return _palestranteRepository.GetAll().ProjectTo<PalestranteViewModel>();
        }

        public PalestranteViewModel GetById(Guid id)
        {
            return _mapper.Map<PalestranteViewModel>(_palestranteRepository.GetById(id));
        }

        public void Register(PalestranteViewModel palestranteViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewPalestranteCommand>(palestranteViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(PalestranteViewModel palestranteViewModel)
        {
            var updateCommand = _mapper.Map<UpdatePalestranteCommand>(palestranteViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemovePalestranteCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<PalestranteHistoryData> GetAllHistory(Guid id)
        {
            return PalestranteHistory.ToJavaScriptPalestranteHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}