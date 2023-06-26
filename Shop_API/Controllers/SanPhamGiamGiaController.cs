using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamGiamGiaController : ControllerBase
    {
        private readonly ISanPhamGiamGiaRepository _sanPhamGiamGiaRepository;
        public SanPhamGiamGiaController(ISanPhamGiamGiaRepository sanPhamGiamGiaRepository)
        {
            _sanPhamGiamGiaRepository = sanPhamGiamGiaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSanPhamGiamGias()
        {
            return Ok(await _sanPhamGiamGiaRepository.GetAllSanPhamGiamGias());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSanPhamGiamGia(SanPhamGiamGia obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _sanPhamGiamGiaRepository.Create(obj))
            {
                return Ok("Thêm Thành Công");
            }
            return BadRequest("Thêm Thất Bại");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSanPhamGiamGia(SanPhamGiamGia obj)
        {
            if (await _sanPhamGiamGiaRepository.Update(obj))
            {
                return Ok("Chỉnh Sửa Thành Công");
            }
            return BadRequest("Chỉnh Sửa Thất Bại");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPhamGiamGia(Guid id)
        {
            if (await _sanPhamGiamGiaRepository.Delete(id))
            {
                return Ok("Xóa Thành Công");
            }
            return BadRequest("Xóa Thất Bại");
        }
    }
}
