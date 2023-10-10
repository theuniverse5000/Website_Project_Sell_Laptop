using Microsoft.AspNetCore.Mvc;
using Shop_API.Service.IService;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> TieuDiemChoLanDauMuaAsync(Guid IdBill, double TongTienThanhToan)
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
            return Ok(_tichDiemServices.TichDiemChoLanDauMuaHang(IdBill, TongTienThanhToan));
        }

        [HttpPost]
        public async Task<IActionResult> TieuDiemChoLanMuaSauAsync(Guid IdBill, double TongTienThanhToan,double SoDiemMuonDung)
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
            return Ok(_tichDiemServices.TichDiemChoNhungLanMuaSau(IdBill, TongTienThanhToan, SoDiemMuonDung));
        }

    }
}
