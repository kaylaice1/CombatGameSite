using CombatGameSite.Data;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this for Include method

public class HomeController : Controller
{
    private readonly CombatGameDbContext _context;

    public HomeController(CombatGameDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            Teams = _context.Teams.ToList(),
            Battles = _context.Battles
                            .Include(b => b.Team1)
                            .Include(b => b.Team2)
                            .Include(b => b.WinningTeam)
                            .OrderByDescending(b => b.BattleDate)
                            .ToList()
        };

        return View(viewModel);
    }
}
