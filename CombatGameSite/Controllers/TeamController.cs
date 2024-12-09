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

        // GET: Team/Create
        public IActionResult Create()
        {
            // Fetch Users and Characters from the database
            var users = _context.Users.ToList();
            var characters = _context.Characters.ToList();

            // Pass them to the view using ViewBag
            ViewBag.Users = users;
            ViewBag.Characters = characters;

            return View();
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, List<int> characterIds, int userId)
        {
            if (string.IsNullOrEmpty(name) || characterIds == null || characterIds.Count == 0)
            {
                // Return error if validation fails
                ModelState.AddModelError(string.Empty, "Team name and characters are required.");
                return View();
            }

            // Create new Team object
            var team = new Team
            {
                Name = name,
                UserId = userId,  // Set UserId
                Characters = _context.Characters.Where(c => characterIds.Contains(c.Id)).ToList()
            };

            // Add the new team to the database
            _context.Teams.Add(team);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
