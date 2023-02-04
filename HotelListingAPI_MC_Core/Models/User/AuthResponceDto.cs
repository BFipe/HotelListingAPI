namespace HotelListingAPI_MC_Core.Models.User
{
    public class AuthResponceDto
    {
        public string UserEmail { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
