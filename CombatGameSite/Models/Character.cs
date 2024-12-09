using CombatGameSite.Models;
using System.ComponentModel.DataAnnotations;

public class Character
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int TotalPoints { get; set; }

    [Required]
    [StringLength(100)]
    public string? SpecialMove { get; set; } 
    public int? TeamId { get; set; }
    public Team? Team { get; set; }

    internal bool IsValid()
    {
        throw new NotImplementedException();
    }
}
