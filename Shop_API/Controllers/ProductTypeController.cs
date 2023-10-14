
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _repository;
        private readonly IPagingRepository _iPagingRepository;
        private readonly IConfiguration _config;
        private readonly ResponseDto _reponse;
        public ProductTypeController(IProductTypeRepository repository, IConfiguration config, IPagingRepository pagingRepository)
        {
            _repository = repository;
            _config = config;
            _reponse = new ResponseDto();
            _iPagingRepository = pagingRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductType()
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


        [HttpGet("GetPagingProductsFSP")]
        public async Task<IActionResult> GetPagingProductsFSP(string? search, double? from, double? to, string? sortBy, int page)
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

            _reponse.Result = _iPagingRepository.GetAll(search, from, to, sortBy, page);
            var count = _reponse.Count = _iPagingRepository.GetAll(search, from, to, sortBy, page).Count;


            return Ok(_reponse);
        }


        [HttpPost("CreateProductType")]
        public async Task<IActionResult> CreateProductType(ProductType obj)
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
            obj.Id = Guid.NewGuid();
            obj.Status = 1;
            if (await _repository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductType(ProductType obj)
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
                return Ok("Chỉnh sửa thành công");
            }
            return BadRequest("Chỉnh sửa thất bại");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductType(Guid id)
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

        [HttpGet("ProductTypeById")]
        public async Task<IActionResult> ProductTypeById(Guid guid)
        {

            //    string apiKey = _config.GetSection("ApiKey").Value;
            //    if (apiKey == null)
            //    {
            //        return Unauthorized();
            //    }

            //    var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            //    if (keyDomain != apiKey.ToLower())
            //    {
            //        return Unauthorized();
            //    }
            _reponse.Result = await _repository.GetById(guid);
            return Ok(_reponse);
        }

    }
}
