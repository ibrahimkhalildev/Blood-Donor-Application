using BloodDonar.MVC.Repusitories.Implementations;
using BloodDonar.MVC.Repusitories.Interfaces;

namespace BloodDonar.MVC.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;

        public IBloodDonorRepository BloodDonorRepository => throw new NotImplementedException();

        public IDonationRepository DonationRepository => throw new NotImplementedException();

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
