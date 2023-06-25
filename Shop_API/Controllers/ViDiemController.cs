using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViDiemController : ControllerBase
    {
        private readonly IViDiemRepository _viDiemRepository;
        public ViDiemController(IViDiemRepository viDiemRepository)
        {
            _viDiemRepository = viDiemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllViDiem()
        {
            return Ok(await _viDiemRepository.GetAllViDiems());
        }

        [HttpPost]
        public async Task<IActionResult> CreateViDiem(ViDiem obj)
        {
            if (await _viDiemRepository.Create(obj))
            {
                return Ok("Thêm thành Công");
            }
            return BadRequest("Thêm Thất Bại");

        }
        [HttpPut]
        public async Task<IActionResult> UpdateViDiem(ViDiem obj)
        {
            if (await _viDiemRepository.Update(obj))
            {
                return Ok("Chỉnh sửa thành Công");
            }
            return BadRequest("Chỉnh sửa Thất Bại");

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViDiem(Guid id)
        {
            if (await _viDiemRepository.Delete(id))
            {
                return Ok("Xóa thành Công");
            }
            return BadRequest("Xóa Thất Bại");
        }
    }
}
