using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Shop_API.Controllers
{
    [Route("[controller]/[action]")]
    public class PermissionController : ControllerBase
    {
        [Authorize(Roles = SecurityRoles.Admin)]
        [HttpGet]
        public IActionResult AdminRole()
        {
            return Ok("Hello Admin");
        }

        [Authorize(Roles = SecurityRoles.Manager)]
        [HttpGet]
        public IActionResult ManagerRole()
        {
            return Ok("Hello Manager");
        }

        [Authorize(Roles = SecurityRoles.User)]
        [HttpGet]
        public IActionResult UserRole()
        {
            return Ok("Hello User");
        }
    }
}
