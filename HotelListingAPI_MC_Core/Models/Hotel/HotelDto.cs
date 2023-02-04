using HotelListingAPI_MC_Core.Models.Country;

namespace HotelListingAPI_MC_Core.Models.Hotel
{
    public class HotelDto : BaseHotelDto
    {
        public int HotelEntityId { get; set; }

        public GetCountryDto Country { get; set; }
    }
}