using AutoMapper;
using HotelListingAPI_DATA;
using HotelListingAPI_MC.Contracts;
using HotelListingAPI_MC.Data.Entities.HotelEntities;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI_MC.Repositories
{
    public class HotelRepository : GenericRepository<HotelEntity>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
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
