using BloodDonar.MVC.Repusitories.Implementations;
using BloodDonar.MVC.Repusitories.Interfaces;

namespace BloodDonar.MVC.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBloodDonorRepository BloodDonorRepository { get; }
        IDonationRepository DonationRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
