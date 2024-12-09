using CombatGameSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Data
{
    public class CombatGameDbContext : DbContext
    {
        public CombatGameDbContext(DbContextOptions<CombatGameDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } 
        public DbSet<Team> Teams { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public static void Initialize(IServiceProvider serviceProvider, CombatGameDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var user1 = new User { Username = "user1" };
                var user2 = new User { Username = "user2" };

                context.Users.AddRange(user1, user2);
                context.SaveChanges();
            }
        }

    }
}
