using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;
using System.Linq.Expressions;

namespace BloodDonar.MVC.Repusitories.Interfaces
{
    public interface IDonationRepository
    {
        Task<IEnumerable<Donation>> GetAllAsync();
        Task<Donation?> GetByIdAsync(int id);
        void Add(Donation bloodDonor);
        void Update(Donation bloodDonor);
        void Delete(Donation bloodDonor);
    }
}
