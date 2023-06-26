using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturer;

        public ManufacturerController(IManufacturerRepository manufacturerRepository)
        {
            _manufacturer = manufacturerRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllManu()
        {
            return Ok(await _manufacturer.GetAll());
        }

        [HttpPost]

        public async Task<IActionResult> Create(Manufacturer obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _manufacturer.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }

        [HttpPut]
        public async Task<IActionResult> Update(Manufacturer x)
        {
            if (await _manufacturer.Update(x))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _manufacturer.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }
    }
}
