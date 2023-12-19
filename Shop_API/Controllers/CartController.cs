using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_Models.Dto;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ResponseDto _reponse;
        public CartController(ICartService cartService, ICartDetailRepository cartDetailRepository, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _cartService = cartService;
            _cartDetailRepository = cartDetailRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _reponse = new ResponseDto();
        }
        [AllowAnonymous]// For admin
        [HttpGet("GetAllCarts")]
        public async Task<IActionResult> GetAllCarts()
        {
            var reponse = await _cartService.GetAllCarts();
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }
        }
        [AllowAnonymous]// For admin
        [HttpGet("GetCartById")]
        public async Task<IActionResult> GetCartById(Guid id)
        {
            var username = User.Identity.Name;
            var reponse = await _cartService.GetCartById(id);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }

        }
        [AllowAnonymous]// For client
        [HttpGet("GetCartJoinForUser")]
        public async Task<IActionResult> GetCartJoinForUser(string userName)
        {
            //var (userId, userName) = TokenUtility.GetTokenInfor(Request);
            var reponse = await _cartService.GetCartJoinForUser(userName);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }
        }
        //[HttpGet("GetAllCartJoin")]
        //public async Task<ResponseDto> GetAllCartJoin()
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
        [AllowAnonymous]// For client
        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart(string userName, string codeProductDetail)
        {
            var reponse = await _cartService.AddCart(userName, codeProductDetail);
            if (reponse.IsSuccess)
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest("Error");
            }

        }
        [AllowAnonymous]// For client
        [HttpPut("CongQuantity")]
        public async Task<IActionResult> CongQuantityCartDetail(Guid idCartDetail)
        {
            var reponse = await _cartService.CongQuantityCartDetail(idCartDetail);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }
        }
        [AllowAnonymous]
        [HttpPut("TruQuantityCartDetail")]
        public async Task<IActionResult> TruQuantityCartDetail(Guid idCartDetail)
        {
            var reponse = await _cartService.TruQuantityCartDetail(idCartDetail);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }
        }
        [AllowAnonymous]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string username)
        {
            var userId = _userRepository.GetAllUsers().Result.FirstOrDefault(x => x.UserName == username).Id;

            if (await _cartRepository.Delete(userId))
                return Ok();
            return BadRequest();

        }
    }
}
