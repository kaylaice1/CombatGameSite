using CombatGameSite.Data;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Controllers
{
    public class CharacterController : Controller
    {
        private readonly CombatGameDbContext _context;

        public CharacterController(CombatGameDbContext context)
        {
            _context = context;
        }

        // GET: Character/Index
        public IActionResult Index()
        {
            var characters = _context.Characters.Include(c => c.Team).ToList();

            // Pass characters list to view using ViewBag
            ViewBag.Characters = characters;
            return View();
        }

        // GET: Character/Create
        public IActionResult Create()
        {
            // Fetch teams to display in the dropdown
            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Character character)
        {
            if (ModelState.IsValid)
            {
                _context.Add(character);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            // In case of validation errors, re-populate teams
            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
            return View(character);
        }
    }
}
