using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _repository;

        public ImagesController(IImageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return Ok(await _repository.GetAllImage());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateImage( Image image)
        {
            if(await _repository.Update(image))
            {
                return Ok("Sửa thành công");
               
            }
            return BadRequest("Sửa thất bại");
        }

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Image>> CreateImage(Image image)
        {
          image.Id= Guid.NewGuid();
          if(await _repository.Create(image))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
           if(await _repository.Delete(id)) {

                return Ok("Xóa thành công");
           }
            return BadRequest("Xóa thất bại");
        }

      
    }
}
