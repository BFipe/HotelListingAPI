using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListingAPI_MC_Core.Contracts;
using HotelListingAPI_MC_Core.Models;
using HotelListingAPI_MC_Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI_MC_Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private protected readonly HotelListingDbContext _dbContext;
        private protected readonly IMapper _mapper;

        public GenericRepository(HotelListingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            var task = new Task(() =>
            {
                var removeEntity = entity;
                _dbContext.Set<T>().Remove(removeEntity);
            });
            task.Start();
            await task;
        }

        public virtual async Task<List<K>> GetAllAsync<K>()
        {
            return await _dbContext.Set<T>()
                .ProjectTo<K>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _dbContext
                .Set<T>()
                .FindAsync(id);
        }

        public async virtual Task<K> GetAsync<K>(int id)
        {
            var result = await _dbContext
                .Set<T>()
                .FindAsync(id);
            return _mapper.Map<K>(result);
        }

        public virtual async Task<bool> IsExists(int id)
        {
            return await GetAsync(id) is not null;
        }


        public virtual async Task UpdateAsync(T entity)
        {
            var task = new Task(() =>
            {
                var updateEntity = entity;
                _dbContext.Update(updateEntity);
            });
            task.Start();
            await task;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<PagedResult<K>> GetAllAsync<K>(QueryParameters queryParameters)
        {
            var totalSize = await _dbContext.Set<T>().CountAsync();

            var items = await _dbContext.Set<T>()
                .OrderBy(q => q)
                .Skip(queryParameters.PageNumber * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ProjectTo<K>(_mapper.ConfigurationProvider)
                .ToListAsync();


            return new PagedResult<K> 
            {
            Items = items,
            PageNumber = queryParameters.PageNumber,
            ItemsPerPage = queryParameters.PageSize,
            TotalCount = totalSize
            };
        }
    }
}
