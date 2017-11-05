using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fantasy.API.DataAccess.DbContexts;

namespace Fantasy.API.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContext Context { get; }
        Task<bool> Commit();
    }


}