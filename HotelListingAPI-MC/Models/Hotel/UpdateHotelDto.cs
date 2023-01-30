using Microsoft.Build.Framework;

namespace HotelListingAPI_MC.Models.Hotel
{
    public class UpdateHotelDto : BaseHotelDto
    {
        [Required]
        public int HotelEntityId { get; set; }
    }
}