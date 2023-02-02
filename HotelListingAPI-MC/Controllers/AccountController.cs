using HotelListingAPI_MC.Contracts;
using HotelListingAPI_MC.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingAPI_MC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }

        // POST: api/Account/Register
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            _logger.LogInformation($"Registration attempt for {registerUserDto.Email}");
            try
            {
                var errors = await _authManager.Register(registerUserDto);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)} when user {registerUserDto.Email} tried to register");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        // POST: api/Account/Login
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            _logger.LogInformation($"Registration attempt for {loginUserDto.Email}");
            try
            {
                var result = await _authManager.Login(loginUserDto);
                if (result is null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)} when user {loginUserDto.Email} tried to login");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }


        // POST: api/Account/RefreshToken
        [HttpPost]
        [Route("RefreshToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken([FromBody] AuthResponceDto request)
        {
            _logger.LogInformation($"Refreshing token attempt for {request.UserEmail}");
            try
            {
                var result = await _authManager.VerifyRefreshToken(request);
                if (result is null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RefreshToken)} when updating refresh token for {request.UserEmail}");
                return Problem($"Something went wrong in the {nameof(RefreshToken)}", statusCode: 500);
            }
        }
    }
}
