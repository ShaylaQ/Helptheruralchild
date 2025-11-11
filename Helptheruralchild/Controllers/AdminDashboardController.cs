using Microsoft.AspNetCore.Mvc;
using Helptheruralchild.Data;

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
            ViewBag.TotalDonations = _context.Donations.Count();
            ViewBag.TotalDrivers = _context.Users.Count(u => u.Role == "Driver");
            ViewBag.TotalDonors = _context.Users.Count(u => u.Role == "Donor");
            ViewBag.TotalPickups = _context.Pickups.Count();
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
