using HotelListingAPI_MC.Models.Hotel;

namespace HotelListingAPI_MC.Models.Country
{
    public class CountryDto : BaseCountryDto
    {
        public int CountryId { get; set; }

        public List<HotelDto> Hotels { get; set; }
    }
}
