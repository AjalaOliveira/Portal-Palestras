using Palestras.Domain.Models;

namespace Palestras.Domain.Interfaces
{
    public interface IPalestraRepository : IRepository<Palestra>
    {
        Palestra GetByEmail(string email);
    }
}