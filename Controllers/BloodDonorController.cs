using BloodDonar.MVC.Data;
using BloodDonar.MVC.Models;
using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;
using BloodDonar.MVC.Services.Implementations;
using BloodDonar.MVC.Services.Interfaces;
using BloodDonar.MVC.Services.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace BloodDonar.MVC.Controllers
{
    public class BloodDonorController : Controller
    {
        private readonly BloodDonorDbContext _context;
        private readonly IFileService _fileService;
        private readonly IBloodDonorService _bloodDonorService;
        public BloodDonorController(BloodDonorDbContext context, IFileService fileService, IBloodDonorService bloodDonorService)
        {
            _context = context;
            _fileService = fileService;
            _bloodDonorService = bloodDonorService;

        }
        //[Route("BloodDonor/[controller]")]
        public async Task<IActionResult> Index(string bloodGroup, string address, bool? isEligible)
        {
            var filter = new FilterDonorModel { bloodGroup = bloodGroup, address = address, isEligible = isEligible };
            var donors = await _bloodDonorService.GetFilteredBloodDonorAsync(filter);

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
                LastDonationDate = donor.LastDonationDate,
                ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture) // Use the file service to save the profile picture
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
                _context.BloodDonors.Add(donorEntity);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DetailsAsync(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
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
                LastDonationDate = donor.LastDonationDate,
                ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture)
            };
            _context.BloodDonors.Add(donorEntity);
            _context.SaveChanges();
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
