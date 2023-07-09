using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillDetailRepository _billDetailRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly ReponseDto _reponse;
        private static Guid getUserId;  // Tạo 1 biết static phạm vi private dùng trong controller
        private static IEnumerable<CartItemDto>? cartItem;
        public BillController(IBillRepository billRepository, IBillDetailRepository billDetailRepository,
            IProductDetailRepository productDetailRepository, IUserRepository userRepository,
            ICartRepository cartRepository, IVoucherRepository voucherRepository)
        {
            _billRepository = billRepository;
            _billDetailRepository = billDetailRepository;
            _productDetailRepository = productDetailRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _reponse = new ReponseDto();
            _voucherRepository = voucherRepository;
        }
        [HttpGet("GetAllBills")]
        public async Task<ReponseDto> GetAllBills()
        {
            var bill = await _billRepository.GetAll();
            if (bill == null)
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = "Lỗi";
                return _reponse;
            }
            else
            {
                _reponse.Result = bill;
                _reponse.Code = 200;
                return _reponse;
            }

        }
        [HttpPost("CreateBill")]
        public async Task<ReponseDto> CreateBill(string username, string maVoucher)
        {
            getUserId = _userRepository.GetAllUsers().Result.FirstOrDefault(x => x.Username == username).Id;
            cartItem = await _cartRepository.GetCartItem(username);
            if (maVoucher == null || cartItem == null)
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = "Không có sản phẩm trong giỏ hàng";
                return _reponse;
            }
            else
            {
                var listVoucher = await _voucherRepository.GetAllVouchers();
                var voucherX = listVoucher.FirstOrDefault(x => x.MaVoucher == maVoucher);
                Bill bill = new Bill()
                {
                    Id = Guid.NewGuid(),
                    Ma = "Bill" + DateTime.Now.ToString(),
                    CreateDate = DateTime.Now,
                    HoTenKhachHang = "1",
                    SDTKhachHang = "2",
                    DiaChiKhachHang = "3",
                    Status = 1,
                    UserId = getUserId,
                    VoucherId = voucherX != null ? voucherX.Id : null
                };
                if (await _billRepository.Create(bill))
                {
                    foreach (var x in cartItem)
                    {
                        List<BillDetail> listBillDetal = new List<BillDetail>();
                        BillDetail billDetail = new BillDetail();
                        billDetail.Id = Guid.NewGuid();
                        billDetail.BillId = bill.Id;
                        billDetail.ProductDetailId = x.IdProductDetails;
                        billDetail.Quantity = x.Quantity;
                        billDetail.Price = Convert.ToDouble(x.Price);
                        if (!await _billDetailRepository.CreateBillDetail(billDetail))
                        {
                            _reponse.Result = null;
                            _reponse.IsSuccess = false;
                            _reponse.Code = 404;
                            _reponse.Message = "Lỗi khi thêm sản phẩm vào hóa đơn";
                            return _reponse;
                        }
                    }
                    _reponse.Result = null;
                    _reponse.IsSuccess = true;
                    _reponse.Code = 200;
                    _reponse.Message = "Thành công";
                    return _reponse;
                }
                else
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không thể tạo hóa đơn";
                    return _reponse;
                }
            }
            _reponse.Result = null;
            _reponse.IsSuccess = false;
            _reponse.Code = 404;
            _reponse.Message = "g";
            return _reponse;
        }
    }
}
