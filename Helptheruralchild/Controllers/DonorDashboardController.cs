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

            var donor = _context.Users.FirstOrDefault(u => u.Email == email);
            var donations = _context.Donations.Where(d => d.DonorId == donor!.Id.ToString()).ToList();

            return View(donations);
        }

        public IActionResult Track(int driverId)
        {
            return RedirectToAction("View", "Tracking", new { driverId });
        }
    }
}
