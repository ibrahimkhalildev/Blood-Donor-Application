using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;
using BloodDonar.MVC.Repusitories.Implementations;
using BloodDonar.MVC.Services.Interfaces;
using BloodDonar.MVC.Services.Model;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BloodDonar.MVC.Services.Implementations
{
    public class BloodDonorService : IBloodDonorService
    {
        private readonly IBloodDonorRepository _bloodDonorRepository;
        public BloodDonorService(IBloodDonorRepository bloodDonorRepository)
        {
            _bloodDonorRepository = bloodDonorRepository;
        }
        public Task AddAsync(BloodDonor bloodDonor)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BloodDonor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BloodDonor?> GetByIdAsync(int id)
        {
            return await _bloodDonorRepository.GetByIdAsync(id);
        }

        public async Task<IList<BloodDonorListViewModel>> GetFilteredBloodDonorAsync(FilterDonorModel filter)
        {
            var query = await _bloodDonorRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter.bloodGroup))
                query = query.Where(d => d.bloodGroup.ToString() == filter.bloodGroup);
            if (!string.IsNullOrEmpty(filter.address))
                query = query.Where(d => d.Address != null && d.Address.Contains(filter.address));

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

            if (filter.isEligible.HasValue)
            {
                donors = donors.Where(X => X.isEligible == filter.isEligible).ToList();
            }
            return donors;
        }

        public Task UpdateAsync(BloodDonor bloodDonor)
        {
            throw new NotImplementedException();
        }
    }
}
