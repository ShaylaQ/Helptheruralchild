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
            var donations = _context.Donations
                .Include(d => d.Donor)
                .OrderByDescending(d => d.Timestamp)
                .ToList();

            ViewBag.TotalDonations = donations.Count;
            ViewBag.TotalDrivers = _context.Users.Count(u => u.Role == "Driver");
            ViewBag.TotalDonors = _context.Users.Count(u => u.Role == "Donor");
            ViewBag.TotalPickups = _context.Pickups.Count();

            return View(donations);
        }

        
        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var donation = _context.Donations.FirstOrDefault(d => d.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            donation.Status = status;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
