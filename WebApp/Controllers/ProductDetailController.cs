using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using X.PagedList;

namespace WebApp.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ILogger<ProductDetailController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private string? urlApi;
        public ProductDetailController(ILogger<ProductDetailController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
            urlApi = _config.GetSection("UrlApiAdmin").Value;
        }
        [HttpGet("sản-phẩm")]
        public async Task<IActionResult> ShowListProductDetail([FromQuery] string? searchString)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
                var apiUrl = $"/api/ProductDetail/PGetProductDetail?status=1&search={searchString}";
                var apiRespone = await httpClient.GetStringAsync(apiUrl);
                var Respone = apiRespone.ToString();
                var responeModel = JsonConvert.DeserializeObject<ResponseDto>(Respone);
                var content = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModel.Result.ToString());
                ViewBag.ListProduct = content;             
                return View();
            }
            catch (Exception)
            {
                return View("Error");
                throw;
            }

        }
        [HttpGet("sản-phẩm/{code}")]
        public async Task<IActionResult> Detail(string code, string name)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
            {
                HttpResponseMessage response = await client.GetAsync($"/api/ProductDetail/PGetProductDetail?codeProductDetail={code}&status=1");

                if (response.IsSuccessStatusCode)
                {
                    var resultString = await response.Content.ReadAsStringAsync();
                    var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    var product = JsonConvert.DeserializeObject<List<ProductDetailDto>>(resultResponse.Result.ToString());
                    ViewBag.product = product;
                    var resultSame = client.GetFromJsonAsync<ResponseDto>($"/api/ProductDetail/PGetProductDetail?search={name}").Result;
                    ViewBag.listProductSame = JsonConvert.DeserializeObject<List<ProductDetailDto>>(resultSame.Result.ToString());
                }
                else
                {
                    ViewBag.product = null;
                }

                return View();
            }
        }

        //[HttpGet("sản-phẩm")]
        public async Task<IActionResult> ShowListProductDetailLoc([FromQuery] string? searchString,int? page, string? productType, string? namufacturerz, double? from, double? to)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
                var apiUrl = $"/api/ProductDetail/PGetProductDetail?status=1&search={searchString}&page={page}&productType={productType}&hangsx={namufacturerz}&from={from}&to={to}";
                var apiRespone = await httpClient.GetStringAsync(apiUrl);
                var Respone = apiRespone.ToString();
                var responeModel = JsonConvert.DeserializeObject<ResponseDto>(Respone);
                var content = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModel.Result.ToString());
                ViewBag.ListProduct = content;
               
                // Specify the page number and page size (4 records per page)
                int pageNumber = page ?? 1;
                int pageSize = 6;

                // Create a paged list
                var pagedList = content.ToPagedList(pageNumber, pageSize);
                ViewBag.ManagePost = pagedList;


                //// Specify the page number and page size (... records per page)
                //int pageNumber = page ?? 1;
                //int pageSize = 10;
                //// Create a paged list
                //var pagedList = content.ToPagedList(pageNumber, pageSize);
                //ViewBag.ListPhanTrang = pagedList;


                // Return the partial view with the filtered product list
                return PartialView("_ProductListPartial", content);
            }
            catch (Exception)
            {
                return View("Error");
                throw;
            }
        }

    }
}
