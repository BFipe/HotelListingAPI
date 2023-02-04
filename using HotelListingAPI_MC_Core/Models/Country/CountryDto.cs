using HotelListingAPI_MC_Core.Models.Hotel;

namespace HotelListingAPI_MC_Core.Models.Country
{
    public class CountryDto : BaseCountryDto
    {
        public int CountryId { get; set; }

        public List<GetHotelDto> Hotels { get; set; }
    }
}
