using Palestras.Domain.Models;
using System;
using System.Collections.Generic;

namespace Palestras.Domain.Interfaces
{
    public interface IPalestranteRepository : IRepository<Palestrante>
    {
        Palestra GetByPalestranteId(Guid palestranteId);

        Palestrante GetByNome(string titulo);

        IEnumerable<Palestrante> GetAllCompleteList();
    }
}