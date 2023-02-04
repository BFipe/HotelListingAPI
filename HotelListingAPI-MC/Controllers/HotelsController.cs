using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI_MC_Core.Contracts;
using AutoMapper;
using HotelListingAPI_MC_Core.Models.Hotel;
using HotelListingAPI_MC_Data.Entities.HotelEntities;
using Microsoft.AspNetCore.Authorization;
using HotelListingAPI_MC_Core.Exceptions;
using HotelListingAPI_MC_Core.Models;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListingAPI_MC.Controllers
{
    [Route("api/v{version:apiVersion}/Hotels")]
    [ApiController]
    [ApiVersion("1.0")]

    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // GET: api/Hotels/GetAll
        [HttpGet("GetAll")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync<GetHotelDto>();
            return Ok(hotels);
        }

        // GET: api/Hotels/?StartIndex=0&PageSize=25&PageNumber=1
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<PagedResult<GetHotelDto>>> GetHotels([FromQuery]QueryParameters parameters)
        {
            var hotels = await _hotelRepository.GetAllAsync<GetHotelDto>(parameters);
            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotelEntity(int id)
        {
            var hotelEntity = await _hotelRepository.GetAsync<HotelDto>(id);

            if (hotelEntity == null)
            {
                throw new NotFoundException(nameof(GetHotelEntity), id);
            }

            return Ok(hotelEntity);
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
