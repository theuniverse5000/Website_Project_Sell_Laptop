using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_API.Services.IServices;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _repository;
        private readonly IConfiguration _config;

        private readonly IImagesServies _imageUploadService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;


        public ImagesController(IImageRepository repository, IConfiguration config, IImagesServies imageUploadService, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _repository = repository;
            _config = config;

            _imageUploadService = imageUploadService;
            _hostingEnvironment = hostingEnvironment;
            // Tạo thư mục lưu trữ nếu nó không tồn tại
            Directory.CreateDirectory(_uploadFolder);
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
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
            return Ok(await _repository.GetAllImage());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateImage(Image image)
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
            if (await _repository.Update(image))
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
            image.Id = Guid.NewGuid();
            if (await _repository.Create(image))
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
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



        [HttpPost]
        [Route("TaiAnh")]
        public async Task<IActionResult> TaiAnh(IFormFile file, string objectType, Guid? objectId, string imageCode)
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

            //if (objectId == Guid.Empty)
            //{

            //	return BadRequest("Thiếu sản phẩm chi tiết được chọn để thêm ảnh");
            //}
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










        [HttpPost]
        [Route("test")]
        public async Task<IActionResult> test(IFormFile? file, string objectType, Guid? objectId, string imageCode)
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

            string basePath = _hostingEnvironment.WebRootPath;
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(basePath, objectFolder, fileName);

            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Vui lòng tải lên một tệp ảnh hợp lệ.");
            }

            long maxFileSizeInBytes = 5 * 1024 * 1024;
            if (file.Length > maxFileSizeInBytes)
            {
                return BadRequest("Kích thước tệp quá lớn. Vui lòng tải lên một tệp nhỏ hơn.");
            }
            var spct = await _context.ProductDetails.FindAsync(objectId);
            if (objectType == "a" && spct == null)
            {
                return BadRequest("Thiếu sản phẩm chi tiết được chọn để thêm ảnh");

            }
            else if (objectType == "a" || spct != null)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            }
            else if (objectType != "a" && (objectId == null || objectId == Guid.Empty))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }




            if (saveToDb)
            {
                var image = new Image
                {
                    Id = Guid.NewGuid(),
                    Ma = imageCode,
                    LinkImage = $"/{objectFolder}/{fileName}",
                    ProductDetailId = (Guid)objectId,
                    Status = 1
                };

                // Lưu ảnh vào cơ sở dữ liệu ở đây (sử dụng dịch vụ _imageUploadService)
                await _imageUploadService.SaveImageAsync(image);
            }

            return Ok(new { message = "Tải lên và lưu thành công." });
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

            var image = _context.Images.FirstOrDefault(x => x.LinkImage == $"/{objectFolder}/{imageName}");
            // tìm ảnh theo tên 
            //var imagesByName = _context.Images.FirstOrDefault(x=>x.Ma==);
            if (System.IO.File.Exists(filePath) && image != null)
            {
                _context.Images.Remove(image);
                _context.SaveChanges();
                System.IO.File.Delete(filePath);
                return Ok("Xóa tệp ảnh thành công.");
            }
            else if (System.IO.File.Exists(filePath) && image == null)
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
        public async Task<IActionResult> UpdateImage(string imageName, string objectType,/* [FromForm]*/ IFormFile file)
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


                string _newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string NewfilePath = Path.Combine(basePath, objectFolder, _newFileName);

                var image = _context.Images.FirstOrDefault(x => x.LinkImage == $"/{objectFolder}/{imageName}");
                // tìm ảnh theo tên 

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Không tìm thấy tệp ảnh.");
                }

                if (System.IO.File.Exists(filePath) && image != null && objectType == "a")
                {
                    image.LinkImage = $"/{objectFolder}/{_newFileName}";


                    // Lưu ảnh vào cơ sở dữ liệu ở đây (sử dụng dịch vụ _imageUploadService)

                    _context.Images.Update(image);
                    await _context.SaveChangesAsync();

                    // Xóa tệp ảnh cũ
                    System.IO.File.Delete(filePath);

                    // Lưu tệp ảnh mới
                    using (var stream = new FileStream(NewfilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    //System.IO.File.Create(NewfilePath);
                    //return Ok("chỉnh tệp ảnh thành công.");
                }
                else
                {
                    System.IO.File.Delete(filePath);

                    // Lưu tệp ảnh mới
                    using (var stream = new FileStream(NewfilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }






                return Ok(new { Message = "Cập nhật ảnh thành công.", ImagePath = NewfilePath });
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
                    // (Thêm các case tương tự như trước)
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
                string[] imageFileNames = Directory.GetFiles(objectFolderPath);

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
        [Route("uploadManyProductDetailImages")] // NO NO
        public async Task<IActionResult> UploadImages([FromForm] List<IFormFile> files, [FromForm] Guid ProductId)
        {
            var objectFolder = "product_images";
             //var ProductId = Guid.Empty;
            try
            {
                var uploadedFiles = new List<string>();
                var spct = await _context.ProductDetails.FindAsync(ProductId);

                if (spct != null)
                {
                    int i = 1;
                    foreach (var file in files)
                    {
                        if (file != null && file.Length > 0)
                        {
                            string basePath = _hostingEnvironment.WebRootPath;
                            // Tạo tên tệp duy nhất bằng cách sử dụng Guid và đuôi mở rộng
                            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                            //string filePath = Path.Combine(basePath, fileName);

                            
                         
                            string filePath = Path.Combine(basePath, objectFolder, fileName);


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            uploadedFiles.Add(fileName);

                           

                            var image = new Image
                            {
                                Id = Guid.NewGuid(),
                                Ma = "Anh" + i++,
                                LinkImage = $"/{objectFolder}/{fileName}",
                                ProductDetailId = (Guid)ProductId,
                                Status = 1

                            };

                            // Lưu ảnh vào cơ sở dữ liệu ở đây (sử dụng dịch vụ _imageUploadService)
                            await _imageUploadService.SaveImageAsync(image);

                        }
                    }
                }

                return Ok(new { Message = "Tải lên thành công", Files = uploadedFiles });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("getProductDetailImages")]
        public IActionResult GetProductDetailImages(Guid ProductId)
        {
            try
            {
                var productDetail = _context.ProductDetails
                    .Include(p => p.Imagess)
                    .FirstOrDefault(p => p.Id == ProductId);

                if (productDetail != null)
                {
                    var imageDetails = productDetail.Imagess.Select(image => new
                    {
                        ImageId = image.Id,
                        ImageCode = image.Ma,
                        ImageUrl = image.LinkImage,

                    });

                    return Ok(imageDetails);
                }
                else
                {
                    return NotFound("Không tìm thấy sản phẩm chi tiết");
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ở đây
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }


        [HttpPost]// DONE wwwroot, not done db
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



        // update many images
        [HttpPut] // DONE wwwroot, not done db
        [Route("updateImages")]
        public async Task<IActionResult> UpdateImages(List<IFormFile> files)
        {
            try
            {
                // Bước 1: Lấy danh sách các tệp ảnh cũ trong thư mục "allPhotoUploaded"
                string uploadFolder = "allPhotoUploaded";
                string folderPath = Path.Combine(_hostingEnvironment.WebRootPath, uploadFolder);

                if (Directory.Exists(folderPath))
                {
                    // Bước 2: Xóa toàn bộ tệp ảnh cũ trong thư mục "allPhotoUploaded"
                    Directory.Delete(folderPath, true);
                }

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var uploadedFiles = new List<string>();

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        // Bước 3: Lưu tệp ảnh mới từ dữ liệu PUT vào thư mục "allPhotoUploaded"
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        string filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        uploadedFiles.Add(fileName);
                    }
                }

                // Bước 4: Trả về danh sách tên tệp ảnh sau khi đã cập nhật
                return Ok(new { Message = "Cập nhật ảnh thành công", Files = uploadedFiles });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }



        [HttpGet]
        [Route("getProductDetailImages2")]
        public IActionResult GetProductDetailImages2(Guid ProductId, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                var productDetail = _context.ProductDetails
                    .Include(p => p.Imagess)
                    .FirstOrDefault(p => p.Id == ProductId);

                if (productDetail != null)
                {
                    var wwwRootPath = hostingEnvironment.WebRootPath;
                    var objectFolder = "product_images";
                    var objectFolderPath = Path.Combine(wwwRootPath, objectFolder);

                    var imageDetails = productDetail.Imagess.Select(image => new Image
                    {
                        Id = image.Id,
                        Ma = image.Ma,
                        LinkImage = Path.Combine(wwwRootPath, objectFolder, image.LinkImage),
                        Status = image.Status,
                        ProductDetailId = image.ProductDetailId,
                        ProductDetail = null  // Setting this to null to break the circular reference
                    }).Where(x=>x.Status==1).ToList();

                    return Ok(imageDetails);
                }
                else
                {
                    return NotFound("Không tìm thấy sản phẩm chi tiết");
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ở đây
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }












    }
}
