using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI_MC_Core.Models.Country;
using AutoMapper;
using HotelListingAPI_MC_Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListingAPI_MC_Core.Exceptions;
using HotelListingAPI_MC_Core.Models;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListingAPI_MC.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountriesController(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }


        // GET: api/Countries/GetAll
        [HttpGet("GetAll")]
        [EnableQuery]

        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countryRepository.GetAllAsync<GetCountryDto>();
            var dtoCountries = _mapper.Map<IEnumerable<GetCountryDto>>(countries);
            return Ok(dtoCountries);
        }

        // GET: api/Countries/?StartIndex=0&PageSize=25&PageNumber=1
        [HttpGet]
        [EnableQuery]

        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetCountries([FromQuery] QueryParameters parameters)
        {
            var pagedCountries = await _countryRepository.GetAllAsync<GetCountryDto>(parameters);
            return Ok(pagedCountries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountryEntity(int id)
        {
            var countryDto = await _countryRepository.GetDtoAsync(id);

            if (countryDto == null)
            {
                throw new NotFoundException(nameof(GetCountryEntity), id);
            }

            return Ok(countryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles ="Manager, Admin")]
        public async Task<IActionResult> PutCountryEntity(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.CountryId)
            {
                return BadRequest();
            }

            await _countryRepository.PutDtoCountryAsync(id, updateCountryDto);   

            try
            {
                await _countryRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryEntityExists(id))
                {
                    throw new NotFoundException(nameof(PutCountryEntity), id);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult<CreateCountryDto>> PostCountryEntity(CreateCountryDto createCountry)
        {
            await _countryRepository.AddDtoAsync(createCountry);
            await _countryRepository.SaveChangesAsync();

            return Ok(createCountry);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> DeleteCountryEntity(int id)
        {
            await _countryRepository.DeleteAsync(id);
            await _countryRepository.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> CountryEntityExists(int id)
        {
            return await _countryRepository.IsExists(id);
        }
    }
}
