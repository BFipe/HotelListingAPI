using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI_MC_Core.Models.User
{
    public class RegisterUserDto : LoginUserDto
    {
        [Required]
        [StringLength(120, ErrorMessage = "Minimal first name length - 1, maximum - 120", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "Minimal last name length - 1, maximum - 120", MinimumLength = 1)]
        public string LastName { get; set; }
    }
} 