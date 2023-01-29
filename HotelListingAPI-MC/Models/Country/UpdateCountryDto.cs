using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI_MC.Models.Country
{
    public class UpdateCountryDto : BaseCountryDto
    {
        [Required]
        public int CountryId { get; set; }
    }
}
