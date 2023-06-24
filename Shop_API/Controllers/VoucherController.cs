using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository _repository;
        public VoucherController(IVoucherRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> GetAllVoucher()
        {
            return Ok(await _repository.GetAllImage());
        }
        public async Task<IActionResult> CreateVoucher(Voucher voucher)
        {
            voucher.Id= Guid.NewGuid();
            if(await _repository.Create(voucher))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateVoucher(Voucher obj)
        {
            if (await _repository.Update(obj))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteRam(Guid id)
        {
            if (await _repository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }

    }
}
