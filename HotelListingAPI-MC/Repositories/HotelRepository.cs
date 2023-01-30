using HotelListingAPI_DATA;
using HotelListingAPI_DATA.Entities;
using HotelListingAPI_MC.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI_MC.Repositories
{
    public class HotelRepository : GenericRepository<HotelEntity>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext dbContext) : base(dbContext)
        {
            
        }

        public override async Task<HotelEntity> GetAsync(int id)
        {
            return await _dbContext.Hotels.Include(q => q.Country).SingleOrDefaultAsync(q => q.HotelEntityId == id);
        }

        public async Task<bool> IsCountryExist(int id)
        {
            return await _dbContext.Countries.SingleOrDefaultAsync(q => q.CountryId == id) is not null;
        }
    }
}
