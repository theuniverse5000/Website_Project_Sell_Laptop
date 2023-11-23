using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Service.IService;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ChucNangTichDiemController : ControllerBase
    {
        private readonly ITichDiemServices _tichDiemServices;
        private readonly IConfiguration _config;
        public ChucNangTichDiemController(ITichDiemServices tichDiemServices, IConfiguration config)
        {
            _tichDiemServices = tichDiemServices;
            _config = config;

        }

        [HttpPost("first-buy")]
        public async Task<IActionResult> TieuDiemChoLanDauMuaAsync(string phoneNumber, double TongTienThanhToan)
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
            return Ok(await _tichDiemServices.TichDiemChoLanDauMuaHangAsync(phoneNumber, TongTienThanhToan));
        }

        [HttpPost]
        public async Task<IActionResult> TieuDiemChoLanMuaSauAsync(string phoneNumber, double TongTienThanhToan, double SoDiemMuonDung)
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
            return Ok(await _tichDiemServices.TichDiemChoNhungLanMuaSauAsync(phoneNumber, TongTienThanhToan, SoDiemMuonDung));
        }

    }
}
