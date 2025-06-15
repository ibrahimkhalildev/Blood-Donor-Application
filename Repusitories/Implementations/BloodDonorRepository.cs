using BloodDonar.MVC.Data;
using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;
using BloodDonar.MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodDonar.MVC.Repusitories.Implementations
{
    public class BloodDonorRepository : IBloodDonorRepository
    {
        private readonly BloodDonorDbContext _context;
        public BloodDonorRepository(BloodDonorDbContext context)
        {
            _context = context;
        }

        public void Add(BloodDonor bloodDonor)
        {
            _context.BloodDonors.Add(bloodDonor);
        }
        public void Update(BloodDonor bloodDonor)
        {
            _context.BloodDonors.Update(bloodDonor);
        }
        public void Delete(BloodDonor bloodDonor)
        {
            _context.BloodDonors.Remove(bloodDonor);
        }

        public Task<IList<BloodDonorListViewModel>> FindAllAsync(Expression<Func<BloodDonor, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BloodDonor>> GetAllAsync()
        {
           return await _context.BloodDonors.ToListAsync();
        }

        public async Task<BloodDonor?> GetByIdAsync(int id)
        {
            return await _context.BloodDonors.FindAsync(id);
        }
    }
}
