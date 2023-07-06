using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly ReponseDto _reponse;
        private static Guid getUserId;  // Tạo 1 biết static phạm vi private dùng trong controller
        public CartController(ICartRepository cartRepository, ICartDetailRepository cartDetailRepository,
            IProductDetailRepository productDetailRepository, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _cartDetailRepository = cartDetailRepository;
            _productDetailRepository = productDetailRepository;
            _userRepository = userRepository;
            _reponse = new ReponseDto();
        }
        [HttpGet("GetAllCarts"), Authorize]
        public async Task<ReponseDto> GetAllCarts()
        {
            var cartItem = await _cartRepository.GetAll();
            if (cartItem == null)
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = "Lỗi";
                return _reponse;
            }
            else
            {
                _reponse.Result = cartItem;
                _reponse.Code = 200;
                return _reponse;
            }

        }
        [HttpGet("GetCartByUsername")]
        public async Task<ReponseDto> GetCartByUsername(string username)
        {
            var cartItem = await _cartRepository.GetCartByUsername(username);
            if (cartItem == null)
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = "Lỗi";
                return _reponse;
            }
            else
            {
                _reponse.Result = cartItem;
                _reponse.Code = 200;
                return _reponse;
            }

        }
        [HttpGet("GetCartJoinForUser"), AllowAnonymous]
        public async Task<ReponseDto> GetCartJoinForUser(string username)
        {
            var cartItem = await _cartRepository.GetCartItem(username);
            if (cartItem == null)
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = "Lỗi";
                return _reponse;
            }
            else
            {
                _reponse.Result = cartItem;
                _reponse.Code = 200;
                return _reponse;
            }

        }
        //[HttpGet("GetAllCartJoin")]
        //public async Task<ReponseDto> GetAllCartJoin()
        //{
        //    var cartItem = await _cartRepository.GetAllCarts();
        //    if (cartItem == null)
        //    {
        //        _reponse.Result = null;
        //        _reponse.IsSuccess = false;
        //        _reponse.Code = 404;
        //        _reponse.Message = "Lỗi";
        //        return _reponse;
        //    }
        //    else
        //    {
        //        _reponse.Result = cartItem;
        //        _reponse.Code = 200;
        //        return _reponse;
        //    }

        //}
        [HttpPost("AddCart"), AllowAnonymous]
        public async Task<ReponseDto> AddCart(string username, string maProductDetail)
        {
            try
            {
                Guid idProductDetail = _productDetailRepository.GetAll().Result.FirstOrDefault(x => x.Ma == maProductDetail).Id;
                // Bước 1: Khi truyền vào username lấy ra được id của user
                getUserId = _userRepository.GetAllUsers().Result.FirstOrDefault(x => x.Username == username).Id;
                var userCart = _cartRepository.GetAll().Result.FirstOrDefault(x => x.UserId == getUserId);// Tìm cart của người dùng
                var productDetailX = _productDetailRepository.GetAll().Result.Where(x => x.Id == idProductDetail).ToList();// Kiểm tra sản phẩm trong db
                int soLuongProductDetail = productDetailX.FirstOrDefault().AvailableQuantity;// Lấy số lượng giỏ hàng
                if (soLuongProductDetail <= 0)// Check xem còn hàng không
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Số lượng sản phẩm không đủ";
                    return _reponse;
                }
                if (userCart != null)// TH người dùng đã có giỏ hàng => trỏ đến giỏ hàng đó thực hiện
                {
                    var checkProductDetailInCart = _cartDetailRepository.GetAll().Result.FirstOrDefault(a => a.CartId == getUserId && a.ProductDetailId == idProductDetail);
                    if (checkProductDetailInCart != null)
                    {
                        CartDetail a = new CartDetail();
                        a.Id = checkProductDetailInCart.Id;
                        a.Quantity = checkProductDetailInCart.Quantity += 1;
                        if (await _cartDetailRepository.Update(a))
                        {
                            _reponse.Result = a;
                            _reponse.IsSuccess = true;
                            _reponse.Code = 200;
                            return _reponse;
                        }
                        _reponse.Result = null;
                        _reponse.IsSuccess = false;
                        _reponse.Code = 404;
                        _reponse.Message = "Không thể cập nhật số lượng sản phẩm trong giỏ hàng";
                        return _reponse;
                    }
                    else
                    {
                        CartDetail x = new CartDetail();
                        x.Id = new Guid();
                        x.ProductDetailId = idProductDetail;
                        x.CartId = getUserId;
                        x.Quantity = 1;
                        if (await _cartDetailRepository.Create(x))
                        {
                            _reponse.Result = x;
                            _reponse.IsSuccess = true;
                            _reponse.Code = 201;
                            _reponse.Message = "Thêm sản phẩm vào giỏ hàng thành công";
                            return _reponse;
                        }
                        _reponse.Result = null;
                        _reponse.IsSuccess = false;
                        _reponse.Code = 404;
                        _reponse.Message = "Không thể thêm sản phẩm vào trong giỏ hàng";
                        return _reponse;
                    }
                }
                else // TH người dùng chưa có giỏ hàng
                {
                    Cart cart = new Cart(); // Khởi tạo 1 giỏ hàng mới
                    cart.UserId = getUserId;// Id là id của người dùng lấy được thông qua username
                    cart.Description = $"Giỏ hàng của {username}";
                    if (await _cartRepository.Create(cart))// Thêm giỏ hàng vào db nếu thành công thì tiếp tục
                    {
                        CartDetail b = new CartDetail();// Khởi tạo giỏ hàng chi tiết
                        b.Id = new Guid();// id tự sinh
                        b.ProductDetailId = idProductDetail;// id sản phẩm lấy được khi truyền vào mã ctsp
                        b.CartId = getUserId;// Id giỏ hàng = id user
                        b.Quantity = 1;// Thêm vào giỏ hàng số lượng mặc định là 1
                        if (await _cartDetailRepository.Create(b))// Thêm ctsp vào giỏ hàng chi tiết
                        {
                            _reponse.Result = b;// trả về result là 1 chuỗi obj dạng json
                            _reponse.IsSuccess = true;
                            _reponse.Code = 201;
                            _reponse.Message = "Thêm sản phẩm vào giỏ hàng thành công";
                            return _reponse;// Trả về reponse
                        }
                        else
                        {
                            _reponse.Result = null;
                            _reponse.IsSuccess = false;
                            _reponse.Code = 404;
                            _reponse.Message = "Không thể thêm chi tiết sản phẩm vào giỏ hàng chi tiết";
                            return _reponse;
                        }

                    }
                    // Nếu tạo thất bại trả về lỗi
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không thể tạo giỏ hàng";
                    return _reponse;
                }
            }
            catch (Exception e)
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = e.Message;
                return _reponse;
            }

        }
        [HttpPut("CongQuantity"), AllowAnonymous]
        public async Task<ReponseDto> CongQuantityCartDetail(Guid idCartDetail)
        {
            try
            {
                var checkProductDetailInCart = _cartDetailRepository.GetAll().Result.FirstOrDefault(x => x.Id == idCartDetail).Quantity;
                CartDetail cartDetail = new CartDetail();
                cartDetail.Id = idCartDetail;
                cartDetail.Quantity = (checkProductDetailInCart += 1);
                if (await _cartDetailRepository.Update(cartDetail))
                {
                    _reponse.Result = cartDetail;
                    _reponse.IsSuccess = true;
                    _reponse.Code = 200;
                    _reponse.Message = "Tăng số lượng sản phẩm thành công";
                    return _reponse;
                }
                else
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Thất bại";
                    return _reponse;
                }
            }
            catch (Exception e)
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = e.Message;
                return _reponse;
            }
        }
        [HttpPut("TruQuantityCartDetail"), AllowAnonymous]
        public async Task<ReponseDto> TruQuantityCartDetail(Guid idCartDetail)
        {
            try
            {
                var checkProductDetailInCart = _cartDetailRepository.GetAll().Result.FirstOrDefault(x => x.Id == idCartDetail).Quantity;
                if (checkProductDetailInCart > 0)
                {
                    CartDetail cartDetail = new CartDetail();
                    cartDetail.Id = idCartDetail;
                    cartDetail.Quantity = (checkProductDetailInCart -= 1);
                    if (await _cartDetailRepository.Update(cartDetail))
                    {
                        _reponse.Result = cartDetail;
                        _reponse.IsSuccess = true;
                        _reponse.Code = 200;
                        _reponse.Message = "Giảm số lượng sản phẩm thành công";
                        return _reponse;
                    }
                    else
                    {
                        _reponse.Result = null;
                        _reponse.IsSuccess = false;
                        _reponse.Code = 404;
                        _reponse.Message = "Thất bại";
                        return _reponse;
                    }
                }
                else
                {
                    if (await _cartDetailRepository.Delete(idCartDetail))
                    {
                        _reponse.Result = null;
                        _reponse.IsSuccess = true;
                        _reponse.Code = 204;
                        _reponse.Message = "Bạn vừa xóa sản phẩm khỏi giỏ hàng";
                        return _reponse;
                    }
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Thất bại, có lỗi gì đó";
                    return _reponse;
                }

            }
            catch (Exception e)
            {
                _reponse.Result = null;
                _reponse.IsSuccess = false;
                _reponse.Code = 404;
                _reponse.Message = e.Message;
                return _reponse;
            }
        }
    }
}
