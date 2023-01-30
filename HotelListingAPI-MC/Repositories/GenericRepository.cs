using HotelListingAPI_DATA;
using HotelListingAPI_MC.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI_MC.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private protected readonly HotelListingDbContext _dbContext;

        public GenericRepository(HotelListingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            var task = new Task(() => _dbContext.Set<T>().Remove(entity));
            task.Start();
            await task;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<bool> IsExists(int id)
        {
            return await GetAsync(id) is not null;
        }


        public virtual async Task UpdateAsync(T entity)
        {
            var task = new Task(() => _dbContext.Update(entity));
            task.Start();
            await task;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
