namespace CombatGameSite.Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }

    // Navigation property to Teams
    public List<Team> Teams { get; set; } = new List<Team>();
}
