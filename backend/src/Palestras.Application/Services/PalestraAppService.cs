using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Palestras.Application.EventSourcedNormalizers;
using Palestras.Application.Interfaces;
using Palestras.Application.ViewModels;
using Palestras.Domain.Commands.Palestra;
using Palestras.Domain.Core.Bus;
using Palestras.Domain.Interfaces;
using Palestras.Infra.Data.Repository.EventSourcing;

namespace Palestras.Application.Services
{
    public class PalestraAppService : IPalestraAppService
    {
        private readonly IPalestraRepository _palestraRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        public PalestraAppService(IMapper mapper,
            IPalestraRepository palestraRepository,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _palestraRepository = palestraRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<PalestraViewModel> GetAll()
        {
            return _palestraRepository.GetAll().ProjectTo<PalestraViewModel>();
        }

        public PalestraViewModel GetById(Guid id)
        {
            return _mapper.Map<PalestraViewModel>(_palestraRepository.GetById(id));
        }

        public void Register(PalestraViewModel palestraViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewPalestraCommand>(palestraViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(PalestraViewModel palestraViewModel)
        {
            var updateCommand = _mapper.Map<UpdatePalestraCommand>(palestraViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemovePalestraCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<PalestraHistoryData> GetAllHistory(Guid id)
        {
            return PalestraHistory.ToJavaScriptPalestraHistory(_eventStoreRepository.All(id));
        }

        public IEnumerable<PalestraViewModel> GetPalestrasByPalestranteId(Guid paletranteId)
        {
            return _mapper.Map<IEnumerable<PalestraViewModel>>(_palestraRepository.GetPalestrasByPalestranteId(paletranteId));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}