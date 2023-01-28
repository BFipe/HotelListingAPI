using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI_MC.Models.Country
{
    public class CreateCountryDto
    {
        [Required]
        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}
