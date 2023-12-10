using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailRepository _repository;
        private readonly ResponseDto _reponse;
        private readonly IConfiguration _config;
        public ProductDetailController(IProductDetailRepository repository, IConfiguration config)
        {
            _repository = repository;
            _reponse = new ResponseDto();
            _config = config;
        }
        [HttpGet("GetAllPDD")]

        public async Task<IActionResult> GetAllPDD()
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
            return Ok(await _repository.GetAll());
        }

        [HttpGet("GetAlls")]
        public async Task<IActionResult> GetAlls()
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
            _reponse.Result = await _repository.GetAll();
            return Ok(_reponse);
        }
       
        [HttpGet("PGetProductDetail")]
        public async Task<IActionResult> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? search, double? from, double? to, string? sortBy, int page)
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
            var listProductDetail = await _repository.PGetProductDetail(getNumber, codeProductDetail, status, search, from, to, sortBy, page);
            _reponse.Result = listProductDetail;
            _reponse.Count = listProductDetail.ToList().Count;
            return Ok(_reponse);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProductDetail(ProductDetail obj)
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
            //obj.Id = Guid.NewGuid();
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

        [HttpPost("CreateReturnDTO")]
        public async Task<IActionResult> CreateReturnDTO(ProductDetail obj)
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
            //obj.Id = Guid.NewGuid();
            var response = await _repository.CreateReturnDto(obj);
            if (response.IsSuccess)
            {
                //_reponse.Result = obj;
                return Ok(response);
            }
            //_reponse.Result = null;
            //_reponse.IsSuccess = false;
            //_reponse.Message = "Thất bại";
            return BadRequest(response);
        }

        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany(List<ProductDetail> list)
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
            if (await _repository.CreateMany(list))
            {
                return Ok(list);
            }
            return BadRequest("Lỗi");
        }

        [HttpPut("UpdateProductDetail")]
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

        [HttpGet("ProductDetailById")]
        public async Task<IActionResult> ProductDetailById(Guid guid)
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
            _reponse.Result = await _repository.GetById(guid);
            return Ok(await _repository.GetById(guid));
        }

    }
}
