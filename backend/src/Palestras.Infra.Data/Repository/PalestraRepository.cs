using System;
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

        public Palestra GetByDescricao(string descricao)
        {

            return DbSet.AsNoTracking()
                .FirstOrDefault(c => c.Descricao == descricao);
        }

        public override Palestra GetById(Guid id)
        {
            return Db.Palestras.Where(c => c.Id == id)
                .Include(c => c.Palestrante)
                .FirstOrDefault();
        }
    }
}