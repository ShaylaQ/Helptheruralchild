using Microsoft.AspNetCore.Mvc;
using Helptheruralchild.Data;
using System.Linq;

namespace Helptheruralchild.Controllers
{
    public class TrackingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TrackingController(ApplicationDbContext context) => _context = context;

        public IActionResult View(int driverId)
        {
            var points = _context.TrackingPoints
                .Where(t => t.DriverId == driverId)
                .OrderByDescending(t => t.Timestamp)
                .ToList();

            return View(points);
        }
    }
}
