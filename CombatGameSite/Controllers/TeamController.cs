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

        // GET: Team/Index
        public IActionResult Index()
        {
            var teams = _context.Teams.Include(t => t.Characters).ToList();

            if (teams == null || !teams.Any())
            {
                return View("Error"); // Render error view if no teams exist
            }

            return View(teams); // Pass the teams to the view
        }

        // GET: Team/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(team); // Return to the Create view if there are validation errors
        }
    }
}
