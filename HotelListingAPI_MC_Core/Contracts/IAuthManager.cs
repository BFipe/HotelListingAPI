using HotelListingAPI_MC_Core.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI_MC_Core.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(RegisterUserDto registerUserDto);
        
        Task<AuthResponceDto> Login(LoginUserDto registerUserDto);

        Task<string> CreateRefreshToken();

        Task<AuthResponceDto> VerifyRefreshToken(AuthResponceDto request);
    }
}
