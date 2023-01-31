﻿using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI_MC.Data.Entities
{
    public class APIUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
