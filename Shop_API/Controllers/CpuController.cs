using Azure;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuController : Controller
    {
        private readonly ICpuRepository _cpuRepository;
        private readonly IConfiguration _config;
        private readonly ResponseDto _reponse;
        private readonly IPagingRepository _iPagingRepository;
        public CpuController(ICpuRepository cpuRepository, IConfiguration config, IPagingRepository iPagingRepository)
        {
            _cpuRepository = cpuRepository;
            _config = config; _reponse = new ResponseDto();
            _iPagingRepository = iPagingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCpu()
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
            return Ok(await _cpuRepository.GetAllCpus());
        }
        [HttpPost("CreateCpu")]
        public async Task<IActionResult> CreateCpu(Cpu obj)
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
            obj.Id = Guid.NewGuid();
            obj.TrangThai = 1;
            if (await _cpuRepository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCpu(Cpu obj)
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
            if (await _cpuRepository.Update(obj))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCpu(Guid id)
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
            if (await _cpuRepository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");

        }

        [HttpGet("GetCpuById")]
        public async Task<IActionResult> GetCpuById(Guid guid)
        {

            //    string apiKey = _config.GetSection("ApiKey").Value;
            //    if (apiKey == null)
            //    {
            //        return Unauthorized();
            //    }

            //    var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            //    if (keyDomain != apiKey.ToLower())
            //    {
            //        return Unauthorized();
            //    }
            _reponse.Result = await _cpuRepository.GetById(guid);
            return Ok(_reponse);
        }

        [HttpGet("GetCPUFSP")]
        public async Task<IActionResult> GetCPUFSP(string? search, double? from, double? to, string? sortBy, int page)
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
            _reponse.Result = _iPagingRepository.GetAllCpu(search, from, to, sortBy, page);
            _reponse.Count = _iPagingRepository.GetAllCpu(search, from, to, sortBy, page).Count;
            return Ok(_reponse);
        }
    }
}
