using CombatGameSite.Data;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly CombatGameDbContext _context;

        public HomeController(CombatGameDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            // Fetch all teams from the database
            var teams = _context.Teams.Include(t => t.Characters).ToList();

            // Check if the teams list is empty or null
            if (teams == null || !teams.Any())
            {
                return View("Error"); // Render error view if no teams are available
            }

            return View(teams); // Pass the teams list to the view
        }
    }
}
