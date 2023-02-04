using HotelListingAPI_MC_Core.Models.Country;
using HotelListingAPI_MC_Data.Entities.CountryEntities;

namespace HotelListingAPI_MC_Core.Contracts
{
    public interface ICountryRepository : IGenericRepository<CountryEntity>
    {
        public Task<CountryEntity> AddDtoAsync(CreateCountryDto dtoCountry);
        public Task PutDtoCountryAsync(int id, UpdateCountryDto dtoCountry);
        public Task<CountryDto> GetDtoAsync(int id);
    }
}
