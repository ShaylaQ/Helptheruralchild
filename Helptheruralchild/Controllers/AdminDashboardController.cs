using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Helptheruralchild.Data;
using Helptheruralchild.Models;
using System.Linq;

namespace Helptheruralchild.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch all donations with donor information
            var donations = _context.Donations
                                    .Include(d => d.Donor) // Navigation property
                                    .OrderByDescending(d => d.Timestamp)
                                    .ToList();

            // Pass counts to ViewBag
            ViewBag.TotalDonations = donations.Count;
            ViewBag.TotalDrivers = _context.Users.Count(u => u.Role == "Driver");
            ViewBag.TotalDonors = _context.Users.Count(u => u.Role == "Donor");
            ViewBag.TotalPickups = _context.Pickups.Count();

            return View(donations); // Pass donations list as model
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
