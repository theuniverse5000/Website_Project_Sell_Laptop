using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialDaBanController : ControllerBase
    {
        private readonly ISerialDaBanRepository _repository;
        public SerialDaBanController(ISerialDaBanRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSerialDaBan()
        {
            return Ok(await _repository.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> CreateSerialDaBan(SerialDaBan obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _repository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSerialDaBan(SerialDaBan obj)
        {
            if (await _repository.Update(obj))
            {
                return Ok("Chỉnh sửa thành công");
            }
            return BadRequest("Chỉnh sửa thất bại");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteSerialDaBan(Guid id)
        {
            if (await _repository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");
        }
    }
}
