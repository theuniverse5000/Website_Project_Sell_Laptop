using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shop_API.Service.IService;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IConfiguration _config;
        public BillController(IConfiguration config,
            IBillService billService)
        {
            _config = config;
            _billService = billService;
        }
        [AllowAnonymous]
        [HttpGet("GetByByPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> GetBillByPhoneNumber(string phoneNumber)
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
            var result = await _billService.GetBillByPhoneNumber(phoneNumber);
            Log.Information("GetBill => {@_reponse}", result);
            return Ok(result);

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
