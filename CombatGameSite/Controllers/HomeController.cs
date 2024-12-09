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
        var model = new HomeViewModel
        {
            Teams = _context.Teams.Include(t => t.Characters).ToList(),
            Battles = _context.Battles
                                .Include(b => b.Team1)
                                .Include(b => b.Team2)
                                .Include(b => b.WinningTeam)
                                .ToList()
        };

        return View(model);
    }
}
