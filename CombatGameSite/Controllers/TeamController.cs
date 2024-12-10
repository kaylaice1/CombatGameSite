using CombatGameSite.Data;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Controllers
{
    public class TeamController : Controller
    {
        private readonly CombatGameDbContext _context;

        public TeamController(CombatGameDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            // Fetch Characters from the database
            var characters = _context.Characters.ToList();

            // Ensure characters list is not null
            if (characters == null || !characters.Any())
            {
                ModelState.AddModelError(string.Empty, "No characters available to create a team.");
                return View(new Team()); // Return with an empty model
            }

            // Pass characters to the view
            ViewBag.Characters = characters;

            return View(new Team());
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, List<int> characterIds)
        {
            // Check if the team name or characters are empty or null
            if (string.IsNullOrEmpty(name) || characterIds == null || characterIds.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Team name and characters are required.");
                return View();
            }

            // Ensure no more than 5 characters are selected
            if (characterIds.Count > 5)
            {
                ModelState.AddModelError(string.Empty, "You cannot select more than 5 characters for a team.");
                return View();
            }

            // Fetch selected characters from the database
            var characters = _context.Characters.Where(c => characterIds.Contains(c.Id)).ToList();

            // Check if all selected characters exist in the database
            if (characters.Count != characterIds.Count)
            {
                ModelState.AddModelError(string.Empty, "Some selected characters do not exist.");
                return View();
            }

            // Create the new team
            var team = new Team
            {
                Name = name,
                Characters = characters // Associate the characters with the team
            };

            // Add the new team to the database
            _context.Teams.Add(team);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
