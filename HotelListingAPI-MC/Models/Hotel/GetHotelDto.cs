using HotelListingAPI_MC.Models.Country;

namespace HotelListingAPI_MC.Models.Hotel
{
    public class GetHotelDto : BaseHotelDto
    {
        public int HotelEntityId { get; set; }
    }
}