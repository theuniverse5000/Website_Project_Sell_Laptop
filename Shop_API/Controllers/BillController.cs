﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize]
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
        [AllowAnonymous]
        [HttpPost("CreateBill")]
        public async Task<ReponseDto> CreateBill(string username, string maVoucher)
        {
            try // Bắt lỗi ngoại lệ
            {
                var user = _userRepository.GetAllUsers().Result.FirstOrDefault(x => x.Username == username);
                if (user == null)
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không tìm thấy tài khoản người dùng";
                    return _reponse;// Nếu chưa có sản phẩm trong giỏ hàng thì không thể tạo hóa đơn
                }
                getUserId = user.Id;
                cartItem = await _cartRepository.GetCartItem(username);// Tìm  ra giỏ hàng của user
                if (cartItem == null)// Kiểm tra giỏ hàng của người dùng 
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không có sản phẩm trong giỏ hàng";
                    return _reponse;// Nếu chưa có sản phẩm trong giỏ hàng thì không thể tạo hóa đơn
                }
                else
                {
                    var listVoucher = await _voucherRepository.GetAllVouchers();// Lấy danh sách voucher
                    var voucherX = listVoucher.FirstOrDefault(x => x.MaVoucher == maVoucher);// Kiểm tra xem có voucher ko
                    if (voucherX == null)// Không có thì trả về kết quả
                    {
                        _reponse.Result = null;
                        _reponse.IsSuccess = false;
                        _reponse.Code = 404;
                        _reponse.Message = "Voucher sai hoặc không tồn tại";
                        return _reponse;
                    }
                    int checkSlVoucher = voucherX.SoLuong;
                    DateTime startTimeVoucher = voucherX.StarDay;
                    DateTime endTimeVoucher = voucherX.EndDay;
                    if (checkSlVoucher <= 0)
                    {
                        _reponse.Result = null;
                        _reponse.IsSuccess = false;
                        _reponse.Code = 404;
                        _reponse.Message = "Voucher đã hết lượt sử dụng";
                        return _reponse;
                    }
                    if (startTimeVoucher > DateTime.Now || DateTime.Now > endTimeVoucher)
                    {
                        _reponse.Result = null;
                        _reponse.IsSuccess = false;
                        _reponse.Code = 404;
                        _reponse.Message = "Voucher đã hết hạn sử dụng hoặc chưa đến ngày sử dụng";
                        return _reponse;
                    }
                    Bill bill = new Bill()// Tạo hóa đơn
                    {
                        Id = Guid.NewGuid(),
                        Ma = "Bill" + DateTime.Now.ToString(),
                        CreateDate = Convert.ToDateTime(DateTime.Now.ToString()),
                        HoTenKhachHang = user.FullName,
                        SDTKhachHang = user.PhoneNumber,
                        DiaChiKhachHang = user.Address,
                        Status = 1,
                        UserId = getUserId,
                        VoucherId = voucherX != null ? voucherX.Id : null
                    };
                    if (await _billRepository.Create(bill))// Nếu tạo hóa đơn thành công thì tiếp tục 
                    {
                        foreach (var x in cartItem)
                        {
                            var productDetailX = _productDetailRepository.GetAll().Result.FirstOrDefault(x => x.Id == x.Id);
                            if (productDetailX == null)
                            {
                                _reponse.IsSuccess = false;
                                _reponse.Code = 404;
                                _reponse.Message = "Sản phẩm không tồn tại";
                                return _reponse;
                            }
                            int soLuongProductDetail = productDetailX.AvailableQuantity;
                            if (soLuongProductDetail <= 0 || soLuongProductDetail < x.Quantity)
                            {
                                _reponse.Result = null;
                                _reponse.IsSuccess = false;
                                _reponse.Code = 404;
                                _reponse.Message = "Số lượng sản phẩm trong kho không đủ";
                                return _reponse;
                            }
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
                    else // Nếu không tạo được hóa đơn thì trả về kết quả
                    {
                        _reponse.Result = null;
                        _reponse.IsSuccess = false;
                        _reponse.Code = 404;
                        _reponse.Message = "Không thể tạo hóa đơn";
                        return _reponse;
                    }
                }
            }
            catch (Exception e)
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = "Có lỗi gì đó: " + e.Message;
                return _reponse;
            }
        }
    }
}
