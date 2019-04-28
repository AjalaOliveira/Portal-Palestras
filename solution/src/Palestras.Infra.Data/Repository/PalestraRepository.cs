using System;
using System.Collections.Generic;
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

        public Palestra GetByTitulo(string titulo)
        {
            return DbSet.AsNoTracking()
                .FirstOrDefault(c => c.Titulo == titulo);
        }

        public override Palestra GetById(Guid id)
        {
            return Db.Palestras.Where(c => c.Id == id)
                .Include(c => c.Palestrante)
                .FirstOrDefault();
        }

        public IEnumerable<Palestra> GetPalestrasByPalestranteId(Guid paletranteId)
        {
            return Db.Palestras
                .Where(c => c.PalestranteId == paletranteId)
                .ToList();
        }

        public IEnumerable<Palestra> SearchByDate(DateTime data)
        {
            return Db.Palestras.Include(c => c.Palestrante)
                .Where(c => c.Data == data).ToList();
        }

        public Palestra GetConflitctPalestranteDate(DateTime data, Guid id)
        {
            return Db.Palestras
                .Where(c => c.Data == data && c.PalestranteId == id)
                .SingleOrDefault();
        }

        public IEnumerable<Palestra> GetAllCompleteList()
        {
            return Db.Palestras
                .Include(c => c.Palestrante).ToList();
        }

        public Palestra GetConflitctPalestranteTitulo(DateTime data, string titulo)
        {
            return Db.Palestras
                .Where(c => c.Data == data && c.Titulo == titulo)
                .SingleOrDefault();
        }
    }
}