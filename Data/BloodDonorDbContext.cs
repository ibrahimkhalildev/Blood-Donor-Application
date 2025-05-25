using BloodDonar.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonar.MVC.Data
{
    public class BloodDonorDbContext : DbContext
    {
        public BloodDonorDbContext(DbContextOptions<BloodDonorDbContext> options) : base(options)
        {

        }
        public DbSet<BloodDonor> BloodDonors { get; set; }
        public DbSet<Donation> Donations { get; set; }
    }
}
