using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Repusitories.Interfaces;

namespace BloodDonar.MVC.Data.UnitOfWork
{
    internal class DonationRepository : IDonationRepository
    {
        private ApplicationDbContext context;

        public DonationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Donation bloodDonor)
        {
            throw new NotImplementedException();
        }

        public void Delete(Donation bloodDonor)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Donation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Donation?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Donation bloodDonor)
        {
            throw new NotImplementedException();
        }
    }
}