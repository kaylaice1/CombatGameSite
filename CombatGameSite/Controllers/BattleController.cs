using CombatGameSite.Data;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Controllers
{
    public class BattleController : Controller
    {
        private readonly CombatGameDbContext _context;

        public BattleController(CombatGameDbContext context)
        {
            _context = context;
        }

        // GET: Battle/Index
        public IActionResult Index()
        {
            var battles = _context.Battles
                .Include(b => b.Team1)
                .Include(b => b.Team2)
                .ToList();

            if (battles == null || !battles.Any())
            {
                return View("Error"); // Render error view if no battles exist
            }

            return View(battles); // Pass the battles to the view
        }

        // GET: Battle/Create
        public IActionResult Create()
        {
            var teams = _context.Teams.ToList();
            ViewBag.Teams = new SelectList(teams, "Id", "Name");

            return View();
        }

        // POST: Battle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Battle battle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(battle);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, return to the Create view with teams again
            var teams = _context.Teams.ToList();
            ViewBag.Teams = new SelectList(teams, "Id", "Name");
            return View(battle);
        }
    }
}
