using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_Models.Dto.Role;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService; 
        private readonly IConfiguration _config;
        public RoleController(IRoleService roleService, IConfiguration config)
        {
            _roleService = roleService;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {

            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
            return Ok(await _roleService.GetAllRole());
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(Role obj)
        {

            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
            var roleCreate = new RoleCreateDto() 
            {
                Name = obj.Name,
                concurrencyStamp= Guid.NewGuid().ToString(),
                normalizedName= obj.NormalizedName,
            };
            
            try
            {
                var result = await _roleService.CreatRole(roleCreate);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateRole(Guid id, RoleUpdateDto roleUpdateDto)
        {

            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
            try
            {
               var result = await _roleService.EditRole(id, roleUpdateDto);
                return Ok(result);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {

            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
            try
            {
                var reuslt =await _roleService.DelRole(id);
                return Ok(reuslt);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
