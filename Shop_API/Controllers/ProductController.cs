using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController( IProductRepository Ipr)
        {
            _productRepository = Ipr;   
        }

        [HttpGet("Get-All-Product")]

        public async Task<IActionResult> GetAllPro()
        {
            return Ok(await _productRepository.GetAll());
        }

        [HttpPost("Creat-Product")]

        public async Task<IActionResult> Create(Product obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _productRepository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }

        [HttpPut("Update-Product")]
        public async Task<IActionResult> Update(Product x)
        {
            if (await _productRepository.Update(x))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete("Delete-Product")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _productRepository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }
    }
}
