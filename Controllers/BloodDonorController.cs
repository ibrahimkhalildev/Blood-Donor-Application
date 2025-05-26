using BloodDonar.MVC.Data;
using BloodDonar.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonar.MVC.Controllers
{
    public class BloodDonorController : Controller
    {
        private readonly BloodDonorDbContext _context;
        public BloodDonorController(BloodDonorDbContext context)
        {
            _context = context;
        }
        //[Route("BloodDonor/[controller]")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BloodDonorCreateViewModel donor)
        {
            if (!ModelState.IsValid)
                return View(donor);
            var donorEntty = new BloodDonor
            {
                Name = donor.Name,
                ContactNumber = donor.ContactNumber,
                DateOFBirth = donor.DateOFBirth,
                Email = donor.Email,
                BloodGroup = donor.BloodGroup,
                Weight = donor.Weight,
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate
            };
            if (ModelState.IsValid)
            {
                //Check if the donor already exists in the database
                var existingDonor = _context.BloodDonors.FirstOrDefault(d => d.ContactNumber == donor.ContactNumber);
                if (existingDonor != null)
                {
                    // Donor already exists, return an error message or handle accordingly
                    ModelState.AddModelError("", "Donor with this contact number already exists.");
                    return View(donor);
                }
                //Add the new donor to the database
                _context.BloodDonors.Add(donorEntty);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
