using System;
using System.Collections.Generic;
using Palestras.Application.EventSourcedNormalizers;
using Palestras.Application.ViewModels;

namespace Palestras.Application.Interfaces
{
    public interface IPalestraAppService : IDisposable
    {
        void Register(PalestraViewModel palestraViewModel);
        IEnumerable<PalestraViewModel> GetAll();
        PalestraViewModel GetById(Guid id);
        void Update(PalestraViewModel palestraViewModel);
        void Remove(Guid id);
        IList<PalestraHistoryData> GetAllHistory(Guid id);
    }
}