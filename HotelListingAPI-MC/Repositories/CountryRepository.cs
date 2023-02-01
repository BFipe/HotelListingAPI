using HotelListingAPI_DATA;
using HotelListingAPI_MC.Contracts;
using HotelListingAPI_MC.Data.Entities.CountryEntities;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI_MC.Repositories
{
    public class CountryRepository : GenericRepository<CountryEntity>, ICountryRepository
    {
        public CountryRepository(HotelListingDbContext dbContext) : base(dbContext)
        {
            
        }

        public override async Task<CountryEntity> GetAsync(int id)
        {
            return await _dbContext.Countries.Include(q => q.Hotels).SingleOrDefaultAsync(q => q.CountryId == id);
        }
    }
}
