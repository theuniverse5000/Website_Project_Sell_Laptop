using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_Models.Dto;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillController : ControllerBase
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;
        private readonly IBillDetailRepository _billDetailRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly ReponseDto _reponse;
        private static Guid getUserId;  // Tạo 1 biết static phạm vi private dùng trong controller
        private static IEnumerable<CartItemDto>? cartItem;
        private readonly IConfiguration _config;
        public BillController(IBillRepository billRepository, IBillDetailRepository billDetailRepository,
            IProductDetailRepository productDetailRepository, IUserRepository userRepository,
            ICartRepository cartRepository, IVoucherRepository voucherRepository, IConfiguration config,
            IBillService billService)
        {
            _billRepository = billRepository;
            _billDetailRepository = billDetailRepository;
            _productDetailRepository = productDetailRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _reponse = new ReponseDto();
            _voucherRepository = voucherRepository;
            _config = config;
            _billService = billService;
        }
        [AllowAnonymous]
        [HttpGet("GetAllBills")]
        public async Task<IActionResult> GetAllBills()
        {

            //string? apiKey = _config.GetSection("ApiKey").Value;
            //if (apiKey == null)
            //{
            //    return Unauthorized();
            //}

            //var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            //if (keyDomain != apiKey.ToLower())
            //{
            //    return Unauthorized();
            //}
            var bill = await _billRepository.GetAll();
            //if (bill == null)
            //{
            //    _reponse.Result = null;
            //    _reponse.IsSuccess = false;
            //    _reponse.Code = 404;
            //    _reponse.Message = "Lỗi";
            //    return Ok(_reponse);
            //}
            //else
            //{
            _reponse.Result = bill;
            _reponse.Code = 200;
            Log.Information("GetBill => {@_reponse}", _reponse);
            return Ok(_reponse);

            // }

        }
        [AllowAnonymous]
        [HttpPost("CreateBill")]
        public async Task<IActionResult> CreateBill(string username, string maVoucher)
        {
            return Ok(await _billService.CreateBill(username, maVoucher));
        }
    }
}
