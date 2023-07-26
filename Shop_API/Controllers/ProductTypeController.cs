using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _repository;
        public ProductTypeController(IProductTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductType(ProductType obj)
        {
            obj.Id = Guid.NewGuid();
            if(await _repository.Create(obj))
            {
                return Ok("Thêm thành công");
            }return BadRequest("Thêm thất bại");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductType(ProductType obj)
        {
           
            if (await _repository.Update(obj))
            {
                return Ok("Chỉnh sửa thành công");
            }
            return BadRequest("Chỉnh sửa thất bại");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductType(Guid id)
        {

            if (await _repository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductType()
        {
            return Ok(await _repository.GetAll());
        }
    }
}
