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
			// Tạo thư mục lưu trữ nếu nó không tồn tại
			Directory.CreateDirectory(_uploadFolder);
		}



		[HttpPost]
		[Route("TaiAnh")]
		public async Task<IActionResult> TaiAnh(IFormFile file, string objectType, Guid objectId, string imageCode)
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

			if (objectId == Guid.Empty)
			{

				return BadRequest("Thiếu sản phẩm chi tiết được chọn để thêm ảnh");
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


		// UpdateImages By Name Of file in wwwroot

		[HttpPut]
		[Route("update/{imageName}")]
		public IActionResult UpdateImage(string imageName, string objectType,/* [FromForm]*/ IFormFile file)
		{
			try
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
																   //string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
				string filePath = Path.Combine(basePath, objectFolder, imageName);



				if (!System.IO.File.Exists(filePath))
				{
					return NotFound("Không tìm thấy tệp ảnh.");
				}

				if (System.IO.File.Exists(filePath))
				{
					System.IO.File.Create(filePath);
					return Ok("chỉnh tệp ảnh thành công.");
				}




				// Xóa tệp ảnh cũ
				System.IO.File.Delete(filePath);

				// Lưu tệp ảnh mới
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(stream);
				}

				return Ok(new { Message = "Cập nhật ảnh thành công.", ImagePath = filePath });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi: {ex.Message}");
			}
		}


		[HttpGet]
		[Route("get-images-by-object-type/{objectType}")]
		public IActionResult GetImagesByObjectType(string objectType)
		{
			try
			{
				// Xác định thư mục lưu trữ dựa trên objectType
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
				string objectFolderPath = Path.Combine(basePath, objectFolder);

				// Kiểm tra xem thư mục tồn tại
				if (!Directory.Exists(objectFolderPath))
				{
					return NotFound("Không có ảnh nào được tìm thấy cho đối tượng này.");
				}

				// Lấy danh sách tên tệp ảnh trong thư mục
				string[] imageFileNames = Directory.GetFiles(objectFolderPath).Select(Path.GetFileName).ToArray();

				// Trả về danh sách tên tệp ảnh
				return Ok(imageFileNames);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi: {ex.Message}");
			}
		}




		[HttpGet]
		[Route("find-image/{objectType}/{imageName}")] // có cả đuôi png
		public IActionResult FindImage(string objectType, string imageName)
		{
			try
			{
				// Xác định thư mục lưu trữ dựa trên objectType
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
				string objectFolderPath = Path.Combine(basePath, objectFolder);
				string imagePath = Path.Combine(objectFolderPath, imageName);

				// Kiểm tra xem tệp ảnh có tồn tại không
				if (!System.IO.File.Exists(imagePath))
				{
					return NotFound("Không tìm thấy ảnh.");
				}

				// Trả về tệp ảnh dưới dạng một phản hồi tệp
				return PhysicalFile(imagePath, "image/jpeg"); // Điều chỉnh kiểu tệp tùy theo định dạng thực tế của tệp ảnh
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi: {ex.Message}");
			}
		}

		private readonly string _uploadFolder = "uploads"; // Thư mục lưu trữ tệp ảnh


		[HttpPost]
		[Route("uploadManyImages")]
		public async Task<IActionResult> UploadImages(List<IFormFile> files)
		{
			try
			{
				var uploadedFiles = new List<string>();

				foreach (var file in files)
				{
					if (file != null && file.Length > 0)
					{
						// Tạo tên tệp duy nhất bằng cách sử dụng Guid và đuôi mở rộng
						string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
						string filePath = Path.Combine(_uploadFolder, fileName);

						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}

						uploadedFiles.Add(fileName);
					}
				}

				return Ok(new { Message = "Tải lên thành công", Files = uploadedFiles });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi: {ex.Message}");
			}
		}

		[HttpPost]
		[Route("uploadManyImageszszs")]
		public async Task<IActionResult> UploadImagezzs(List<IFormFile> files)
		{
			try
			{
				var uploadedFiles = new List<string>();

				foreach (var file in files)
				{
					if (file != null && file.Length > 0)
					{
						// Tạo thư mục "allPhotoUploaded" nếu nó không tồn tại
						string uploadFolder = "allPhotoUploaded";
						string folderPath = Path.Combine(_hostingEnvironment.WebRootPath, uploadFolder);

						if (!Directory.Exists(folderPath))
						{
							Directory.CreateDirectory(folderPath);
						}

						// Tạo tên tệp duy nhất bằng cách sử dụng Guid và đuôi mở rộng
						string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
						string filePath = Path.Combine(folderPath, fileName);

						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}

						uploadedFiles.Add(fileName);
					}
				}

				return Ok(new { Message = "Tải lên thành công", Files = uploadedFiles });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi: {ex.Message}");
			}
		}

	}
}
