using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Constants;
using Shop_API.Helpers;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> InitRole()
        {
            foreach (var role in SecurityRoles.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new Role
                    {
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });
                }
            }

            return Ok("Done");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var existUsername = await _userManager.FindByNameAsync(dto.UserName);

            if (existUsername != null) return new BadRequestObjectResult($"Username {dto.UserName} has already been taken");

            var appUser = UserHelper.ToApplicationUser(dto);
            appUser.Status = 1;
            var result1 = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);

            List<string> roles = new();

            if (dto.IsAdmin) roles.AddRange(SecurityRoles.Roles.ToList());
            else roles.Add("User");

            var result2 = await _userManager.AddToRolesAsync(appUser, roles);

            if (!result2.Succeeded) return new BadRequestObjectResult(result2.Errors);

            var response = await _userManager.FindByNameAsync(dto.UserName);

            return Ok(response);
        }
    }
}
