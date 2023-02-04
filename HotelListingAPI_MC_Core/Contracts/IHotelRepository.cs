using HotelListingAPI_MC_Core.Models.Hotel;
using HotelListingAPI_MC_Data.Entities.HotelEntities;

namespace HotelListingAPI_MC_Core.Contracts
{
    public interface IHotelRepository : IGenericRepository<HotelEntity>
    {
        public Task<bool> IsCountryExist(int id);
        public Task<HotelDto> GetDtoAsync(int id);
    }
}
