using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
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
        public ProductDetailController(ILogger<ProductDetailController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
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
            string getProductDetails = await client.GetStringAsync($"/api/ProductDetail/ProductDetailById?id={id}");
            ProductDetail productGetById = JsonConvert.DeserializeObject<ProductDetail>(getProductDetails);
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
            string getProductDetails = await client.GetStringAsync($"/api/ProductDetail/ProductDetailById?id={id}");
            ProductDetail productGetById = JsonConvert.DeserializeObject<ProductDetail>(getProductDetails);
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
                var result = await client.PostAsJsonAsync($"/api/ProductDetail", productDetail);
                if (result.IsSuccessStatusCode)
                {
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
        [HttpPost]
        public async Task<IActionResult> Index(ProductDetailDto productRequest, string editor)
        {
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
            var result = await client.PostAsJsonAsync($"/api/ProductDetail", productDetail);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ProductDetail");
            }
            return RedirectToAction("Index", "ProductDetail", BadRequest("loi"));
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
            var response = await client.GetAsync($"/api/ProductDetail/{id}");

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
            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
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
            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
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
        }
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
        //public IActionResult ImportProductsFromExcel()
        //{
        //    return View();
        //}
        public async Task<IActionResult> ImportProductsFromExcel(IFormFile excelFile)
        {
            try
            {
                using (var package = new ExcelPackage(excelFile.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var products = new List<ProductDetail>();
                    var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var product = new ProductDetail
                        {
                            Id = Guid.NewGuid(),
                            Code = worksheet.Cells[row, 1].Text,
                            ImportPrice = float.Parse(worksheet.Cells[row, 2].Text),
                            Price = float.Parse(worksheet.Cells[row, 3].Text),
                            Upgrade = worksheet.Cells[row, 4].Text,
                            Description = worksheet.Cells[row, 5].Text,
                            Status = int.Parse(worksheet.Cells[row, 6].Text),
                            ProductId = Guid.Parse("D29B5AF1-AF18-4A6C-A4C6-82468DDA11B3"),
                            ColorId = null,
                            RamId = null,
                            CpuId = null,
                            HardDriveId = null,
                            ScreenId = null,
                            CardVGAId = null,
                        };
                        products.Add(product);
                        var result = await client.PostAsJsonAsync($"/api/ProductDetail/Create", product);
                        //if (result.IsSuccessStatusCode)
                        //{
                        //    return RedirectToAction("Index", "ProductDetail");
                        //}

                        //return BadRequest(result);
                    }
                    return RedirectToAction("Index", "ProductDetail");


                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }
        [HttpGet("export-products-to-excel")]
        public async Task<IActionResult> ExportProductsToExcel()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
                var getProductDetails = await client.GetFromJsonAsync<ProductDetail>($"/api/ProductDetail/PGetProductDetail");

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("getProductDetails");
                    worksheet.Cells[1, 1].Value = "Code";
                    worksheet.Cells[1, 2].Value = "ImportPrice";
                    worksheet.Cells[1, 3].Value = "Price";
                    worksheet.Cells[1, 4].Value = "Upgrade";
                    worksheet.Cells[1, 5].Value = "Description";
                    worksheet.Cells[1, 6].Value = "Status";
                    int row = 2;
                    //foreach (var product in products)
                    //{
                    //    worksheet.Cells[row, 1].Value = product.Code;
                    //    worksheet.Cells[row, 2].Value = product.ImportPrice;
                    //    worksheet.Cells[row, 3].Value = product.Price;
                    //    worksheet.Cells[row, 4].Value = product.Upgrade;
                    //    worksheet.Cells[row, 5].Value = product.Description;
                    //    worksheet.Cells[row, 6].Value = product.Status;
                    //    row++;
                    //}
                    var memoryStream = new MemoryStream();
                    package.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

    }

}

