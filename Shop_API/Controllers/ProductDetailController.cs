using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_API.RequestModel;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : LaptopShopController
    {
        private readonly IProductDetailRepository _repository;
        private readonly ReponseDto _reponse;
        private readonly IConfiguration _config;
        public ProductDetailController(IProductDetailRepository repository, IConfiguration config)
        {
            _repository = repository;
            _reponse = new ReponseDto();
            _config = config;
        }
        [HttpGet("GetAllNoJoin")]
        public async Task<IActionResult> GetAlls(
            [FromQuery] int? page,
            [FromQuery] int? size,
            [FromQuery] string? sort,
            [FromQuery] string? filter)
        {
            var queryModel = new ProductDetailQueryModel();
            queryModel.CurrentPage = page ?? 1;
            queryModel.PageSize = size ?? 20;
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
            _reponse.Result = await _repository.GetAll(queryModel);
            return Ok(_reponse);
        }
        [HttpGet("GetProductDetailsFSP")]
        public async Task<IActionResult> GetProductDetailsFSP(string? search, double? from, double? to, string? sortBy, int page)
        {
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
            _reponse.Result = await _repository.GetProductDetail(search, from, to, sortBy, page);
            _reponse.Count = 10;
            return Ok(_reponse);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetProductDetailById(Guid id)
        {
            //string apiKey = _config.GetSection("ApiKey").Value;
            //if (apiKey == null)
            //{
            //    return Unauthorized();
            //}

            //var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            //if (keyDomain != apiKey.ToLower())
            //{
            //    return Unauthorized();
            //}
            //var list = await _repository.GetAllProductDetail();
            //var proX = list.Where(x => x.Id == id);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(ProductDetail obj)
        {
            //string apiKey = _config.GetSection("ApiKey").Value;
            //if (apiKey == null)
            //{
            //    return Unauthorized();
            //}

            //var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            //if (keyDomain != apiKey.ToLower())
            //{
            //    return Unauthorized();
            //}
            obj.Id = Guid.NewGuid();
            if (await _repository.Create(obj))
            {
                _reponse.Result = obj;
                return Ok(_reponse);
            }
            _reponse.Result = null;
            _reponse.IsSuccess = false;
            _reponse.Message = "Thất bại";
            return BadRequest(_reponse);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(ProductDetail obj)
        {
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
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
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
            if (await _repository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }
    }
}
