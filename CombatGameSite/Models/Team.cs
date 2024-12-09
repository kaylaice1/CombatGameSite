namespace CombatGameSite.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; } 
        public User? User { get; set; }
        public List<Character> Characters { get; set; }
    }

}
