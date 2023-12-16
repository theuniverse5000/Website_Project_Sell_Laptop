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
            return Ok(await _sanPhamGiamGiaRepository.GetAllSanPhamGiamGias());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSanPhamGiamGia(SanPhamGiamGia obj)
        {
            obj.Id = Guid.NewGuid();
            obj.TrangThai = 1;
            var response = await _sanPhamGiamGiaRepository.Create(obj);
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

        [HttpPut]
        public async Task<IActionResult> UpdateSanPhamGiamGia(SanPhamGiamGia obj)
        {
            if (await _sanPhamGiamGiaRepository.Update(obj))
            {
                return Ok("Chỉnh Sửa Thành Công");
            }
            return BadRequest("Chỉnh Sửa Thất Bại");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPhamGiamGia(Guid id)
        {
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
    }
}
