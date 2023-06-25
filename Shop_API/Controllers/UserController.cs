using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var list = _userRepository.GetAllUsers();
            return Ok(await list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _userRepository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(User obj)
        {
            if (await _userRepository.Update(obj))
            {
                return Ok("Chỉnh sửa thành Công");
            }
            return BadRequest("Chỉnh sửa Thất Bại");

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (await _userRepository.Delete(id))
            {
                return Ok("Xóa thành Công");
            }
            return BadRequest("Xóa Thất Bại");
        }
    }
}
