using HotelListingAPI_MC_Core.Models.Country;

namespace HotelListingAPI_MC_Core.Models.Hotel
{
    public class GetHotelDto : BaseHotelDto
    {
        public int HotelEntityId { get; set; }
    }
}