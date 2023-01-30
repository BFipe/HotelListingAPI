using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI_DATA;
using HotelListingAPI_DATA.Entities;
using HotelListingAPI_MC.Contracts;

namespace HotelListingAPI_MC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelsController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelEntity>>> GetHotels()
        {
            return await _hotelRepository.GetAllAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelEntity>> GetHotelEntity(int id)
        {
            var hotelEntity = await _hotelRepository.GetAsync(id);

            if (hotelEntity == null)
            {
                return NotFound();
            }

            return hotelEntity;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelEntity(int id, HotelEntity hotelEntity)
        {
            if (id != hotelEntity.HotelEntityId)
            {
                return BadRequest();
            }

            

            try
            {
                await _hotelRepository.UpdateAsync(hotelEntity);
                await _hotelRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelEntityExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelEntity>> PostHotelEntity(HotelEntity hotelEntity)
        {
            await _hotelRepository.AddAsync(hotelEntity);
            await _hotelRepository.SaveChangesAsync();

            return CreatedAtAction("GetHotelEntity", new { id = hotelEntity.HotelEntityId }, hotelEntity);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelEntity(int id)
        {
            var hotelEntity = await _hotelRepository.GetAsync(id);
            if (hotelEntity == null)
            {
                return NotFound();
            }

            await _hotelRepository.DeleteAsync(id);
            await _hotelRepository.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> HotelEntityExists(int id)
        {
            return await _hotelRepository.IsExists(id);
        }
    }
}
