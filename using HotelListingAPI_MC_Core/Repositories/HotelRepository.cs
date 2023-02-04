using AutoMapper;
using HotelListingAPI_MC_Core.Contracts;
using HotelListingAPI_MC_Core.Models.Hotel;
using HotelListingAPI_MC_Data;
using HotelListingAPI_MC_Data.Entities.HotelEntities;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI_MC_Core.Repositories
{
    public class HotelRepository : GenericRepository<HotelEntity>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }

        public async Task<HotelDto> GetDtoAsync(int id)
        {
            return _mapper.Map<HotelDto>(await _dbContext.Hotels.Include(q => q.Country).SingleOrDefaultAsync(q => q.HotelEntityId == id));
        }

        public async Task<bool> IsCountryExist(int id)
        {
            return await _dbContext.Countries.SingleOrDefaultAsync(q => q.CountryId == id) is not null;
        }
    }
}
