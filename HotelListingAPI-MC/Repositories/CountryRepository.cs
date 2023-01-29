using HotelListingAPI_DATA;
using HotelListingAPI_DATA.Entities;
using HotelListingAPI_MC.Contracts;

namespace HotelListingAPI_MC.Repositories
{
    public class CountryRepository : GenericRepository<CountryEntity>, ICountryRepository
    {
        public CountryRepository(HotelListingDbContext dbContext) : base(dbContext)
        {
        }
    }
}
