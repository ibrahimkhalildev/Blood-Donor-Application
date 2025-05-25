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
        public IActionResult Create(BloodDonor donor)
        {
            if(ModelState.IsValid)
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
                _context.BloodDonors.Add(donor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
