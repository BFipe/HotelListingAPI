using HotelListingAPI_MC_Core.Models;

namespace HotelListingAPI_MC_Core.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<K> GetAsync<K>(int id);

        Task<List<K>> GetAllAsync<K>();

        Task<PagedResult<K>> GetAllAsync<K>(QueryParameters queryParameters);
        
        Task<T> AddAsync(T entity);
        
        Task DeleteAsync(int id);
        
        Task UpdateAsync(T entity);
        
        Task<bool> IsExists(int id);
        
        Task SaveChangesAsync();
    }
}
