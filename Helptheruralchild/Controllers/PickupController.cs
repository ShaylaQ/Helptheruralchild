using Helptheruralchild.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Helptheruralchild.Data;
using Helptheruralchild.Models;
using Microsoft.EntityFrameworkCore;  
using System.Linq;


public class PickupController : Controller
{
    private readonly ApplicationDbContext _context;

    public PickupController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Assigned()
    {
        var email = HttpContext.Session.GetString("UserEmail");
        if (email == null)
            return RedirectToAction("Login", "Account");

        var driver = _context.Users.FirstOrDefault(u => u.Email == email);
        if (driver == null)
            return RedirectToAction("Login", "Account");

        var assignedPickups = _context.Pickups
            .Include(p => p.Donation)
            .Where(p => p.DriverId == driver.Id)
            .ToList();

        return View(assignedPickups);
    }

    [HttpPost]
    public IActionResult UpdatePickup(int pickupId, string status)
    {
        var pickup = _context.Pickups.FirstOrDefault(p => p.Id == pickupId);
        if (pickup != null)
        {
            pickup.Status = status;
            _context.SaveChanges();
        }
        return RedirectToAction("Assigned");
    }
}
