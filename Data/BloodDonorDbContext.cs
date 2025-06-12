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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BloodDonor>()
                .HasData(new BloodDonor
                {
                    Id = 1,
                    Name = "Mahbubur Rahman",
                    ContactNumber = "01932878112",
                    DateOFBirth = new DateTime(1990, 1, 1),
                    Email = "mahbuburrahman@example.com",
                    BloodGroup = BloodGroup.OPositive, // Fix: Set the required 'BloodGroup' property  
                    Weight = 70.5f, // Optional: Add other properties if needed  
                    Address = "123 Main St, Dhaka", // Optional: Add address if needed
                    LastDonationDate = new DateTime(2024, 11, 1), // Optional: Add last donation date if needed
                    ProfilePicture = "profiles/mahbubur.jpg", // Optional: Add profile picture if needed
                },
                new BloodDonor
                {
                    Id = 2,
                    Name = "Ibrahim Khalil",
                    ContactNumber = "01582878199",
                    DateOFBirth = new DateTime(1990, 1, 1),
                    Email = "IbrahimKhalil@example.com",
                    BloodGroup = BloodGroup.OPositive, // Fix: Set the required 'BloodGroup' property  
                    Weight = 70.5f,  // Optional: Add other properties if needed
                    Address = "9/A West Paikpara, Dhaka", // Optional: Add address if needed
                    LastDonationDate = new DateTime(2023, 10, 1), // Optional: Add last donation date if needed
                    ProfilePicture = "profiles/ibrahim.jpg", // Optional: Add profile picture if needed
                });
        }
    }
}
