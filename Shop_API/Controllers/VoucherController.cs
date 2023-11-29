using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository _repository;
        private readonly IConfiguration _config;
        private readonly IPagingRepository _pagingRepository;
        private readonly ResponseDto _response;
        public VoucherController(IVoucherRepository repository, IConfiguration config, IPagingRepository pagingRepository)
        {
            _repository = repository;
            _config = config;
            _pagingRepository = pagingRepository;
            _response = new ResponseDto();
        }
        [HttpGet("GetAllVoucher")]
        public async Task<IActionResult> GetAllVoucher()
        {

            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
            return Ok(await _repository.GetAllVouchers());
        }
        [HttpPost("CreateVoucher")]
        public async Task<IActionResult> CreateVoucher([FromBody] Voucher voucher)
        {

            //string apiKey = _config.GetSection("ApiKey").Value;
            //if (apiKey == null)
            //{
            //    return Unauthorized();
            //}

            //var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            //if (keyDomain != apiKey.ToLower())
            //{
            //    return Unauthorized();
            //}
            //voucher.Id = Guid.NewGuid();
            //if (await _repository.Create(voucher))
            //{
            //    return Ok("Thêm thành công");
            //}
            return BadRequest("Thêm thất bại");
        }


        [HttpPost("CreateVoucher2")]
        public async Task<IActionResult> CreateVoucher2([FromBody] Voucher voucher)
        {
            voucher.Id = Guid.NewGuid();
            var response = await _repository.Create(voucher);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }



        [HttpPut("UpdateVoucher")]
        public async Task<IActionResult> UpdateVoucher(Voucher obj)
        {

            //string apiKey = _config.GetSection("ApiKey").Value;
            //if (apiKey == null)
            //{
            //    return Unauthorized();
            //}

            //var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            //if (keyDomain != apiKey.ToLower())
            //{
            //    return Unauthorized();
            //}
            var response = await _repository.Update(obj);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteRam(Guid id)
        {

            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                return Unauthorized();
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                return Unauthorized();
            }
            if (await _repository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }
        [HttpGet("GetAllVoucherPGs")]
        public IActionResult GetAllVoucherPGs(string? search, double? from, double? to, string? sortBy, int? page)
        {
            _response.Result = _pagingRepository.GetAllVoucherPG(search, from, to, sortBy, page);
            _response.Count = _pagingRepository.GetAllVoucherPG(search, from, to, sortBy, page).Count();
            return Ok(_response);
        }

        [HttpPut("DuyetVoucher")]
        public async Task<IActionResult> DuyetVoucher(Guid id)
        {
            if (await _repository.Duyet(id))
            {
                return Ok("DuyetVoucher thành công");
            }
            return BadRequest("DuyetVoucher thất bại");
         
        }
        [HttpPut("HuyDuyetVoucher")]
        public async Task<IActionResult> HuyDuyetVoucher(Guid id)
        {
            if (await _repository.HuyDuyet(id))
            {
                return Ok("HuydDuyetVoucher thành công");
            }
            return BadRequest("HuydDuyetVoucher thất bại");
        }
    }
}
