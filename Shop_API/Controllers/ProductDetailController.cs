using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailRepository _repository;
        public ProductDetailController(IProductDetailRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("GetAllNoJoin")]
        public async Task<IActionResult> GetAlls()
        {
            return Ok(await _repository.GetAll());
        }
        [HttpGet("GetAllJoin")]
        public async Task<IActionResult> GetAllProductDetails()
        {
            return Ok(await _repository.GetAllProductDetail());
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetProductDetailById(Guid id)
        {
            var list = await _repository.GetAllProductDetail();
            var proX = list.Where(x => x.Id == id);
            return Ok(proX);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(ProductDetail obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _repository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(ProductDetail obj)
        {
            if (await _repository.Update(obj))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductDetail(Guid id)
        {
            if (await _repository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }
    }
}
