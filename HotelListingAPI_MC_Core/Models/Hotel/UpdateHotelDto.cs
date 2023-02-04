using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI_MC_Core.Models.Hotel
{
    public class UpdateHotelDto : BaseHotelDto
    {
        [Required]
        public int HotelEntityId { get; set; }
    }
}