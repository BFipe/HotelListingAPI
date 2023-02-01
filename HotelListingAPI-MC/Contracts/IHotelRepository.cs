using HotelListingAPI_MC.Data.Entities.HotelEntities;

namespace HotelListingAPI_MC.Contracts
{
    public interface IHotelRepository : IGenericRepository<HotelEntity>
    {
        public Task<bool> IsCountryExist(int id);
    }
}
