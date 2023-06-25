using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuTieuDiemController : ControllerBase
    {
        private readonly ILichSuTieuDiemRepository _lichSuTieuDiem;
        public LichSuTieuDiemController(ILichSuTieuDiemRepository lichSuTieuDiem)
        {
            _lichSuTieuDiem = lichSuTieuDiem;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLichSuTieuDiem()
        {
            return Ok(await _lichSuTieuDiem.GetAllLichSuTieuDiems());
        }

        [HttpPost]
        public async Task<IActionResult> CreateLichSuTieuDiem(LichSuTieuDiem obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _lichSuTieuDiem.Create(obj))
            {
                return Ok("Thêm thành Công");
            }
            return BadRequest("Thêm Thất Bại");

        }
        [HttpPut]
        public async Task<IActionResult> UpdateLichSuTieuDiem(LichSuTieuDiem obj)
        {
            if (await _lichSuTieuDiem.Update(obj))
            {
                return Ok("Chỉnh sửa thành Công");
            }
            return BadRequest("Chỉnh sửa Thất Bại");

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLichSuTieuDiem(Guid id)
        {
            if (await _lichSuTieuDiem.Delete(id))
            {
                return Ok("Xóa thành Công");
            }
            return BadRequest("Xóa Thất Bại");
        }
    }
}
