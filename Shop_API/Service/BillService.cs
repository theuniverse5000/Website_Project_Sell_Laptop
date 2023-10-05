using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_API.Utilities;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Service
{
    public class BillService : IBillService
    {

        private readonly IBillRepository _billRepository;
        private readonly IBillDetailRepository _billDetailRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly ReponseDto _reponse;
        private readonly ReponseBillDto _reponseBill;
        private static Guid getUserId;  // Tạo 1 biết static phạm vi private dùng trong controller
        private static IEnumerable<CartItemDto>? cartItem;
        public BillService(IBillRepository billRepository, IBillDetailRepository billDetailRepository,
            IProductDetailRepository productDetailRepository, IUserRepository userRepository,
            ICartRepository cartRepository, IVoucherRepository voucherRepository)
        {
            _billRepository = billRepository;
            _billDetailRepository = billDetailRepository;
            _productDetailRepository = productDetailRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _reponse = new ReponseDto();
            _reponseBill = new ReponseBillDto();
            _voucherRepository = voucherRepository;
        }

        public async Task<ReponseDto> CreateBill(string username, string? maVoucher)
        {
            try
            {
                var user = _userRepository.GetAllUsers().Result;//.FirstOrDefault(x => x.UserName == username);
                if (user == null)
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không tìm thấy tài khoản người dùng";
                    return _reponse;// Nếu chưa có sản phẩm trong giỏ hàng thì không thể tạo hóa đơn
                }
                //   getUserId = user.Id;
                cartItem = await _cartRepository.GetCartItem(username);// Tìm  ra giỏ hàng của user
                if (cartItem == null)// Kiểm tra giỏ hàng của người dùng 
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không có sản phẩm trong giỏ hàng";
                    return _reponse;// Nếu chưa có sản phẩm trong giỏ hàng thì không thể tạo hóa đơn
                }
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
                    InvoiceCode = "Bill" + RamdomString.GenerateRandomString(7),
                    CreateDate = Convert.ToDateTime(DateTime.Now.ToString()),
                    //FullName = user.FullName,
                    //PhoneNumber = user.PhoneNumber,
                    //Address = user.Address,
                    Status = 2,// Trạng thái 2: Chờ xác nhận
                    UserId = getUserId,
                    VoucherId = voucherX != null ? voucherX.Id : null
                };
                if (await _billRepository.Create(bill))
                {
                    var cartItemToBill = cartItem.Where(x => x.Status == 3).ToList();
                    foreach (var x in cartItemToBill)
                    {
                        //var productDetailX = _productDetailRepository.GetAll().Result.FirstOrDefault(x => x.Id == x.Id);
                        //if (productDetailX == null)
                        //{
                        //    _reponse.IsSuccess = false;
                        //    _reponse.Code = 404;
                        //    _reponse.Message = "Sản phẩm không tồn tại";
                        //    return _reponse;
                        //}

                        BillDetail billDetail = new BillDetail();
                        billDetail.Id = Guid.NewGuid();
                        billDetail.Code = bill.InvoiceCode + RamdomString.GenerateRandomString(6);
                        // billDetail.CodeProductDetail = productDetailX.Code;
                        billDetail.Price = x.Price;
                        billDetail.Quantity = x.Quantity;
                        billDetail.BillId = bill.Id;
                        await _billDetailRepository.CreateBillDetail(billDetail);


                    }
                    _reponse.Result = bill;
                    _reponse.IsSuccess = true;
                    _reponse.Code = 200;
                    _reponse.Message = "Đặt hàng thành công";
                    return _reponse;
                }
                else
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 400;
                    _reponse.Message = "Đặt hàng thất bại";
                    return _reponse;
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

        public async Task<ReponseDto> CreateBillDetail(string invoiceCode, string serialNumber, string username)
        {
            var billX = _billRepository.GetAll().Result.FirstOrDefault(x => x.InvoiceCode == invoiceCode);
            var user = _userRepository.GetAllUsers().Result.FirstOrDefault();//x => x.UserName == username
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
            BillDetail billDetail = new BillDetail();
            billDetail.Id = Guid.NewGuid();
            billDetail.BillId = billX.Id;
            //   billDetail.ProductDetailId = x.IdProductDetails;
            //   billDetail.Quantity = x.Quantity;
            billDetail.Price = 1000;
            if (!await _billDetailRepository.CreateBillDetail(billDetail))
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = "Lỗi khi thêm sản phẩm vào hóa đơn";
                return _reponse;
            }
            throw new NotImplementedException();
        }

        public Task<ReponseDto> CreateBillDetail(string invoiceCode, string serialNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<ReponseBillDto> GetBillByPhoneNumber(string phoneNumber)
        {
            Bill billT = await _billRepository.GetBillByPhoneNumber(phoneNumber);

            var listBillDetail = await _billRepository.GetBillDetailByPhoneNumber(phoneNumber);
            _reponseBill.InvoiceCode = billT.InvoiceCode;
            _reponseBill.BillDetail = listBillDetail;
            _reponseBill.Count = listBillDetail.Count();
            _reponseBill.Code = 201;
            _reponseBill.IsSuccess = true;
            _reponseBill.Message = "Lấy thành công";
            return _reponseBill;
        }

        public async Task<Bill> GetAllBill(string phoneNumber)
        {
            return await _billRepository.GetBillByPhoneNumber(phoneNumber);
        }
    }
}
