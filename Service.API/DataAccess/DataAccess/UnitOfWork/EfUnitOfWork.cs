using System;
using System.Threading.Tasks;
using Fantasy.API.DataAccess.DbContexts;

namespace Fantasy.API.DataAccess.UnitOfWork
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
