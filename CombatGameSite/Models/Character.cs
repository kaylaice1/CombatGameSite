using System.ComponentModel.DataAnnotations;

namespace CombatGameSite.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Strength is required")]
        [Range(1, 100, ErrorMessage = "Strength must be between 1 and 100")]
        public int Strength { get; set; }

        [Required(ErrorMessage = "Agility is required")]
        [Range(1, 100, ErrorMessage = "Agility must be between 1 and 100")]
        public int Agility { get; set; }

        [Required(ErrorMessage = "Intelligence is required")]
        [Range(1, 100, ErrorMessage = "Intelligence must be between 1 and 100")]
        public int Intelligence { get; set; }

        public int TotalPoints { get; set; } = 0;

        public string SpecialMove { get; set; } = string.Empty;

        public int? TeamId { get; set; }
        public Team? Team { get; set; }
    }
}