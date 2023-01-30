using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI_MC.Models.Hotel
{
    public abstract class BaseHotelDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Range(0d, 10d)]
        public double? Rating { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }
    }
}
