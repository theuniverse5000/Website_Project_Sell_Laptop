using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiamGiaController : ControllerBase
    {
        private readonly IGiamGiaRepository _giamGiaRepository;
        public GiamGiaController(IGiamGiaRepository giamGiaRepository)
        {
            _giamGiaRepository = giamGiaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGiamGias()
        {
            return Ok(await _giamGiaRepository.GetAllGiamGias());
        }

        [HttpPost]
        public async Task<IActionResult> CreateGiamGia(GiamGia obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _giamGiaRepository.Create(obj))
            {
                return Ok("Thêm Thành Công");
            }
            return BadRequest("Thêm Thất Bại");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGiamGia(GiamGia obj)
        {
            if (await _giamGiaRepository.Update(obj))
            {
                return Ok("Chỉnh Sửa Thành Công");
            }
            return BadRequest("Chỉnh Sửa Thất Bại");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGiamGia(Guid id)
        {
            if (await _giamGiaRepository.Delete(id))
            {
                return Ok("Xóa Thành Công");
            }
            return BadRequest("Xóa Thất Bại");
        }
    }
}
