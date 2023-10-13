using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardVGAController : ControllerBase
    {
        private readonly ICardVGARepository _repository;
        private readonly IConfiguration _config;
        private readonly IPagingRepository _iPagingRepository;
        private readonly ResponseDto _reponse;
        public CardVGAController(ICardVGARepository repository, IConfiguration config, IPagingRepository pagingRepository)
        {
            _repository = repository;
            _config = config;
            _iPagingRepository = pagingRepository;
            _reponse = new ResponseDto();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCardVGA()
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
            return Ok(await _repository.GetAllCardVGA());
        }
        [HttpPost("CreateCardVGA")]
        public async Task<IActionResult> CreateCardVGA(CardVGA obj)
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
            if (await _repository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCardVGA(CardVGA obj)
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
            if (await _repository.Update(obj))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardVGA(Guid id)
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

        [HttpGet("GetCardVGAFSP")]
        public IActionResult GetCardVGAFSP(string? search, double? from, double? to, string? sortBy, int page)
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
            _reponse.Result = _iPagingRepository.GetAllCardVGA(search, from, to, sortBy, page);
            var count = _reponse.Count = _iPagingRepository.GetAllCardVGA(search, from, to, sortBy, page).Count;
            return Ok(_reponse);
        }

    }
}
