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

            if (characters == null || !characters.Any())
            {
                return View("Error"); // Render error view if no characters exist
            }

            return View(characters); // Pass the characters to the view
        }

        // GET: Character/Create
        public IActionResult Create()
        {
            var teams = _context.Teams.ToList();
            ViewBag.Teams = new SelectList(teams, "Id", "Name");

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
                return RedirectToAction(nameof(Index));
            }

            var teams = _context.Teams.ToList();
            ViewBag.Teams = new SelectList(teams, "Id", "Name");
            return View(character); // Return to the Create view if there are validation errors
        }
    }
}
