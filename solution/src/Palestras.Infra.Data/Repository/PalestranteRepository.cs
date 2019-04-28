using System;
using System.Collections.Generic;
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

        public Palestra GetByPalestranteId(Guid palestranteId)
        {
            return Db.Palestras.Where(c => c.PalestranteId == palestranteId)
                .Include(c => c.Palestrante)
                .FirstOrDefault();
        }

        public Palestrante GetByNome(string nome)
        {
            return DbSet.AsNoTracking()
                .FirstOrDefault(c => c.Nome == nome);
        }

        public IEnumerable<Palestrante> GetAllCompleteList()
        {
            return Db.Palestrantes
                .Include(c => c.Palestras).ToList();
        }
    }
}