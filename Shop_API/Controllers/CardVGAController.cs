using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardVGAController : ControllerBase
    {
        private readonly ICardVGARepository _repository;
        public CardVGAController(ICardVGARepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCardVGA()
        {
            return Ok(await _repository.GetAllCardVGA());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCardVGA(CardVGA obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _repository.Create(obj))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCardVGA(CardVGA obj)
        {
            if (await _repository.Update(obj))
            {
                return Ok("Sửa thành công");
            }
            return BadRequest("Sửa thất bại");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardVGA(Guid id)
        {
            if (await _repository.Delete(id))
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");
        }
    }
}
