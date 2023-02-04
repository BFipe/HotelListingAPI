using HotelListingAPI_MC_Data.Entities.CountryEntities;

namespace HotelListingAPI_MC_Data.Entities.HotelEntities
{
    public class HotelEntity
    {
        public int HotelEntityId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double? Rating { get; set; }


        public int CountryId { get; set; }
        public CountryEntity Country { get; set; }
    }
}
