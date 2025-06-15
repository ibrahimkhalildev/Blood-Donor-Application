using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;
using BloodDonar.MVC.Services.Model;

namespace BloodDonar.MVC.Services.Interfaces
{
    public interface IBloodDonorService
    {
        Task<IEnumerable<BloodDonor>> GetAllAsync();
        Task<IList<BloodDonorListViewModel>> GetFilteredBloodDonorAsync(FilterDonorModel filter);
        Task<BloodDonor?> GetByIdAsync(int id);
        Task AddAsync(BloodDonor bloodDonor);
        Task UpdateAsync(BloodDonor bloodDonor);
        Task DeleteAsync(int id);
    }
}
