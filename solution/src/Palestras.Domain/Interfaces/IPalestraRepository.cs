using Palestras.Domain.Models;
using System;
using System.Collections.Generic;

namespace Palestras.Domain.Interfaces
{
    public interface IPalestraRepository : IRepository<Palestra>
    {
        Palestra GetByTitulo(string titulo);
        IEnumerable<Palestra> GetPalestrasByPalestranteId(Guid paletranteId);
        IEnumerable<Palestra> SearchByDate(DateTime data);
        Palestra GetConflitctPalestranteDate(DateTime data, Guid id);
        IEnumerable<Palestra> GetAllCompleteList();
        Palestra GetConflitctPalestranteTitulo(DateTime data, string titulo);
    }
}