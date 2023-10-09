using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Service.IService;
using Shop_Models.Dto;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ResponseDto _reponse;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
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
        [HttpGet("GetCartByUsername")]
        public async Task<IActionResult> GetCartByUsername(string username)
        {
            var reponse = await _cartService.GetCartByUsername(username);
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
        public async Task<IActionResult> GetCartJoinForUser(string username)
        {
            var reponse = await _cartService.GetCartJoinForUser(username);
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
        public async Task<IActionResult> AddCart(string username, string codeProductDetail)
        {
            var reponse = await _cartService.AddCart(username, codeProductDetail);
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
    }
}
