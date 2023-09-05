using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Models.Entities;
using UploadApi.Services.IServices;

namespace UploadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesServies _imageUploadService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImagesController(IImagesServies imageUploadService, IWebHostEnvironment hostingEnvironment)
        {
            _imageUploadService = imageUploadService;
            _hostingEnvironment = hostingEnvironment;
        }



        [HttpPost]
        [Route("anh")]
        public async Task<IActionResult> taianh(IFormFile file, string objectType, Guid? objectId, string imageCode)
        {
            if (file == null)
            {
                return BadRequest("Vui lòng chọn một tệp để tải lên và lưu.");
            }

            string objectFolder = null;
            bool saveToDb = false;

            switch (objectType)
            {
                case "a":
                    objectFolder = "product_images";
                    saveToDb = true;
                    break;
                case "b":
                    objectFolder = "user_images";
                    saveToDb = false;
                    break;
                case "c":
                    objectFolder = "bai_dang_images";
                    saveToDb = true;
                    break;
                default:
                    return BadRequest("Loại đối tượng không hợp lệ.");
            }

            string basePath = _hostingEnvironment.WebRootPath; // Đường dẫn gốc của thư mục wwwroot
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(basePath, objectFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (saveToDb)
            {
                var image = new Image
                {
                    Id = Guid.NewGuid(),
                    Ma = imageCode, // Mã của ảnh
                    LinkImage = $"/{objectFolder}/{fileName}", // Đường dẫn tương đối đến ảnh
                    ProductDetailId = (Guid)objectId, // Đối tượng ProductDetail
                    Status = 1 // Trạng thái ảnh
                };

                await _imageUploadService.SaveImageAsync(image);
            }
            if (objectType == "c")
            {

            }

            return Ok("Tải lên và lưu thành công.");
        }



    }
}
