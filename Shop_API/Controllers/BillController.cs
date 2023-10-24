using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IBillRepository _billRepository;
        private readonly IConfiguration _config;
        public BillController(IConfiguration config,
            IBillService billService, IBillRepository billRepository)
        {
            _config = config;
            _billService = billService;
            _billRepository = billRepository;
        }
        [AllowAnonymous]
        [HttpGet("GetBillByInvoiceCodeP/{invoiceCode}")]
        public async Task<IActionResult> GetBillByInvoiceCodeP(string invoiceCode)
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
            var result = await _billService.GetBillByInvoiceCode(invoiceCode);
            Log.Information("GetBill => {@_reponse}", result);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
            // }

        }
        [AllowAnonymous]
        [HttpGet("GetListBill")]
        public async Task<IActionResult> GetListBill(string? phoneNumber)
        {

            return Ok(await _billService.GetAllBill(phoneNumber));
        }
        [AllowAnonymous]
        [HttpGet("GetBillDetailByInvoiceCode")]
        public async Task<IActionResult> GetBillDetail(string invoiceCode)
        {

            return Ok(await _billService.GetBillDetailByInvoiceCode(invoiceCode));
        }
        [AllowAnonymous]
        [HttpPost("CreateBill")]
        public async Task<IActionResult> CreateBill(string username, string maVoucher)
        {
            return Ok(await _billService.CreateBill(username, maVoucher));
        }
        [AllowAnonymous]
        [HttpPut("UpdateBill")]
        public async Task<IActionResult> UpdateBill(Bill bill)
        {
            if (await _billRepository.Update(bill))
            {
                return Ok($"Cập nhật hóa đơn {bill.InvoiceCode} thành công");
            }
            return BadRequest("Cập nhật thất bại");
        }
    }
}
