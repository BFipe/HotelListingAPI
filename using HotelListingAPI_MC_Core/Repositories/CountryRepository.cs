using AutoMapper;
using HotelListingAPI_MC_Core.Contracts;
using HotelListingAPI_MC_Core.Exceptions;
using HotelListingAPI_MC_Core.Models.Country;
using HotelListingAPI_MC_Data;
using HotelListingAPI_MC_Data.Entities.CountryEntities;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI_MC_Core.Repositories
{
    public class CountryRepository : GenericRepository<CountryEntity>, ICountryRepository
    {
        public CountryRepository(HotelListingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public async Task<CountryDto> GetDtoAsync(int id)
        {
            return _mapper.Map<CountryDto>(await _dbContext.Countries.Include(q => q.Hotels).SingleOrDefaultAsync(q => q.CountryId == id));
        }

        public Task<CountryEntity> AddDtoAsync(CreateCountryDto dtoCountry)
        {
            var country = _mapper.Map<CountryEntity>(dtoCountry);
            return base.AddAsync(country);
        }

        public async Task PutDtoCountryAsync(int id, UpdateCountryDto dtoCountry)
        {
            var countryEntity = await GetAsync(id);
            if (countryEntity == null)
            {
                throw new NotFoundException(nameof(PutDtoCountryAsync), id);
            }

            _mapper.Map(dtoCountry, countryEntity);
        }
    }
}
