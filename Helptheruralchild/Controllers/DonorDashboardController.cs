using Microsoft.AspNetCore.Mvc;
using Helptheruralchild.Data;
using Microsoft.EntityFrameworkCore;

namespace Helptheruralchild.Controllers
{
    public class DonorDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonorDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
                return RedirectToAction("Login", "Account");

            // Get donor user by email
            var donor = _context.Users.FirstOrDefault(u => u.Email == email);
            if (donor == null)
                return RedirectToAction("Login", "Account");

            // Compare int to int
            var donations = _context.Donations
                                    .Where(d => d.DonorId == donor.Id)
                                    .Include(d => d.Donor) // optional, if you want donor info
                                    .OrderByDescending(d => d.Timestamp)
                                    .ToList();

            return View(donations);
        }

        public IActionResult Track(int driverId)
        {
            return RedirectToAction("View", "Tracking", new { driverId });
        }
    }
}

