using CombatGameSite.Data;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CombatGameSite.Controllers;

public class CharacterController(CombatGameDbContext context) : Controller
{
    private readonly CombatGameDbContext _context = context;

    // GET: Character/Create
    [HttpGet]
    public IActionResult Create()
    {
        // Fetch teams to display in the dropdown
        ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
        return View(new Character());
    }

    // POST: Character/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Character character)
    {
        if (ModelState.IsValid)
        {
            // Set default value for TotalPoints if not provided
            if (character.TotalPoints == 0)
            {
                character.TotalPoints = character.Strength + character.Agility + character.Intelligence;
            }

            _context.Characters.Add(character);
            _context.SaveChanges();

            // Add a success message
            TempData["SuccessMessage"] = "Character created successfully!";

            return RedirectToAction("Index", "Home");
        }

        // If we got this far, something failed, redisplay form
        return View(character);
    }
}