using HotelListingAPI_MC.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI_MC.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(RegisterUserDto registerUserDto);
        Task<AuthResponceDto> Login(LoginUserDto registerUserDto);
    }
}
