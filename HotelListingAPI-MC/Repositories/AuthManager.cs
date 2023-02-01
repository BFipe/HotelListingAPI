using AutoMapper;
using HotelListingAPI_MC.Contracts;
using HotelListingAPI_MC.Data.Entities.UserEntities;
using HotelListingAPI_MC.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI_MC.Repositories
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<APIUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> Login(LoginUserDto loginUserDto)
        {
            bool isValid = false;
            try
            {
                var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
                if (user == null) 
                {
                    return false;
                }
                isValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            }
            catch (Exception)
            {
                isValid = false;
            }
            return isValid;
        }

        public async Task<IEnumerable<IdentityError>> Register(RegisterUserDto registerUserDto)
        {
            List<IdentityError> resultErrors = new List<IdentityError>();
            var user = _mapper.Map<APIUser>(registerUserDto);
            user.UserName = registerUserDto.Email;

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                if (addToRoleResult.Succeeded == false)
                {
                    resultErrors.AddRange(addToRoleResult.Errors);
                }
            }
            else
            {
                resultErrors.AddRange(result.Errors);
            }

            return resultErrors;
        }
    }
}
