using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_API.AppDbContext;
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
        Product ProductById;
        List<Color> listColor;
        List<Ram> listRam;
        List<Cpu> listCpu;
        List<HardDrive> listHardDrive;
        List<Screen> listScreen;
        List<CardVGA> listCardVGA;
        private readonly ApplicationDbContext _context;

        public ProductDetailController(ILogger<ProductDetailController> logger, IConfiguration config, IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
            _context = context;



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
            string result = await client.GetStringAsync($"/api/ProductDetail/PGetProductDetail");
            return Content(result, "application/json");
        }



        public async Task<IActionResult> Create(Guid id)
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

            //if (id == Guid.Empty)
            //{
            //    return PartialView("Create", id);
            //}
            //else
            //{
            string getProductDetails = await client.GetStringAsync($"/api/ProductDetail/ProductDetailById?id={id}");
            ProductDetail productGetById = JsonConvert.DeserializeObject<ProductDetail>(getProductDetails);
            //ViewBag.ProductGetById = productGetById;

            // Populate select options for Product, Color, Ram, CPU, and other entities
            ViewBag.SelectedProductId = productGetById.ProductId;
            ViewBag.SelectedColorId = productGetById.ColorId;
            //ViewBag.SelectedRamId = productGetById.IdRam;
            //ViewBag.SelectedCPUId = productGetById.IdCpu;
            //ViewBag.SelectedScreenId = productGetById.IdScreen;
            //ViewBag.SelectedHardDriveId = productGetById.IdHardDrive;
            //ViewBag.SelectedCardVGAId = productGetById.IdCardVGA;
            return View(productGetById);

            //return PartialView("Create", productGetById);
            //}         
        }
        public async Task<IActionResult> Edit(Guid id)
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

            //if (id == Guid.Empty)
            //{
            //    return PartialView("Create", id);
            //}
            //else
            //{
            string getProductDetails = await client.GetStringAsync($"/api/ProductDetail/ProductDetailById?id={id}");
            ProductDetail productGetById = JsonConvert.DeserializeObject<ProductDetail>(getProductDetails);
            //ViewBag.ProductGetById = productGetById;

            // Populate select options for Product, Color, Ram, CPU, and other entities
            ViewBag.SelectedProductId = productGetById.ProductId;
            ViewBag.SelectedColorId = productGetById.ColorId;
            //ViewBag.SelectedRamId = productGetById.IdRam;
            //ViewBag.SelectedCPUId = productGetById.IdCpu;
            //ViewBag.SelectedScreenId = productGetById.IdScreen;
            //ViewBag.SelectedHardDriveId = productGetById.IdHardDrive;
            //ViewBag.SelectedCardVGAId = productGetById.IdCardVGA;
            return View(productGetById);

            //return PartialView("Create", productGetById);
            //}         
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
            //if (productRequest.IdProduct == Guid.Empty || productRequest.IdColor == Guid.Empty || productRequest.IdRam == Guid.Empty
            //    || productRequest.IdCpu == Guid.Empty || productRequest.IdScreen == Guid.Empty
            //    || productRequest.IdHardDrive == Guid.Empty || productRequest.IdCardVGA == Guid.Empty
            //    || string.IsNullOrEmpty(editor))
            //{
            //    ModelState.AddModelError("", "Vui lòng chọn đầy đủ thông tin và nhập nội dung trường soạn thảo.");
            //    return View(productRequest); // Trả về lại view với model và thông báo lỗi
            //}

            //if (ModelState.IsValid)
            //{
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
                ColorId = productRequest.IdColor == Guid.Empty ? null : productRequest.IdColor,
                RamId = productRequest.IdRam == Guid.Empty ? null : productRequest.IdRam,
                CpuId = productRequest.IdCpu == Guid.Empty ? null : productRequest.IdCpu,
                HardDriveId = productRequest.IdHardDrive == Guid.Empty ? null : productRequest.IdHardDrive,
                ScreenId = productRequest.IdScreen == Guid.Empty ? null : productRequest.IdScreen,
                CardVGAId = productRequest.IdCardVGA == Guid.Empty ? null : productRequest.IdCardVGA,
            };

            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
            var result = await client.PostAsJsonAsync($"https://localhost:44333/api/ProductDetail", productDetail);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ProductDetail");
            }
            return RedirectToAction("Index", "ProductDetail", BadRequest("loi"));
            //}
            //else
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            //    return Json(new
            //    {
            //        Errors = errors
            //    });
            //}
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
        public async Task<IActionResult> Index(Guid guid)
        {
            // Logic để lấy dữ liệu sản phẩm dựa trên Id và truyền vào view

            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");

            //var getProduct = await client.GetFromJsonAsync<List<Product>>($"/api/Product");
            //ViewBag.GetProduct = getProduct;
            var getProduct = await client.GetStringAsync($"/api/Product");
            ViewBag.GetProduct = JsonConvert.DeserializeObject<List<Product>>(getProduct); ;


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

            //ViewBag.GuidDetail = guid;
            // Logic để lấy dữ liệu sản phẩm dựa trên Id và truyền vào view
            if (guid == Guid.Empty)
            {
                return View();
            }
            else
            {
                var getProductDetails = await client.GetFromJsonAsync<ProductDetailDto>($"/api/ProductDetail/ProductDetailByIdReturnProDetailDTO?guid={guid}");
                if (getProductDetails != null)
                {

                    return View(getProductDetails);
                }
                else
                {
                    return View();

                }
            }
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            // Logic để lấy dữ liệu sản phẩm dựa trên Id và truyền vào view

            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");

            //var getProduct = await client.GetFromJsonAsync<List<Product>>($"/api/Product");
            //ViewBag.GetProduct = getProduct;

            //var getManufacturer = await client.GetFromJsonAsync<List<Manufacturer>>($"/api/Manufacturer");
            ////ViewBag.GetManufacturer = JsonConvert.DeserializeObject<List<Manufacturer>>(getManufacturer);
            //ViewBag.Manufacturer = getManufacturer;

            //var getColor = await client.GetFromJsonAsync<List<Color>>($"/api/Color");
            ////ViewBag.GetColor = JsonConvert.DeserializeObject<List<Color>>(getColor);
            ////listColor = JsonConvert.DeserializeObject<List<Color>>(getColor);
            //ViewBag.GetColor = getColor;


            //var getProductType = await client.GetFromJsonAsync<List<ProductType>>($"/api/ProductType");
            ////ViewBag.GetProductType = JsonConvert.DeserializeObject<List<ProductType>>(getProductType);
            //ViewBag.ProductType = getProductType;

            //var getCPU = await client.GetFromJsonAsync<List<Cpu>>($"/api/Cpu");
            ////ViewBag.GetCPU = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);
            ////listCpu = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);
            //ViewBag.GetCPU = getCPU;

            //var getRam = await client.GetFromJsonAsync<List<Ram>>($"/api/Ram");
            ////ViewBag.GetRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);
            ////listRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);
            //ViewBag.GetRam = getRam;

            //var getHardDrive = await client.GetFromJsonAsync<List<HardDrive>>($"/api/HardDrive");
            ////ViewBag.GetHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);
            ////listHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);
            //ViewBag.HardDrive = getHardDrive;

            //var getScreen = await client.GetFromJsonAsync<List<Screen>>($"/api/Screen");
            ////ViewBag.GetScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);
            ////listScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);
            //ViewBag.GetScreen = getScreen;

            //var getlistCardVGA = await client.GetFromJsonAsync<List<CardVGA>>($"/api/CardVGA");
            ////ViewBag.GetlistCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);
            ////listCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);
            //ViewBag.GetlistCardVGA = getlistCardVGA;


            var getProductDetails = await client.GetFromJsonAsync<ProductDetail>($"/api/ProductDetail/ProductDetailById?guid={id}");
            return PartialView("_Update", getProductDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDetail getProductDetails, string? editor)
        {


            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
            ProductDetail productDetail = new ProductDetail();
            productDetail.ImportPrice = (float)getProductDetails.ImportPrice;
            productDetail.Price = (float)getProductDetails.Price;
            productDetail.Upgrade = getProductDetails.Upgrade;
            productDetail.Description = editor;
            var result = client.PutAsJsonAsync($"/api/ProductDetail/UpdateProductDetail", getProductDetails).Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ProductDetail");
            }
            return RedirectToAction("Index", "ProductDetail");
            //}
            //else
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            //    return Json(new
            //    {
            //        Errors = errors
            //    });
            //}
        }


        //public async Task<IActionResult> Update(ProductDetailDto productDetailDto)
        //{
        //    // Logic để lấy dữ liệu sản phẩm dựa trên Id và truyền vào view

        //    var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");

        //    //var getProduct = await client.GetFromJsonAsync<List<Product>>($"/api/Product");
        //    //ViewBag.GetProduct = getProduct;

        //    //var getManufacturer = await client.GetFromJsonAsync<List<Manufacturer>>($"/api/Manufacturer");
        //    ////ViewBag.GetManufacturer = JsonConvert.DeserializeObject<List<Manufacturer>>(getManufacturer);
        //    //ViewBag.Manufacturer = getManufacturer;

        //    //var getColor = await client.GetFromJsonAsync<List<Color>>($"/api/Color");
        //    ////ViewBag.GetColor = JsonConvert.DeserializeObject<List<Color>>(getColor);
        //    ////listColor = JsonConvert.DeserializeObject<List<Color>>(getColor);
        //    //ViewBag.GetColor = getColor;


        //    //var getProductType = await client.GetFromJsonAsync<List<ProductType>>($"/api/ProductType");
        //    ////ViewBag.GetProductType = JsonConvert.DeserializeObject<List<ProductType>>(getProductType);
        //    //ViewBag.ProductType = getProductType;

        //    //var getCPU = await client.GetFromJsonAsync<List<Cpu>>($"/api/Cpu");
        //    ////ViewBag.GetCPU = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);
        //    ////listCpu = JsonConvert.DeserializeObject<List<Cpu>>(getCPU);
        //    //ViewBag.GetCPU = getCPU;

        //    //var getRam = await client.GetFromJsonAsync<List<Ram>>($"/api/Ram");
        //    ////ViewBag.GetRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);
        //    ////listRam = JsonConvert.DeserializeObject<List<Ram>>(getRam);
        //    //ViewBag.GetRam = getRam;

        //    //var getHardDrive = await client.GetFromJsonAsync<List<HardDrive>>($"/api/HardDrive");
        //    ////ViewBag.GetHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);
        //    ////listHardDrive = JsonConvert.DeserializeObject<List<HardDrive>>(getHardDrive);
        //    //ViewBag.HardDrive = getHardDrive;

        //    //var getScreen = await client.GetFromJsonAsync<List<Screen>>($"/api/Screen");
        //    ////ViewBag.GetScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);
        //    ////listScreen = JsonConvert.DeserializeObject<List<Screen>>(getScreen);
        //    //ViewBag.GetScreen = getScreen;

        //    //var getlistCardVGA = await client.GetFromJsonAsync<List<CardVGA>>($"/api/CardVGA");
        //    ////ViewBag.GetlistCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);
        //    ////listCardVGA = JsonConvert.DeserializeObject<List<CardVGA>>(getlistCardVGA);
        //    //ViewBag.GetlistCardVGA = getlistCardVGA;

        //    ProductDetail productDetail = 

        //    var getProductDetails = await client.PutAsJsonAsync<ProductDetailDto>($"https://localhost:44333/api/ProductDetail",guid);
        //    return PartialView("_Update", getProductDetails);
        //}

        public async Task<ActionResult> UpdatePartialView(Guid guid)
        {
            // Logic để lấy dữ liệu sản phẩm dựa trên Id và truyền vào view

            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
            var getProduct = await client.GetFromJsonAsync<List<Product>>($"/api/Product");
            ViewBag.GetProduct = getProduct;

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


            var getProductDetails = await client.GetFromJsonAsync<ProductDetailDto>($"/api/ProductDetail/ProductDetailByIdReturnProDetailDTO?guid={guid}");
            return PartialView(getProductDetails);
        }

    }

}

