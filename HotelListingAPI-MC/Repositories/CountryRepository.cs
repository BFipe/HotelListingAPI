using HotelListingAPI_DATA;
using HotelListingAPI_DATA.Entities;
using HotelListingAPI_MC.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI_MC.Repositories
{
    public class CountryRepository : GenericRepository<CountryEntity>, ICountryRepository
    {
        public CountryRepository(HotelListingDbContext dbContext) : base(dbContext)
        {
            
        }

        public override Task<CountryEntity> GetAsync(int id)
        {
            return _dbContext.Countries.Include(q => q.Hotels).SingleOrDefaultAsync(q => q.CountryId == id);
        }
    }
}
