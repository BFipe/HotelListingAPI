﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI_DATA;
using HotelListingAPI_DATA.Entities;

namespace HotelListingAPI_MC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListingDbContext _context;

        public CountriesController(HotelListingDbContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryEntity>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryEntity>> GetCountryEntity(int id)
        {
            var countryEntity = await _context.Countries.FindAsync(id);

            if (countryEntity == null)
            {
                return NotFound();
            }

            return countryEntity;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryEntity(int id, CountryEntity countryEntity)
        {
            if (id != countryEntity.CountryId)
            {
                return BadRequest();
            }

            _context.Entry(countryEntity).State = EntityState.Modified;

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
        public async Task<ActionResult<CountryEntity>> PostCountryEntity(CountryEntity countryEntity)
        {
            _context.Countries.Add(countryEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryEntity", new { id = countryEntity.CountryId }, countryEntity);
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
