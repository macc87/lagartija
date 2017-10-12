using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.DbContexts;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContext Context { get; }
        Task<bool> Commit();
    }


}