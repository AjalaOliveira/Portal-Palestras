using System;
using System.Collections.Generic;
using Palestras.Application.EventSourcedNormalizers;
using Palestras.Application.ViewModels;

namespace Palestras.Application.Interfaces
{
    public interface IPalestranteAppService : IDisposable
    {
        void Register(PalestranteViewModel palestranteViewModel);
        IEnumerable<PalestranteViewModel> GetAll();
        PalestranteViewModel GetById(Guid id);
        void Update(PalestranteViewModel palestranteViewModel);
        void Remove(Guid id);
        IList<PalestranteHistoryData> GetAllHistory(Guid id);
    }
}