using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace AdminApp.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ILogger<ProductDetailController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        List<Product> listProduct;
        List<Color> listColor;
        List<Ram> listRam;
        List<Cpu> listCpu;
        List<HardDrive> listHardDrive;
        List<Screen> listScreen;
        List<CardVGA> listCardVGA;


        public ProductDetailController(ILogger<ProductDetailController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;




        }
        public async Task<IActionResult> IndexTamThoiDoiTen()
        {
            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");

            string getProduct = await client.GetStringAsync($"/api/Product");
            ViewBag.GetProduct = JsonConvert.DeserializeObject<List<Product>>(getProduct);
            listProduct = JsonConvert.DeserializeObject<List<Product>>(getProduct);

            string getManufacturer = await client.GetStringAsync($"/api/Manufacturer");
            ViewBag.GetManufacturer = JsonConvert.DeserializeObject<List<Manufacturer>>(getManufacturer);

            string getColor = await client.GetStringAsync($"/api/Color");
            ViewBag.GetColor = JsonConvert.DeserializeObject<List<Color>>(getColor);
            listColor = JsonConvert.DeserializeObject<List<Color>>(getColor);

            string getProductType = await client.GetStringAsync($"/api/ProductType");
            ViewBag.GetProductType = JsonConvert.DeserializeObject<List<ProductType>>(getProductType);

            string getCPU = await client.GetStringAsync($"/api/Cpu");
            ViewBag.GetCPU = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);
            listCpu = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);

            string getRam = await client.GetStringAsync($"/api/Ram");
            ViewBag.GetRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);
            listRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);

            string getHardDrive = await client.GetStringAsync($"/api/HardDrive");
            ViewBag.GetHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);
            listHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);

            string getScreen = await client.GetStringAsync($"/api/Screen");
            ViewBag.GetScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);
            listScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);

            string getlistCardVGA = await client.GetStringAsync($"/api/CardVGA");
            ViewBag.GetlistCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);
            listCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);

            return View();

            //return View();
        }
        public async Task<IActionResult> GetProductDetail()
        {

            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
            string result = await client.GetStringAsync($"/api/ProductDetail/GetProductDetailsFSP");
            return Content(result, "application/json");
        }



        public async Task<IActionResult> Create(ProductDetailDto productDetailDto)
        {
            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");

            string getProduct = await client.GetStringAsync($"/api/Product");
            ViewBag.GetProduct = JsonConvert.DeserializeObject<List<Product>>(getProduct);
            listProduct = JsonConvert.DeserializeObject<List<Product>>(getProduct);

            string getManufacturer = await client.GetStringAsync($"/api/Manufacturer");
            ViewBag.GetManufacturer = JsonConvert.DeserializeObject<List<Manufacturer>>(getManufacturer);

            string getColor = await client.GetStringAsync($"/api/Color");
            ViewBag.GetColor = JsonConvert.DeserializeObject<List<Color>>(getColor);
            listColor = JsonConvert.DeserializeObject<List<Color>>(getColor);

            string getProductType = await client.GetStringAsync($"/api/ProductType");
            ViewBag.GetProductType = JsonConvert.DeserializeObject<List<ProductType>>(getProductType);

            string getCPU = await client.GetStringAsync($"/api/Cpu");
            ViewBag.GetCPU = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);
            listCpu = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);

            string getRam = await client.GetStringAsync($"/api/Ram");
            ViewBag.GetRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);
            listRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);

            string getHardDrive = await client.GetStringAsync($"/api/HardDrive");
            ViewBag.GetHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);
            listHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);

            string getScreen = await client.GetStringAsync($"/api/Screen");
            ViewBag.GetScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);
            listScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);

            string getlistCardVGA = await client.GetStringAsync($"/api/CardVGA");
            ViewBag.GetlistCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);
            listCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);


            //if (productDetailDto == null) productDetailDto = new ProductDetailDto();
            //if (productDetailDto.Id!=Guid.Empty)
            //             productDetailDto = 

            return PartialView("Create", productDetailDto);

        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDetailDto productRequest, string editor)
        {
            if (ModelState.IsValid)
            {
                ProductDetail productDetail = new ProductDetail()
                {
                    Id = Guid.NewGuid(),
                    Code = productRequest.Code,
                    ImportPrice = (float)productRequest.ImportPrice,
                    Price = (float)productRequest.Price,
                    //ImportPrice = (float)productRequest.ImportPrice,
                    Upgrade = productRequest.Upgrade,
                    Description = editor,
                    Status = 1,
                    ProductId = productRequest.IdProduct,
                    ColorId = productRequest.IdColor,
                    RamId = productRequest.IdRam,
                    CpuId = productRequest.IdCpu,
                    HardDriveId = productRequest.IdHardDrive,
                    ScreenId = productRequest.IdScreen,
                    CardVGAId = productRequest.IdCardVGA,

                };
                var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
                var result = await client.PostAsJsonAsync($"https://localhost:44333/api/ProductDetail", productDetail);
                if (result.IsSuccessStatusCode)
                {
                    //return Json(productDetail);
                    return RedirectToAction("Index", "ProductDetail");
                }
                return Json(null);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { Errors = errors });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(ProductDetailDto productRequest, string editor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ProductDetail productDetail = new ProductDetail()
        //        {
        //            Id = Guid.NewGuid(),
        //            Code = productRequest.Code,
        //            ImportPrice = (float)productRequest.ImportPrice,
        //            Price = (float)productRequest.Price,
        //            //ImportPrice = (float)productRequest.ImportPrice,
        //            Upgrade = productRequest.Upgrade,
        //            Description = editor,
        //            Status = 1,
        //            ProductId = productRequest.IdProduct,
        //            ColorId = productRequest.IdColor,
        //            RamId = productRequest.IdRam,
        //            CpuId = productRequest.IdCpu,
        //            HardDriveId = productRequest.IdHardDrive,
        //            ScreenId = productRequest.IdScreen,
        //            CardVGAId = productRequest.IdCardVGA,

        //        };
        //        var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
        //        var result = await client.PostAsJsonAsync($"https://localhost:44333/api/ProductDetail", productDetail);
        //        if (result.IsSuccessStatusCode)
        //        {
        //            //return Json(productDetail);
        //            return RedirectToAction("Index", "ProductDetail");
        //        }
        //        return RedirectToAction("Index", "ProductDetail",BadRequest("loi"));
        //    }
        //    else
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        //        return Json(new
        //        {
        //            Errors = errors
        //        });
        //    }
        //}



        [HttpPost] // index theem
        public async Task<IActionResult> Index(ProductDetailDto productRequest, string editor)
        {
            // Kiểm tra tính hợp lệ của các trường select và trường soạn thảo ở phía server
            if (productRequest.IdProduct == Guid.Empty || productRequest.IdColor == Guid.Empty || productRequest.IdRam == Guid.Empty
                || productRequest.IdCpu == Guid.Empty || productRequest.IdScreen == Guid.Empty
                || productRequest.IdHardDrive == Guid.Empty || productRequest.IdCardVGA == Guid.Empty
                || string.IsNullOrEmpty(editor))
            {
                ModelState.AddModelError("", "Vui lòng chọn đầy đủ thông tin và nhập nội dung trường soạn thảo.");
                return View(productRequest); // Trả về lại view với model và thông báo lỗi
            }

            if (ModelState.IsValid)
            {
                // Tạo đối tượng ProductDetail và thêm vào cơ sở dữ liệu
                ProductDetail productDetail = new ProductDetail()
                {
                    Id = Guid.NewGuid(),
                    Code = productRequest.Code,
                    ImportPrice = (float)productRequest.ImportPrice,
                    Price = (float)productRequest.Price,
                    Upgrade = productRequest.Upgrade,
                    Description = editor,
                    Status = 1,
                    ProductId = productRequest.IdProduct,
                    ColorId = productRequest.IdColor,
                    RamId = productRequest.IdRam,
                    CpuId = productRequest.IdCpu,
                    HardDriveId = productRequest.IdHardDrive,
                    ScreenId = productRequest.IdScreen,
                    CardVGAId = productRequest.IdCardVGA,
                };

                var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
                var result = await client.PostAsJsonAsync($"https://localhost:44333/api/ProductDetail", productDetail);

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "ProductDetail");
                }
                return RedirectToAction("Index", "ProductDetail", BadRequest("loi"));
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new
                {
                    Errors = errors
                });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
            var response = await client.GetAsync($"https://localhost:44333/api/ProductDetail/{id}");

            if (response.IsSuccessStatusCode)
            {
                var productDetail = await response.Content.ReadFromJsonAsync<ProductDetailDto>();
                return View(productDetail);
            }
            else
            {
                // Xử lý khi không tìm thấy chi tiết sản phẩm
                return RedirectToAction("Index", "ProductDetail");
            }
        }


        [HttpGet]
        ////[HttpPost]
        public async Task<IActionResult> Index(Guid id)
        {
            // Logic để lấy dữ liệu sản phẩm dựa trên Id và truyền vào view
            // ProductDetailDto product = ...;

            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
            string getProduct = await client.GetStringAsync($"/api/Product");
            ViewBag.GetProduct = JsonConvert.DeserializeObject<List<Product>>(getProduct);
            listProduct = JsonConvert.DeserializeObject<List<Product>>(getProduct);

            string getManufacturer = await client.GetStringAsync($"/api/Manufacturer");
            ViewBag.GetManufacturer = JsonConvert.DeserializeObject<List<Manufacturer>>(getManufacturer);

            string getColor = await client.GetStringAsync($"/api/Color");
            ViewBag.GetColor = JsonConvert.DeserializeObject<List<Color>>(getColor);
            listColor = JsonConvert.DeserializeObject<List<Color>>(getColor);

            string getProductType = await client.GetStringAsync($"/api/ProductType");
            ViewBag.GetProductType = JsonConvert.DeserializeObject<List<ProductType>>(getProductType);

            string getCPU = await client.GetStringAsync($"/api/Cpu");
            ViewBag.GetCPU = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);
            listCpu = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);

            string getRam = await client.GetStringAsync($"/api/Ram");
            ViewBag.GetRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);
            listRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);

            string getHardDrive = await client.GetStringAsync($"/api/HardDrive");
            ViewBag.GetHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);
            listHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);

            string getScreen = await client.GetStringAsync($"/api/Screen");
            ViewBag.GetScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);
            listScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);

            string getlistCardVGA = await client.GetStringAsync($"/api/CardVGA");
            ViewBag.GetlistCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);
            listCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);



            // Logic để lấy dữ liệu sản phẩm dựa trên Id và truyền vào view
            if (id == Guid.Empty)
            {
                return View();
            }
            else
            {
                string getProductDetails = await client.GetStringAsync($"/api/ProductDetail/GetById?id={id}");
                ProductDetail productGetById = JsonConvert.DeserializeObject<ProductDetail>(getProductDetails);
                return View("Create", productGetById);
            }



            //if (productGetById == null)
            //{
            //    return NotFound(); // Trả về trạng thái 404 nếu không tìm thấy sản phẩm
            //}

            //return View(productGetById);    

        }

    }
}
