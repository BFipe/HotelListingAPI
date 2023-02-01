using HotelListingAPI_MC.Data.Entities.HotelEntities;

namespace HotelListingAPI_MC.Data.Entities.CountryEntities
{
    public class CountryEntity
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public List<HotelEntity> Hotels { get; set; }
    }
}