using CombatGameSite.Models;

public class Battle
{
    public int Id { get; set; }
    public int Team1Id { get; set; }
    public Team? Team1 { get; set; }

    public int Team2Id { get; set; }
    public Team? Team2 { get; set; }

    public int WinningTeamId { get; set; }  // This should be the foreign key to the winning team
    public Team? WinningTeam { get; set; }  // This represents the winning team (Navigation property)

    public DateTime BattleDate { get; set; }
}
