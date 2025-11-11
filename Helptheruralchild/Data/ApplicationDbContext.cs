using Helptheruralchild.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Helptheruralchild.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Pickup> Pickups { get; set; }
        public DbSet<TrackingPoint> TrackingPoints { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
