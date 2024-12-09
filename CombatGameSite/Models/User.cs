using CombatGameSite.Models;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; } 
    public ICollection<Team>? Teams { get; set; }
}
