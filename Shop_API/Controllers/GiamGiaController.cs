using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class GiamGiaController : ControllerBase
    {
        private readonly IGiamGiaRepository _giamGiaRepository;
        private readonly IConfiguration _config;
        private readonly IPagingRepository _iPagingRepository;
        private readonly ResponseDto _reponse;
        public GiamGiaController(IGiamGiaRepository giamGiaRepository, IConfiguration config, IPagingRepository iPagingRepository)
        {
            _giamGiaRepository = giamGiaRepository;
            _config = config;
            _reponse = new ResponseDto();
            _iPagingRepository = iPagingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGiamGias()
        {
            return Ok(await _giamGiaRepository.GetAllGiamGias());
        }

        [HttpPost]
        public async Task<IActionResult> CreateGiamGia(GiamGia obj)
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
            if (await _giamGiaRepository.Create(obj))
            {
                return Ok("Thêm Thành Công");
            }
            return BadRequest("Thêm Thất Bại");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGiamGia(GiamGia obj)
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
            if (await _giamGiaRepository.Update(obj))
            {
                return Ok("Chỉnh Sửa Thành Công");
            }
            return BadRequest("Chỉnh Sửa Thất Bại");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGiamGia(Guid id)
        {
            if (await _giamGiaRepository.Delete(id))
            {
                return Ok("Xóa Thành Công");
            }
            return BadRequest("Xóa Thất Bại");
        }

        [AllowAnonymous]
        [HttpGet("GetGiamGiasFSP")]
        public IActionResult GetGiamGiasFSP(string? search, double? from, double? to, string? sortBy, int page)
        {
            _reponse.Result = _iPagingRepository.GetAllGiamGia(search, from, to, sortBy, page);
            var count = _reponse.Count = _iPagingRepository.GetAllGiamGia(search, from, to, sortBy, page).Count;
            return Ok(_reponse);
        }


        [HttpPost("CreateReturnDto")]
        public async Task<IActionResult> CreateReturnDto(GiamGia obj)
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
            obj.TrangThai = 1;
            var response = await _giamGiaRepository.CreateReturnDto(obj);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
