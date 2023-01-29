using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI_DATA;
using HotelListingAPI_DATA.Entities;
using HotelListingAPI_MC.Models.Country;
using AutoMapper;

namespace HotelListingAPI_MC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public CountriesController(HotelListingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _context.Countries.ToListAsync();
            var dtoCountries = _mapper.Map<IEnumerable<GetCountryDto>>(countries);
            return Ok(dtoCountries);
        }


        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountryEntity(int id)
        {
            var countryEntity = await _context.Countries
                .Include(q => q.Hotels)
                .SingleOrDefaultAsync(q => q.CountryId == id);

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
        public async Task<IActionResult> PutCountryEntity(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.CountryId)
            {
                return BadRequest();
            }

            var countryEntity = await _context.Countries.FindAsync(id);
            if (countryEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDto, countryEntity);      

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryEntityExists(id))
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
        public async Task<ActionResult<CreateCountryDto>> PostCountryEntity(CreateCountryDto createCountry)
        {
            var country = _mapper.Map<CountryEntity>(createCountry);

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryEntity", new { id = country.CountryId }, createCountry);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryEntity(int id)
        {
            var countryEntity = await _context.Countries.FindAsync(id);
            if (countryEntity == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(countryEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryEntityExists(int id)
        {
            return _context.Countries.Any(e => e.CountryId == id);
        }
    }
}
