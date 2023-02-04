using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI_MC_Core.Models.Country
{
    public class UpdateCountryDto : BaseCountryDto
    {
        [Required]
        public int CountryId { get; set; }
    }
}
