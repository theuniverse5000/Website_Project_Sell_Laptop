using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagePostController : Controller
    {
        private readonly IManagePostRepository _mpRepository;

        public ManagePostController(IManagePostRepository mpRepository)
        {
            _mpRepository = mpRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMPs()
        {
            return Ok(await _mpRepository.GetAllManagePosts());
        }
        [HttpPost]
        public async Task<IActionResult> CreateMP(ManagePost obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _mpRepository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMP(ManagePost obj)
        {
            if (await _mpRepository.Update(obj))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteMP(Guid id)
        {
            if (await _mpRepository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }
    }
}
