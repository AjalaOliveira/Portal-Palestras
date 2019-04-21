using System.Linq;
using Microsoft.EntityFrameworkCore;
using Palestras.Domain.Interfaces;
using Palestras.Domain.Models;
using Palestras.Infra.Data.Context;

namespace Palestras.Infra.Data.Repository
{
    public class PalestranteRepository : Repository<Palestrante>, IPalestranteRepository
    {
        public PalestranteRepository(PalestrasDbContext context)
            : base(context)
        {
        }

        public Palestrante GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}