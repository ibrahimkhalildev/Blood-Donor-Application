using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;
using System.Linq.Expressions;

namespace BloodDonar.MVC.Repusitories.Implementations
{
    public interface IBloodDonorRepository
    {
        Task<IEnumerable<BloodDonor>> GetAllAsync();
        Task<BloodDonor?> GetByIdAsync(int id);
        Task<IList<BloodDonorListViewModel>>FindAllAsync(Expression<Func<BloodDonor, bool>> predicate);
        void Add(BloodDonor bloodDonor);
        void Update(BloodDonor bloodDonor);
        void Delete(BloodDonor bloodDonor);
    }
}
