using AutoMapper;
using HotelListingAPI_MC_Core.Contracts;
using HotelListingAPI_MC_Core.Models.User;
using HotelListingAPI_MC_Data.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListingAPI_MC_Core.Repositories
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private APIUser _user;

        private const string _loginProvider = "HotelListingAPI-MC";
        private const string _refreshToken = "RefreshToken";


        public AuthManager(IMapper mapper, UserManager<APIUser> userManager, IConfiguration configuration, ILogger<AuthManager> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> CreateRefreshToken()
        {
            try
            {
                await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
                var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
                var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
                if (result.Succeeded)
                {
                    return newRefreshToken;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (_user == null)
                {
                    _logger.LogError(ex, $"Something went wrong in the {nameof(CreateRefreshToken)} while trying to create refresh token for null user");
                }
                else
                {
                    _logger.LogError(ex, $"Something went wrong in the {nameof(CreateRefreshToken)} while trying to create refresh token for user {_user.Email}");
                }
                throw;
            }

        }

        public async Task<AuthResponceDto> VerifyRefreshToken(AuthResponceDto request)
        {
            try
            {
                var jwtSequrityTokenHandler = new JwtSecurityTokenHandler();
                if (jwtSequrityTokenHandler.CanReadToken(request.Token) == false)
                {
                    return null;
                }

                var tokenContent = jwtSequrityTokenHandler.ReadJwtToken(request.Token);

                var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Sub)?.Value;
                _user = await _userManager.FindByNameAsync(username);
                if (_user is null || _user.Email != request.UserEmail)
                {
                    return null;
                }

                var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);
                if (isValidRefreshToken)
                {
                    var refreshToken = await CreateRefreshToken();
                    if (refreshToken is not null)
                    {
                        var token = await GenerateToken();
                        return new AuthResponceDto
                        {
                            Token = token,
                            UserEmail = _user.Email,
                            RefreshToken = refreshToken,
                        };
                    }
                }

                await _userManager.UpdateSecurityStampAsync(_user);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateRefreshToken)} while trying to verify refresh token for user {_user.Email}");
                throw;
            }
        }

        public async Task<AuthResponceDto> Login(LoginUserDto loginUserDto)
        {
            bool isValid = false;

            _user = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (_user == null)
            {
                return null;
            }
            isValid = await _userManager.CheckPasswordAsync(_user, loginUserDto.Password);

            if (isValid)
            {
                var token = await GenerateToken();
                return new AuthResponceDto()
                {
                    UserEmail = _user.Email,
                    Token = token,
                    RefreshToken = await CreateRefreshToken()
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
            _user = _mapper.Map<APIUser>(registerUserDto);
            _user.UserName = registerUserDto.Email;

            var result = await _userManager.CreateAsync(_user, registerUserDto.Password);

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(_user, "User");
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

        private async Task<string> GenerateToken()
        {
            var sequrityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));

            var credentials = new SigningCredentials(sequrityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
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
