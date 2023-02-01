using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI_MC.Models.User
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Incorrect Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "Minimal password length - 6, maximum - 120", MinimumLength = 6)]
        public string Password { get; set; }
    }
}