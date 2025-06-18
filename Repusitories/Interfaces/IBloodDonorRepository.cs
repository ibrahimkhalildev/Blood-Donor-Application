using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;
using BloodDonar.MVC.Repusitories.Interfaces;
using System.Linq.Expressions;

namespace BloodDonar.MVC.Repusitories.Implementations
{
    public interface IBloodDonorRepository : IRepository<BloodDonorEntity>
    {
    }
}
