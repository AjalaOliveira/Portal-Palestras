using System.Linq;
using Microsoft.EntityFrameworkCore;
using Palestras.Domain.Interfaces;
using Palestras.Domain.Models;
using Palestras.Infra.Data.Context;

namespace Palestras.Infra.Data.Repository
{
    public class PalestraRepository : Repository<Palestra>, IPalestraRepository
    {
        public PalestraRepository(PalestrasDbContext context)
            : base(context)
        {
        }

        public Palestra GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}