using Microsoft.AspNetCore.Mvc;
using Helptheruralchild.Data;
using Helptheruralchild.Models;

namespace Helptheruralchild.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserEmail", user.Email);
            

            return user.Role switch
            {
                "Admin" => RedirectToAction("Index", "AdminDashboard"),
                "Driver" => RedirectToAction("Index", "DriverDashboard"),
                _ => RedirectToAction("Index", "DonorDashboard")
            };
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User model)
        {
            _context.Users.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
