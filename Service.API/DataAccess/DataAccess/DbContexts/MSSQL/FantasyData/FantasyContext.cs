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
        public virtual DbSet<ClimaConditions> ClimaConditions { get; set; }
        public virtual DbSet<ContestType> ContestTypes { get; set; }
        public virtual DbSet<Contest> Contests { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<LineUp> LineUps { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<ContestGame> ContestGames { get; set; }
        public virtual DbSet<PlayerLineup> PlayerLineups { get; set; }
        public virtual DbSet<ContestLineup> ContestLineups { get; set; }
        public virtual DbSet<Information> Informations { get; set; }
        public virtual DbSet<AccountFriends> AccountFriends { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsPlayer> PlayerNews { get; set; }
        public virtual DbSet<NewsTeam> TeamNews { get; set; }
        public virtual DbSet<Injury> Injuries  { get; set; }


    }
}
