using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI_DATA;
using HotelListingAPI_MC.Contracts;
using AutoMapper;
using HotelListingAPI_MC.Models.Hotel;
using HotelListingAPI_MC.Data.Entities.HotelEntities;
using Microsoft.AspNetCore.Authorization;
using HotelListingAPI_MC.Exceptions;

namespace HotelListingAPI_MC.Controllers
{
    [Route("api/v{version:apiVersion}/Hotels")]
    [ApiController]
    [ApiVersion("1.1")]

    public class HotelsControllerV2 : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelsControllerV2(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync();

            var hotelsDto = _mapper.Map<List<GetHotelDto>>(hotels);

            return Ok(hotelsDto);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotelEntity(int id)
        {
            var hotelEntity = await _hotelRepository.GetAsync(id);

            if (hotelEntity == null)
            {
                throw new NotFoundException(nameof(GetHotelEntity), id);
            }

            var hotelDto = _mapper.Map<HotelDto>(hotelEntity);

            return Ok(hotelDto);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> PutHotelEntity(int id, UpdateHotelDto updateHotelDto)
        {
            if (id != updateHotelDto.HotelEntityId)
            {
                return BadRequest();
            }

            var hotelEntity = await _hotelRepository.GetAsync(id);

            if (hotelEntity == null)
            {
                throw new NotFoundException(nameof(PutHotelEntity), id);
            }

            _mapper.Map(updateHotelDto, hotelEntity);

            try
            {
                await _hotelRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelEntityExists(id))
                {
                    throw new NotFoundException(nameof(PutHotelEntity), id);
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
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult<HotelEntity>> PostHotelEntity(CreateHotelDto createHotelDto)
        {
            var hotelEntity = _mapper.Map<HotelEntity>(createHotelDto);

            if (await _hotelRepository.IsCountryExist(createHotelDto.CountryId) == false)
            {
                    throw new NotFoundException(nameof(PostHotelEntity), createHotelDto.CountryId);
            }

            await _hotelRepository.AddAsync(hotelEntity);
            await _hotelRepository.SaveChangesAsync();

            return CreatedAtAction("GetHotelEntity", new { id = hotelEntity.HotelEntityId }, createHotelDto);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> DeleteHotelEntity(int id)
        {
            var hotelEntity = await _hotelRepository.GetAsync(id);
            if (hotelEntity == null)
            {
                throw new NotFoundException(nameof(DeleteHotelEntity), id);
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
