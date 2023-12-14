using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,User")]
    public class ChucNangTichDiemController : ControllerBase
    {
        private readonly ITichDiemServices _tichDiemServices;
        private readonly IConfiguration _config;
        public readonly IViDiemRepository _viDiemRepository;
        public readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public ChucNangTichDiemController(ITichDiemServices tichDiemServices, IConfiguration config, IViDiemRepository viDiemRepository, IUserRepository userRepository, UserManager<User> userManager)
        {
            _tichDiemServices = tichDiemServices;
            _config = config;
            _viDiemRepository = viDiemRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpPost("first-buy")]
        public async Task<IActionResult> TieuDiemChoLanDauMuaAsync(string invoiceCode, double TongTienThanhToan, double? SoDiemMuonDung)
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
            return Ok(await _tichDiemServices.TichDiemChoLanDauMuaHangAsync(invoiceCode, TongTienThanhToan, SoDiemMuonDung));
        } 
        
        [HttpPost("TichDiemMuaHangAsync")]
        public async Task<IActionResult> TichDiemMuaHangAsync(string invoiceCode, double TongTienThanhToan)
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
            return Ok(await _tichDiemServices.TichDiemMuaHangAsync(invoiceCode, TongTienThanhToan));
        }

        [HttpPost("TieuDiemMuaHangAsync")]
        public async Task<IActionResult> TieuDiemMuaHangAsync(string invoiceCode, double TongTienThanhToan, double SoDiemMuonDung)
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
            return Ok(await _tichDiemServices.TieuDiemMuaHangAsync(invoiceCode, TongTienThanhToan, SoDiemMuonDung));
        }

        [HttpGet("GetStatusOfPointWallet")]
        public async Task<IActionResult> GetStatusOfPointWallet(string usename)
        {
            var user = await _userRepository.GetUserByUserName(usename);
            //var user = await _userManager.FindByNameAsync(usename);
            var viDiem = await _viDiemRepository.GetViDiemById(user.Id);

            if (viDiem != null)
            {
                return Ok(viDiem.TrangThai);
            } return BadRequest(0);
        }

        [HttpGet("GetThePointOfWallet")]
        public async Task<IActionResult> GetThePointOfWallet(string usename)
        {
            try
            {
                var user = await _userRepository.GetUserByUserName(usename);
                var viDiem = await _viDiemRepository.GetViDiemById(user.Id);
                var soDiemHienCo = viDiem.TongDiem - viDiem.SoDiemDaDung;

                return Ok(new { soDiemHienCo });
            }
            catch (Exception ex)
            {
                // Trả về một trạng thái HTTP 400 và một thông báo lỗi nếu có lỗi
                return BadRequest(new { message = "Lỗi khi lấy số điểm từ ví điểm", error = ex.Message });
            }
        }


        // Trong Controller hoặc nơi khác tương ứng
        //private double CalculateTotalPrice(ProductDetailViewModel productDetail)
        //{
        //    return productDetail.Price * productDetail.Quantity;
        //}


        [HttpPost("CalculateTotalAmount")]
        public IActionResult CalculateTotalAmount(double tongTienHoaDon, double? soDiemMuonDoi)
        {
            const double tiLeDiemTichLuy = 0.000000625;
            const double diemToVND = 10000;

            double diemTichLuy = tongTienHoaDon > 0 ? tongTienHoaDon * tiLeDiemTichLuy : 0;
            double soTienSauKhiDoi = soDiemMuonDoi > 0 ? (double)(soDiemMuonDoi * diemToVND) : 0;

            double totalAmount = tongTienHoaDon - soTienSauKhiDoi;

            return Ok(totalAmount) ;
        }
    }
}
