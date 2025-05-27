using BloodDonar.MVC.Data;
using BloodDonar.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonar.MVC.Controllers
{
    public class BloodDonorController : Controller
    {
        private readonly BloodDonorDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BloodDonorController(BloodDonorDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //[Route("BloodDonor/[controller]")]
        public IActionResult Index()
        {
            var donars = _context.BloodDonors.ToList();
            return View(donars);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(BloodDonorCreateViewModel donor)
        {
            if (!ModelState.IsValid)
                return View(donor);
            var donorEntity = new BloodDonor
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
            if (donor.ProfilePicture != null && donor.ProfilePicture.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(donor.ProfilePicture.FileName)}";
                var folderPath = Path.Combine(_env.WebRootPath, "profiles");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath); // Ensure the directory exists
                }
                var fullPath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await donor.ProfilePicture.CopyToAsync(stream);
                }
                donorEntity.ProfilePicture = $"/images/{fileName}"; // Set the path to the saved image
            }
            //if (donor.ProfilePicture != null && donor.ProfilePicture.Length > 0)
            //{
            //    // Save the profile picture to a specific location and set the path
            //    var fileName = Path.GetFileName(donor.ProfilePicture.FileName);
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        donor.ProfilePicture.CopyTo(stream);
            //    }
            //    donorEntty.ProfilePicture = $"/images/{fileName}"; // Set the path to the saved image
            //}
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
                _context.BloodDonors.Add(donorEntity);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
