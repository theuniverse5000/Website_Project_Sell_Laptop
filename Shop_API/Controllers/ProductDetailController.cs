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
        private readonly ResponseDto _reponse;
        private readonly IConfiguration _config;
        public ProductDetailController(IProductDetailRepository repository, IConfiguration config)
        {
            _repository = repository;
            _reponse = new ResponseDto();
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
        [HttpGet("PGetProductDetail")]
        public async Task<IActionResult> PGetProductDetail(int? getNumber, string? codeProductDetail, string? search, double? from, double? to, string? sortBy, int page)
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
            var listProductDetail = await _repository.PGetProductDetail(getNumber, codeProductDetail, search, from, to, sortBy, page);
            _reponse.Result = listProductDetail;
            _reponse.Count = listProductDetail.ToList().Count;
            return Ok(_reponse);
        }
        [HttpGet("GetProductDetailsPublic")]
        public async Task<IActionResult> GetProductDetailsPublic(string? search, string? productType, double? from, double? to, string? sortBy, int page)
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
            _reponse.Result = await _repository.GetProductDetailPubic(search, productType, from, to, sortBy, page);
            _reponse.Count = 10;
            return Ok(_reponse);
        }
        [HttpGet("GetByCode")]
        public async Task<IActionResult> GetProductDetailByCode(string code, string? search, string? productType, double? from, double? to, string? sortBy, int page)
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

            var list = await _repository.GetProductDetailPubic(search, productType, from, to, sortBy, page);
            var proX = list.Where(x => x.Code == code);
            _reponse.Result = proX;
            _reponse.Count = 1;
            return Ok(_reponse);
        }
        [HttpPost("Create")]
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
        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany(List<ProductDetail> list)
        {
            // List<ProductDetail> list = new List<ProductDetail>();
            if (await _repository.CreateMany(list))
            {
                return Ok(list);
            }
            return BadRequest("Lỗi");
        }

        [HttpPut("UpdateProductDetail")]
        public async Task<IActionResult> UpdateProductDetail(ProductDetail obj)
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
            return Ok(await _repository.GetById(guid));
        }

        [HttpGet("ProductDetailByIdReturnProDetailDTO")]
        public async Task<IActionResult> ProductDetailByIdReturnProDetailDTO(Guid guid)
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
            //_reponse.Result = await _repository.GetProductDetailByIdReturnDto(guid);
            return Ok(await _repository.GetProductDetailByIdReturnDto(guid));
        }

    }
}
