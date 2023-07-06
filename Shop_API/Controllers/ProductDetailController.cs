using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailRepository _repository;
        private readonly ReponseDto _reponse;
        public ProductDetailController(IProductDetailRepository repository)
        {
            _repository = repository;
            _reponse = new ReponseDto();
        }
        [HttpGet("GetAllNoJoin"), AllowAnonymous]
        public async Task<IActionResult> GetAlls()
        {
            _reponse.Result = await _repository.GetAll();
            _reponse.Code = 200;
            return Ok(_reponse);
        }
        [HttpGet("GetAllJoin"), Authorize]
        public async Task<IActionResult> GetAllProductDetails()
        {
            _reponse.Result = await _repository.GetAllProductDetail();
            _reponse.Code = 200;
            return Ok(_reponse);
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
                _reponse.Result = obj;
                return Ok(_reponse);
            }
            _reponse.Result = null;
            _reponse.IsSuccess = false;
            _reponse.Message = "Thất bại";
            _reponse.Code = 404;
            return BadRequest(_reponse);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(ProductDetail obj)
        {
            if (await _repository.Update(obj))
            {
                _reponse.Result = obj;
                return Ok(_reponse);
            }
            _reponse.Result = null;
            _reponse.IsSuccess = false;
            _reponse.Message = "Thất bại";
            return BadRequest(_reponse);
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
