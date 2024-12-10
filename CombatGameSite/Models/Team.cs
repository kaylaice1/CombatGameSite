namespace CombatGameSite.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Foreign Key for User
    public int UserId { get; set; }
    public User User { get; set; }  // Navigation property to User

    // Navigation property to Characters
    public List<Character> Characters { get; set; } = new List<Character>();
}
