using Microsoft.AspNetCore.Mvc;
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
		[Route("taianh")]
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
					saveToDb = false;
					break;
				case "d":
					objectFolder = "card_VGA_images";
					saveToDb = false;
					break;
				case "e":
					objectFolder = "hard_drive_images";
					saveToDb = false;
					break;
				case "f":
					objectFolder = "manufacturer_images"; saveToDb = false;
					break;
				case "g":
					objectFolder = "ram_images"; saveToDb = false;
					break;
				case "h":
					objectFolder = "screen_images"; saveToDb = false;
					break;
				case "i":
					objectFolder = "voucher_images"; saveToDb = false;
					break;

				default:
					return BadRequest("Loại đối tượng không hợp lệ.");
			}

			string basePath = _hostingEnvironment.WebRootPath; // Đường dẫn gốc của thư mục wwwroot
			string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
			string filePath = Path.Combine(basePath, objectFolder, fileName);

			// Kiểm tra định dạng tệp ảnh
			string fileExtension = Path.GetExtension(file.FileName).ToLower();
			if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".gif")
			{
				return BadRequest("Vui lòng tải lên một tệp ảnh hợp lệ.");
			}

			// Giới hạn kích thước tệp
			long maxFileSizeInBytes = 5 * 1024 * 1024; // Giới hạn kích thước tệp là 5 MB
			if (file.Length > maxFileSizeInBytes)
			{
				return BadRequest("Kích thước tệp quá lớn. Vui lòng tải lên một tệp nhỏ hơn.");
			}

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

			return Ok("Tải lên và lưu thành công.");
		}


		[HttpDelete]
		[Route("XoaAnh")]
		public IActionResult XoaAnh(string objectType, string imageName)
		{
			if (string.IsNullOrEmpty(objectType) || string.IsNullOrEmpty(imageName))
			{
				return BadRequest("Thông tin không hợp lệ.");
			}

			string objectFolder = null;

			switch (objectType)
			{
				case "a":
					objectFolder = "product_images";
					break;
				case "b":
					objectFolder = "user_images";
					break;
				case "c":
					objectFolder = "bai_dang_images";
					break;
				case "d":
					objectFolder = "card_VGA_images";
					break;
				case "e":
					objectFolder = "hard_drive_images";
					break;
				case "f":
					objectFolder = "manufacturer_images";
					break;
				case "g":
					objectFolder = "ram_images";
					break;
				case "h":
					objectFolder = "screen_images";
					break;
				case "i":
					objectFolder = "voucher_images";
					break;
				default:
					return BadRequest("Loại đối tượng không hợp lệ.");
			}

			string basePath = _hostingEnvironment.WebRootPath; // Đường dẫn gốc của thư mục wwwroot
			string filePath = Path.Combine(basePath, objectFolder, imageName);

			if (System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
				return Ok("Xóa tệp ảnh thành công.");
			}
			else
			{
				return BadRequest("Không tìm thấy tệp ảnh để xóa.");
			}
		}







	}
}
