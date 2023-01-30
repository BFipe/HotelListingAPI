﻿using HotelListingAPI_MC.Models.Country;

namespace HotelListingAPI_MC.Models.Hotel
{
    public class HotelDto : BaseHotelDto
    {
        public int HotelEntityId { get; set; }

        public GetCountryDto Country { get; set; }
    }
}