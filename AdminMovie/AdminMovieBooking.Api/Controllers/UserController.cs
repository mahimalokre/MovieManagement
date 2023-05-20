using AdminMovieBooking.Api.Security;
using AdminMovieBooking.Domain.Models;
using AdminMovieBooking.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AdminMovieBooking.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/movie/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterUserAsync(UserModel user)
        {
            if (user == null)
            {
                return NotFound();
            }

            return await _userService.RegisterUserAsync(user);
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<ResponseTokenModel>> LoginUserAsync(UserModel user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userService.LoginUserAsync(user);
            if (result.Contains("User Login successful"))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = JwtAuthentication.GetToken(authClaims, _configuration);

                return new ResponseTokenModel { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo };
            }

            return Unauthorized();
        }
    }
}
