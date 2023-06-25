using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuyDoiDiemController : ControllerBase
    {
        private readonly IQuyDoiDiemRepository _quyDoiDiemRepository;
        public QuyDoiDiemController(IQuyDoiDiemRepository quyDoiDiemRepository)
        {
            _quyDoiDiemRepository = quyDoiDiemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuyDoi()
        {
            return Ok(await _quyDoiDiemRepository.GetAllQuyDoiDiems());
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuyDoiDiem(QuyDoiDiem obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _quyDoiDiemRepository.Create(obj))
            {
                return Ok("Thêm Thành Công");
            }
            return BadRequest("Thêm Thất Bại");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuyDoiDiem(QuyDoiDiem obj)
        {
            if (await _quyDoiDiemRepository.Update(obj))
            {
                return Ok("Chỉnh Sửa Thành Công");
            }
            return BadRequest("Chỉnh Sửa Thất Bại");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQuyDoiDiem(Guid id)
        {
            if (await _quyDoiDiemRepository.Delete(id))
            {
                return Ok("Xóa Thành Công");
            }
            return BadRequest("Xóa Thất Bại");
        }


    }
}
