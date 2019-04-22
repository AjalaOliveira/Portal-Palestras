using Palestras.Domain.Models;
using System;

namespace Palestras.Domain.Interfaces
{
    public interface IPalestraRepository : IRepository<Palestra>
    {
        Palestra GetByDescricao(string titulo);
    }
}