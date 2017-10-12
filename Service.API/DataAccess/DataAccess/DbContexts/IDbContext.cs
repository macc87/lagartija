using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DataAccess.DbContexts
{
    public interface IDbContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        IEnumerable<T> ExecuteQuery<T>(string query);
        int ExecuteCommand(string query);
        int SaveChanges();
        Database Database { get; }
        new void Dispose();

    }
}
