using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailController : ControllerBase
    {
        private readonly IBillDetailRepository _billDetailRepository;
        public BillDetailController(IBillDetailRepository billDetailRepository)
        {
            _billDetailRepository = billDetailRepository;
        }

        [AllowAnonymous]
        [HttpGet("GetAllBillDetail")]
        public async Task<IActionResult> GetAllBillDetail()
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
            //var result = await _billDetailRepository.GetAllBillDetails();

            var result = await _billDetailRepository.GetAllBillDetails();


            if (result!=null)
            {
                return Ok(result);
            }else { return BadRequest(result); }


        }
    }
}
