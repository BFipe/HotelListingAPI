using HotelListingAPI_DATA.Entities;

namespace HotelListingAPI_MC.Contracts
{
    public interface IHotelRepository : IGenericRepository<HotelEntity>
    {
        public Task<bool> IsCountryExist(int id);
    }
}
