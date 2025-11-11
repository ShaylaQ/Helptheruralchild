using Microsoft.AspNetCore.Mvc;
using Helptheruralchild.Data;
using Helptheruralchild.Models;

namespace Helptheruralchild.Controllers
{
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Donation donation)
        {
            _context.Donations.Add(donation);
            _context.SaveChanges();
            return RedirectToAction("Index", "DonorDashboard");
        }
    }
}
