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

            // Pass battles to the view using ViewBag
            ViewBag.Battles = battles;
            return View();
        }

        // GET: Battle/Create
        public IActionResult Create()
        {
            // Fetch teams to display in the dropdown
            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
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

            // Re-populate teams in case of validation failure
            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
            return View(battle);
        }
    }
}
