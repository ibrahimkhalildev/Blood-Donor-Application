using BloodDonar.MVC.Data;
using BloodDonar.MVC.Models.Entities;
using BloodDonar.MVC.Repusitories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonar.MVC.Repusitories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;

        public Repository(BloodDonorDbContext context)
        {
           _dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return  await _dbSet.FindAsync(id);
        }

        public IQueryable<T> Query()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        Task<T?> IRepository<T>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
