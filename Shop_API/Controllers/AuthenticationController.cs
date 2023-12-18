using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Helpers;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public AuthenticationController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null) return new BadRequestObjectResult("Username or Password incorrect");

            var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!checkPassword) return new BadRequestObjectResult("Username or Password incorrect");

            IList<string>? userRoles = await _userManager.GetRolesAsync(user);

            var token = TokenHelper.GenerateToken(
                _configuration["JWT:Secret"]
                , _configuration["JWT:ValidIssuer"]
                , _configuration["JWT:ValidAudience"]
                , userRoles
                , user.Id.ToString()
                , user.UserName
                , user.FullName
                , user.PhoneNumber
                , user.Address);

            return Ok(new
            {
                Token = token,
                ValidTo = TokenHelper.GetValidTo(token)
            });
        }
    }
}
