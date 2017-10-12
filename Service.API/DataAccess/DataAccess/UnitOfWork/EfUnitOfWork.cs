using System;
using System.Threading.Tasks;
using DataAccess.DbContexts;

namespace DataAccess.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        readonly IDbContext _context;
        public IDbContext Context { get { return _context; } }

        public EfUnitOfWork( IDbContext context )
        {
            _context = context;
        }
        public async Task<bool> Commit()
        {
            try
            {
                var result = _context.SaveChanges();

                return await Task.FromResult( result > 0 );
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
