using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuController : Controller
    {
        private readonly ICpuRepository _cpuRepository;

        public CpuController(ICpuRepository cpuRepository)
        {
            _cpuRepository = cpuRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCpu()
        {
            return Ok(await _cpuRepository.GetAllCpus());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCpu(Cpu obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _cpuRepository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCpu(Cpu obj)
        {
            if (await _cpuRepository.Update(obj))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCpu(Guid id)
        {
            if (await _cpuRepository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }
    }
}
