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

        // POST action for processing the battle
        [HttpPost]
        public IActionResult StartBattle(int team1Id, int team2Id)
        {
            // Check if the teams are different
            if (team1Id == team2Id)
            {
                TempData["Error"] = "You must select two different teams.";
                return RedirectToAction("Index", "Home");
            }

            // Fetch teams from the database
            var team1 = _context.Teams.FirstOrDefault(t => t.Id == team1Id);
            var team2 = _context.Teams.FirstOrDefault(t => t.Id == team2Id);

            if (team1 == null || team2 == null)
            {
                TempData["Error"] = "One or both teams could not be found.";
                return RedirectToAction("Index", "Home");
            }

            // Simulate battle logic and determine the winner
            var random = new Random();
            var winner = random.Next(0, 2) == 0 ? team1 : team2;

            // Save battle results to the database
            var battle = new Battle
            {
                Team1 = team1,
                Team2 = team2,
                WinningTeam = winner,
                BattleDate = DateTime.Now
            };

            _context.Battles.Add(battle);
            _context.SaveChanges();

            // Redirect to the BattleResult page with the battle's ID
            return RedirectToAction("BattleResult", new { battleId = battle.Id });
        }

        // GET action for viewing the battle result
        public IActionResult BattleResult(int battleId)
        {
            var battle = _context.Battles
                                 .Include(b => b.Team1)
                                 .Include(b => b.Team2)
                                 .Include(b => b.WinningTeam)
                                 .FirstOrDefault(b => b.Id == battleId);

            if (battle == null)
            {
                return NotFound();
            }

            return View(battle);
        }
    }
}
