using AutoMapper;
using HotelListingAPI_MC.Contracts;
using HotelListingAPI_MC.Data.Entities.UserEntities;
using HotelListingAPI_MC.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListingAPI_MC.Repositories
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(IMapper mapper, UserManager<APIUser> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponceDto> Login(LoginUserDto loginUserDto)
        {
            bool isValid = false;

            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (user == null)
            {
                return null;
            }
            isValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (isValid)
            {
                var token = await GenerateToken(user);
                return new AuthResponceDto()
                {
                    UserEmail = user.Email,
                    Token = token
                };
            }
            else
            {
                return null;
            }

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

        private async Task<string> GenerateToken(APIUser user)
        {
            var sequrityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));

            var credentials = new SigningCredentials(sequrityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
