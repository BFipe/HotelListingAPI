namespace HotelListingAPI_MC.Models.Hotel
{
    public class HotelDto
    {
        public int HotelEntityId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Rating { get; set; }

        public int CountryId { get; set; }
    }
}