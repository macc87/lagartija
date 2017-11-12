using Fantasy.API.DataAccess.Configurations;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;

namespace Fantasy.API.DataAccess.DbContexts.MSSQL.FantasyData
{
    public class FantasyContext : BaseContext<FantasyContext>, IDbContext
    {
        private static readonly string fantasyConnName;

        public FantasyContext() : base(fantasyConnName)
        {

        }
        public FantasyContext(DbConnection connection) : base(connection)
        {
        }
        static FantasyContext()
        {
            fantasyConnName = DataLayerEnvironment.GetInstance().FantasyMssqlProperties.ConnectionStringName;
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public IEnumerable<T> ExecuteQuery<T>(string query)
        {
            return this.Database.SqlQuery<T>(query);
        }

        public int ExecuteCommand(string query)
        {
            return this.Database.ExecuteSqlCommand(query);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountMapping());
            
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Account> Accounts { get; set; }
    }
}
