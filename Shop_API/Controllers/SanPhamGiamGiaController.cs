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
    public class SanPhamGiamGiaController : ControllerBase
    {
        private readonly ISanPhamGiamGiaRepository _sanPhamGiamGiaRepository;
        private readonly IConfiguration _config;
        private readonly ResponseDto _reponse;
        private readonly IPagingRepository _iPagingRepository;
        public SanPhamGiamGiaController(ISanPhamGiamGiaRepository sanPhamGiamGiaRepository, IConfiguration config, IPagingRepository iPagingRepository)
        {
            _sanPhamGiamGiaRepository = sanPhamGiamGiaRepository;
            _config = config;
            _reponse = new ResponseDto();
            _iPagingRepository = iPagingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSanPhamGiamGias()
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
            return Ok(await _sanPhamGiamGiaRepository.GetAllSanPhamGiamGias());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSanPhamGiamGia(SanPhamGiamGia obj)
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
            if (await _sanPhamGiamGiaRepository.Create(obj))
            {
                return Ok("Thêm Thành Công");
            }
            return BadRequest("Thêm Thất Bại");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSanPhamGiamGia(SanPhamGiamGia obj)
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
            if (await _sanPhamGiamGiaRepository.Update(obj))
            {
                return Ok("Chỉnh Sửa Thành Công");
            }
            return BadRequest("Chỉnh Sửa Thất Bại");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPhamGiamGia(Guid id)
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
            if (await _sanPhamGiamGiaRepository.Delete(id))
            {
                return Ok("Xóa Thành Công");
            }
            return BadRequest("Xóa Thất Bại");
        }

        [AllowAnonymous]
        [HttpGet("GetSPGGPG")]
        public IActionResult GetSPGGPG(string? search, double? from, double? to, string? sortBy, int page)
        {
            _reponse.Result = _iPagingRepository.GetAllSPGGPGs(search, from, to, sortBy, page);
            var count = _reponse.Count = _iPagingRepository.GetAllSPGGPGs(search, from, to, sortBy, page).Count;
            return Ok(_reponse);
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
            _reponse.Result = await _sanPhamGiamGiaRepository.GetById(guid);
            return Ok(await _sanPhamGiamGiaRepository.GetById(guid));
        }
    }
}
