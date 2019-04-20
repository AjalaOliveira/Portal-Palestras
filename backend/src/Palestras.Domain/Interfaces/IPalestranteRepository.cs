using Palestras.Domain.Models;

namespace Palestras.Domain.Interfaces
{
    public interface IPalestranteRepository : IRepository<Palestrante>
    {
        Palestrante GetByEmail(string email);
    }
}