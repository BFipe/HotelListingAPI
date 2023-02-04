using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI_MC_Data.Entities.UserEntities
{
    public class APIUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
