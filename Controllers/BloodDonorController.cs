using BloodDonar.MVC.Data;
using BloodDonar.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        public IActionResult Index(string bloodGroup, string address, bool? isEligible)
        {
            IQueryable<BloodDonor> query = _context.BloodDonors;

            if (!string.IsNullOrEmpty(bloodGroup))
                query = query.Where(d => d.bloodGroup.ToString() == bloodGroup);
            if (!string.IsNullOrEmpty(address))
                query = query.Where(d => d.Address != null && d.Address.Contains(address));

            var donors = query.Select(d => new BloodDonorListViewModel
            {
                Id = d.Id,
                Name = d.Name,
                ContactNumber = d.ContactNumber,
                Age = DateTime.Now.Year - d.DateOFBirth.Year, // Calculate age from DateOfBirth
                Email = d.Email,
                BloodGroup = d.BloodGroup.ToString(),
                Address = d.Address,
                LastDonationDate = d.LastDonationDate.HasValue ? $"{(DateTime.Today - d.LastDonationDate.Value).Days} Days ago" : "Never",
                Weight = d.Weight,
                ProfilePicture = d.ProfilePicture,
                isEligible = (d.Weight > 45 && d.Weight < 200) && (!d.LastDonationDate.HasValue || (DateTime.Now - d.LastDonationDate.Value).TotalDays >= 90)
            }).ToList();

            if (isEligible.HasValue)
            {
                donors = donors.Where(X => X.isEligible == isEligible).ToList();
            }

            return View(donors);
        }
        //public IActionResult Index(string bloodGroup, string address)
        //{
        //    IQueryable<BloodDonor> query = _context.BloodDonors;

        //    if (!string.IsNullOrEmpty(bloodGroup) && Enum.TryParse<BloodGroup>(bloodGroup, out var parsedGroup))
        //    {
        //        query = query.Where(d => d.BloodGroup == parsedGroup);
        //    }

        //    if (!string.IsNullOrEmpty(address))
        //    {
        //        query = query.Where(d => EF.Functions.Like(d.Address, $"%{address}%"));
        //    }

        //    var donors = query.ToList();
        //    return View(donors);
        //}

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
                donorEntity.ProfilePicture = Path.Combine("profiles", fileName); // Set the path to the saved image
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
        public IActionResult Details(int id)
        {
            var donor = _context.BloodDonors.Include(d => d.Donation).FirstOrDefault(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorViewModel = new BloodDonorListViewModel
            {
                Id = donor.Id,
                Name = donor.Name,
                ContactNumber = donor.ContactNumber,
                Age = DateTime.Now.Year - donor.DateOFBirth.Year, // Calculate age from DateOfBirth
                Email = donor.Email,
                BloodGroup = donor.BloodGroup.ToString(),
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate.HasValue ? $"{(DateTime.Today - donor.LastDonationDate.Value).Days} Days ago" : "Never",
                Weight = donor.Weight,
                ProfilePicture = donor.ProfilePicture,
                isEligible = (donor.Weight > 45 && donor.Weight < 200) && (!donor.LastDonationDate.HasValue || (DateTime.Now - donor.LastDonationDate.Value).TotalDays >= 90)
            };
            return View(donorViewModel);
        }
        public IActionResult Edit(int id)
        {
            var donor = _context.BloodDonors.FirstOrDefault(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorEditModel = new BloodDonorEditViewModel
            {
                Id = donor.Id,
                Name = donor.Name,
                ContactNumber = donor.ContactNumber,
                DateOFBirth = donor.DateOFBirth,
                Email = donor.Email,
                BloodGroup = donor.BloodGroup,
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate,
                ExistingProfilePicture = donor.ProfilePicture,
            };
            return View(donorEditModel);
        }
        [HttpPost]
        public async Task<IActionResult > Edit(BloodDonorEditViewModel donor)
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
                    Directory.CreateDirectory(folderPath);
                }
                var fullPath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await donor.ProfilePicture.CopyToAsync(stream);
                }
                donorEntity.ProfilePicture = Path.Combine("profiles", fileName);
            }
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
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var donor = _context.BloodDonors.Include(d => d.Donation).FirstOrDefault(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorViewModel = new BloodDonorListViewModel
            {
                Id = donor.Id,
                Name = donor.Name,
                ContactNumber = donor.ContactNumber,
                Age = DateTime.Now.Year - donor.DateOFBirth.Year, // Calculate age from DateOfBirth
                Email = donor.Email,
                BloodGroup = donor.BloodGroup.ToString(),
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate.HasValue ? $"{(DateTime.Today - donor.LastDonationDate.Value).Days} Days ago" : "Never",
                Weight = donor.Weight,
                ProfilePicture = donor.ProfilePicture,
                isEligible = (donor.Weight > 45 && donor.Weight < 200) && (!donor.LastDonationDate.HasValue || (DateTime.Now - donor.LastDonationDate.Value).TotalDays >= 90)
            };
            return View(donorViewModel);
        }
        [HttpDelete]
        [ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var donor = _context.BloodDonors.Include(d => d.Donation).FirstOrDefault(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            _context.BloodDonors.Remove(donor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
