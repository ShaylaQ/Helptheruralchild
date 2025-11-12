using Microsoft.AspNetCore.Mvc;
using Helptheruralchild.Data;
using Helptheruralchild.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Donation donation)
        {
            if (ModelState.IsValid)
            {
                
                var email = HttpContext.Session.GetString("UserEmail");
                if (email == null)
                    return RedirectToAction("Login", "Account");

                var donor = _context.Users.FirstOrDefault(u => u.Email == email);
                if (donor == null)
                    return RedirectToAction("Login", "Account");

               
                donation.DonorId = donor.Id;
                donation.Timestamp = DateTime.Now;
                donation.Status = "Pending";

                _context.Donations.Add(donation);
                _context.SaveChanges();

                return RedirectToAction("Index", "DonorDashboard");
            }

            return View(donation);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var donations = _context.Donations
                                    .OrderByDescending(d => d.Timestamp)
                                    .ToList();
            return View(donations);
        }
    }
}


