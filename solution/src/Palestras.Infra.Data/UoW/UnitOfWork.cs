using Palestras.Domain.Interfaces;
using Palestras.Infra.Data.Context;

namespace Palestras.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PalestrasDbContext _context;

        public UnitOfWork(PalestrasDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}