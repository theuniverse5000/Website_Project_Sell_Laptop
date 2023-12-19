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
        private readonly ResponseDto _reponse;
        private readonly BillDto _reponseBill;
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
            _reponse = new ResponseDto();
            _reponseBill = new BillDto();
            _voucherRepository = voucherRepository;
        }

        public async Task<ResponseDto> CreateBill(RequestBillDto request)
        {
            try
            {
                var user = _userRepository.GetAllUsers().Result.Where(x => x.UserName == request.Usename).FirstOrDefault();

                var cartItem = await _cartRepository.GetCartItem(request.Usename);
                //if (cartItem == null)
                //{
                //    return NotFoundResponse("Không có sản phẩm trong giỏ hàng");
                //}



                var listVoucher = await _voucherRepository.GetAllVouchers();
                var voucherX = listVoucher.FirstOrDefault(x => x.MaVoucher == request.CodeVoucher);
                var bill = new Bill
                {
                    Id = Guid.NewGuid(),
                    InvoiceCode = "Bill" + RamdomString.GenerateRandomString(10),
                    CreateDate = DateTime.Now,
                    Status = 2, // Trạng thái 2: Chờ xác nhận
                    FullName = user != null ? user.FullName : request.FullName,
                    PhoneNumber = user != null ? user.PhoneNumber : request.Address,
                    Address = user != null ? user.Address : request.PhoneNumber,
                    UserId = user != null ? user.Id : null,
                    Payment = request.Payment,
                    IsPayment = false,
                    VoucherId = voucherX != null ? voucherX.Id : (Guid?)null
                };

                if (await _billRepository.Create(bill))
                {
                    if (request.Usename == null)
                    {
                        foreach (var x in request.CartItem)
                        {
                            var billDetail = new BillDetail
                            {
                                Id = Guid.NewGuid(),
                                Code = bill.InvoiceCode + RamdomString.GenerateRandomString(6),
                                CodeProductDetail = x.MaProductDetail,
                                Price = x.Price,
                                Quantity = x.Quantity,
                                BillId = bill.Id
                            };

                            await _billDetailRepository.CreateBillDetail(billDetail);
                        }

                    }
                    else
                    {
                        var cartItemToBill = cartItem.Where(x => x.Status == 1).ToList();
                        foreach (var cartItemDetail in cartItemToBill)
                        {
                            var billDetail = new BillDetail
                            {
                                Id = Guid.NewGuid(),
                                Code = bill.InvoiceCode + RamdomString.GenerateRandomString(6),
                                CodeProductDetail = cartItemDetail.MaProductDetail,
                                Price = cartItemDetail.Price,
                                Quantity = cartItemDetail.Quantity,
                                BillId = bill.Id
                            };

                            await _billDetailRepository.CreateBillDetail(billDetail);
                        }
                    }


                    return SuccessResponse(bill, $"{bill.InvoiceCode}");
                }
                else
                {
                    return ErrorResponse("Đặt hàng thất bại");
                }
            }
            catch (Exception e)
            {
                return ErrorResponse("Có lỗi gì đó: " + e.Message);
            }
        }

        private ResponseDto NotFoundResponse(string message)
        {
            return new ResponseDto
            {
                Result = null,
                IsSuccess = false,
                Code = 404,
                Message = message
            };
        }

        private ResponseDto SuccessResponse(Bill bill, string message)
        {
            return new ResponseDto
            {
                Result = bill,
                IsSuccess = true,
                Code = 200,
                Message = message
            };
        }

        private ResponseDto ErrorResponse(string message)
        {
            return new ResponseDto
            {
                Result = null,
                IsSuccess = false,
                Code = 400,
                Message = message
            };
        }

        public async Task<ResponseDto> PGetBillByInvoiceCode(string invoiceCode)
        {
            var billT = await _billRepository.GetBillByInvoiceCode(invoiceCode);
            if (billT != null)
            {
                var listBillDetail = await _billRepository.GetBillDetailByInvoiceCode(invoiceCode);
                _reponseBill.InvoiceCode = billT.InvoiceCode;
                _reponseBill.PhoneNumber = billT.PhoneNumber;
                _reponseBill.FullName = billT.FullName;
                _reponseBill.Address = billT.Address;
                _reponseBill.Status = billT.Status;
                _reponseBill.CreateDate = billT.CreateDate;
                _reponseBill.CodeVoucher = billT.CodeVoucher;
                _reponseBill.GiamGia = billT.GiamGia;
                _reponseBill.Payment = billT.Payment;
                _reponseBill.IsPayment = billT.IsPayment;
                _reponseBill.UserId = billT.UserId;
                _reponseBill.BillDetail = listBillDetail;
                _reponseBill.Count = listBillDetail.Count();
                _reponse.Message = $"Lấy hóa đơn của khách hàng {invoiceCode} thành công.";
                _reponse.Result = _reponseBill;
                return _reponse;
            }
            _reponse.Code = 404;
            _reponse.IsSuccess = false;
            _reponse.Message = $"Không tìm thấy hóa đơn của khách hàng {invoiceCode}.";
            return _reponse;
        }
        public async Task<ResponseDto> GetAllBill(string? phoneNumber)
        {
            _reponse.Count = _billRepository.GetAll().Result.Count();
            _reponse.Result = await _billRepository.GetAll();
            return _reponse;
        }

        public async Task<ResponseDto> GetBillDetailByInvoiceCode(string invoiceCode)
        {
            _reponseBill.Count = 1;
            _reponse.Message = $"thành công.";
            _reponse.Result = await _billRepository.GetBillDetailByInvoiceCode(invoiceCode);
            return _reponse;
        }
    }
}


