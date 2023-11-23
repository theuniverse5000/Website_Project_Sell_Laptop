using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturer;
        private readonly IConfiguration _config;
        private readonly IPagingRepository _iPagingRepository;
        private readonly ResponseDto _reponse;
        public ManufacturerController(IManufacturerRepository manufacturerRepository, IConfiguration config, IPagingRepository pagingRepository)
        {
            _manufacturer = manufacturerRepository;
            _config = config;
            _iPagingRepository = pagingRepository;
            _reponse = new ResponseDto();
        }

        [HttpGet]

        public async Task<IActionResult> GetAllManu()
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
            return Ok(await _manufacturer.GetAll());
        }

        [HttpPost("CreateManu")]

        public async Task<IActionResult> CreateManu(Manufacturer obj)
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
            if (await _manufacturer.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateManu(Manufacturer x)
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
            if (await _manufacturer.Update(x))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteManu(Guid id)
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
            if (await _manufacturer.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }

        [HttpGet("GetManuFSP")]
        public async Task<IActionResult> GetManuFSP(string? search, double? from, double? to, string? sortBy, int page)
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
            _reponse.Result = _iPagingRepository.GetAllManufacturer(search, from, to, sortBy, page);
            _reponse.Count = _iPagingRepository.GetAllManufacturer(search, from, to, sortBy, page).Count;
            return Ok(_reponse);
        }

        [HttpGet("ManufacturerById")]
        public async Task<IActionResult> ManufacturerById(Guid guid)
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
            _reponse.Result = await _manufacturer.GetById(guid);
            return Ok(_reponse);
        }
    }
}
