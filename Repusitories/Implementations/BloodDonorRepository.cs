using BloodDonar.MVC.Data;
using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Models.ViewModel;
using BloodDonar.MVC.Repusitories.Interfaces;
using BloodDonar.MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodDonar.MVC.Repusitories.Implementations
{
    public class BloodDonorRepository : Repository<BloodDonor>, IBloodDonorRepository
    {
        public BloodDonorRepository(BloodDonorDbContext context) : base(context)
        {
        }
    }
}
