using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly ResponseDto _reponse;
        private static Guid getUserId;  // Tạo 1 biết static phạm vi private dùng trong controller
        /*
          Các trạng thái của cart detail
         1: Trạng thái mặc định khi thêm sản phẩm vào giỏ hàng
         2: Trạng thái chờ của sản phẩm khi thêm vào hóa đơn chi tiết
         0: Trạng thái ẩn sản phẩm trong giỏ hàng
         */
        public CartService(ICartRepository cartRepository, ICartDetailRepository cartDetailRepository,
            IProductDetailRepository productDetailRepository, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _cartDetailRepository = cartDetailRepository;
            _productDetailRepository = productDetailRepository;
            _userRepository = userRepository;
            _reponse = new ResponseDto();
        }
        public async Task<ResponseDto> AddCart(Guid userId, string userName, string codeProductDetail)
        {
            try
            {

                var productDetailToCart = _productDetailRepository.PGetProductDetail(1, codeProductDetail, null, null, null, null, 1).Result.FirstOrDefault();
                var userToCart = _userRepository.GetAllUsers().Result;//.FirstOrDefault(x => x.UserName == username);
                if (userToCart == null)
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không tìm thấy tài khoản";
                    return _reponse;
                }
                if (productDetailToCart == null)
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không tìm thấy sản phẩm";
                    return _reponse;
                }
                // Bước 1: Khi truyền vào username lấy ra được id của user

                var userCart = _cartRepository.GetAll().Result.FirstOrDefault(x => x.UserId == userId);
                int soLuongProductDetail = 1;// productDetailToCart.AvailableQuantity;
                if (soLuongProductDetail <= 0)
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Số lượng sản phẩm không đủ";
                    return _reponse;
                }
                if (userCart != null)
                {
                    var checkProductDetailInCart = _cartDetailRepository.GetAll().Result.FirstOrDefault(a => a.CartId == getUserId && a.ProductDetailId == productDetailToCart.Id);
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
                        x.Id = Guid.NewGuid();
                        x.ProductDetailId = productDetailToCart.Id;
                        x.CartId = userId;
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
                else
                {
                    Cart cart = new Cart();
                    cart.UserId = getUserId;
                    cart.Description = $"Giỏ hàng của {userName}";
                    if (await _cartRepository.Create(cart))
                    {
                        CartDetail b = new CartDetail();
                        b.Id = new Guid();
                        b.ProductDetailId = productDetailToCart.Id;
                        b.CartId = getUserId;
                        b.Quantity = 1;
                        b.Status = 1;
                        if (await _cartDetailRepository.Create(b))

                        {
                            _reponse.Result = b;
                            _reponse.IsSuccess = true;
                            _reponse.Code = 201;
                            _reponse.Message = "Thêm sản phẩm vào giỏ hàng thành công";
                            return _reponse;
                        }
                    }
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không thể thêm sản phẩm vào trong giỏ hàng";
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
        public async Task<ResponseDto> CongQuantityCartDetail(Guid idCartDetail,Guid idProductDetail)
        {
            try
            {
                var cartDetailX = _cartDetailRepository.GetAll().Result.FirstOrDefault(x => x.Id == idCartDetail && x.ProductDetailId==idProductDetail);
                if (cartDetailX == null)
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không tìm thấy giỏ hàng chi tiết";
                    return _reponse;
                }
                var checkProductDetailInCart = cartDetailX.Quantity;
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
        public async Task<ResponseDto> TruQuantityCartDetail(Guid idCartDetail, Guid iDProductDetail)
        {
            try
            {
                var cartDetailX = _cartDetailRepository.GetAll().Result.FirstOrDefault(x => x.Id == idCartDetail&& x.ProductDetailId==iDProductDetail);
                if (cartDetailX == null)
                {
                    _reponse.Result = null;
                    _reponse.IsSuccess = false;
                    _reponse.Code = 404;
                    _reponse.Message = "Không tìm thấy giỏ hàng chi tiết";
                    return _reponse;
                }
                var checkProductDetailInCart = cartDetailX.Quantity;
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
        public async Task<ResponseDto> GetAllCarts()
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
        public async Task<ResponseDto> GetCartByUsername(string username)
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
        public async Task<ResponseDto> GetCartJoinForUser(string username)
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
    }
}
