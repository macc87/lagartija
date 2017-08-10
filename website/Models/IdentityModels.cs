using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Owin;

namespace website.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class FantasyUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<FantasyUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FullName { get; set; }

        public int Points { get; set; }

        public float Cash { get; set; }

        public virtual ICollection<FantasyUser> Friends { get; set; }
    }

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
    }
}