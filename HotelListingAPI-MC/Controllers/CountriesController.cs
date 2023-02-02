using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI_DATA;
using HotelListingAPI_MC.Models.Country;
using AutoMapper;
using HotelListingAPI_MC.Contracts;
using HotelListingAPI_MC.Data.Entities.CountryEntities;
using Microsoft.AspNetCore.Authorization;

namespace HotelListingAPI_MC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountriesController(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }


        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var countries = await _countryRepository.GetAllAsync();
            var dtoCountries = _mapper.Map<IEnumerable<CountryDto>>(countries);
            return Ok(dtoCountries);
        }


        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountryEntity(int id)
        {
            var countryEntity = await _countryRepository.GetAsync(id);

            if (countryEntity == null)
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDto>(countryEntity);

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

            var countryEntity = await _countryRepository.GetAsync(id);
            if (countryEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDto, countryEntity);      

            try
            {
                await _countryRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryEntityExists(id))
                {
                    return NotFound();
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
            var country = _mapper.Map<CountryEntity>(createCountry);

            await _countryRepository.AddAsync(country);
            await _countryRepository.SaveChangesAsync();

            return CreatedAtAction("GetCountryEntity", new { id = country.CountryId }, createCountry);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> DeleteCountryEntity(int id)
        {
            var countryEntity = await _countryRepository.GetAsync(id);
            if (countryEntity == null)
            {
                return NotFound();
            }
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
