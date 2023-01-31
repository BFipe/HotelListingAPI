namespace HotelListingAPI_DATA.Entities
{
    public class CountryEntity
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public List<HotelEntity> Hotels { get; set; }
    }
}