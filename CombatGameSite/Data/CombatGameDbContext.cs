using CombatGameSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Data;

public class CombatGameDbContext(DbContextOptions<CombatGameDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Battle> Battles { get; set; }

    public static void Initialize(IServiceProvider serviceProvider, CombatGameDbContext context)
    {
        if (!context.Users.Any())
        {
            var user1 = new User { Username = "user1" };
            var user2 = new User { Username = "user2" };
            context.Users.AddRange(user1, user2);
            context.SaveChanges();

            // Initialize sample teams for users
            var team1 = new Team { Name = "Team A", UserId = user1.Id };
            var team2 = new Team { Name = "Team B", UserId = user2.Id };
            context.Teams.AddRange(team1, team2);
            context.SaveChanges();

            // Initialize characters for the teams
            var character1 = new Character
            {
                Name = "Character1",
                TeamId = team1.Id,
                Strength = 50,
                Agility = 50,
                Intelligence = 50,
                SpecialMove = "Basic Attack"
            };
            var character2 = new Character
            {
                Name = "Character2",
                TeamId = team2.Id,
                Strength = 60,
                Agility = 40,
                Intelligence = 45,
                SpecialMove = "Quick Strike"
            };
            context.Characters.AddRange(character1, character2);
            context.SaveChanges();
        }
    }
}