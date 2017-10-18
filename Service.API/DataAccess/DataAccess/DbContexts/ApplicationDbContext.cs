using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using DataAccess.Models.DummyModel;

namespace DataAccess.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<FantasyUser>
    {
        public string UploadPath { get; set; }
        public string Path { get; set; }

        public ApplicationDbContext()
            : base("FantasyLeague")
        {
            Path = @"D:\Work\Freelance\FantasyLeague\Project\website\website";
            UploadPath = Path + @"\Media\";
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ClimaConditions> ClimaConditions { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<MLBPlayer> MLBPlayers { get; set; }
        public DbSet<LineUp> LineUps { get; set; }
        public DbSet<LineUpToPlayer> LineUpToPlayers { get; set; }
        public DbSet<LineUpToContest> LineUpToContests { get; set; }
        public DbSet<ContestType> ContestTypes { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<ContestToGame> ContestToGames { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

    }
}
