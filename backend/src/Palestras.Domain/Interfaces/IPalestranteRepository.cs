using Palestras.Domain.Models;
using System;

namespace Palestras.Domain.Interfaces
{
    public interface IPalestranteRepository : IRepository<Palestrante>
    {
        Palestra GetByPalestranteId(Guid palestranteId);
    }
}