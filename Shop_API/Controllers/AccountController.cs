using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using Shop_API.Service.IService;
using Shop_Models.Dto;
using Shop_Models.Dto.Account;
using Shop_Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _userServiece;

        public AccountController(IAccountService userServiece)
        {
            _userServiece = userServiece;

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        
        {
            var result = await _userServiece.Validate(loginRequest);
            if (result == null)
            {
                return BadRequest("khong co access");
            }
            return Ok(result);
        }
        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(TokenDto tokenDTO)
        {
            var result = await _userServiece.RenewToken(tokenDTO);
            return Ok(result);
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto signUp)
        {
            var result = await _userServiece.SignUp(signUp);
            return Ok(result);
        }


    }
}
