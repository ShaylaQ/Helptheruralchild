using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Helptheruralchild.Data;
using Helptheruralchild.Models;

namespace Helptheruralchild.Controllers
{
    public class DriverDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DriverDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            var driver = _context.Users.FirstOrDefault(u => u.Email == email);
            if (driver == null)
                return RedirectToAction("Login", "Account");

            var pickups = _context.Pickups
                .Include(p => p.Donation)   
                .Where(p => p.DriverId == driver.Id)
                .ToList();

            return View(pickups);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int pickupId, string status)
        {
            var pickup = _context.Pickups.FirstOrDefault(p => p.Id == pickupId);
            if (pickup != null)
            {
                pickup.Status = status;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

