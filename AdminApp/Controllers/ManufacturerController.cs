using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_API.AppDbContext;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Net.Http;
using Shop_API.Repository.IRepository;

namespace AdminApp.Controllers
{
  
    public class ManufacturerController : Controller
    {

        private readonly ILogger<ProductDetailController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private string? urlApi;
        private readonly IManufacturerRepository _manufacturer;

        public ManufacturerController(ILogger<ProductDetailController> logger, IConfiguration config, IHttpClientFactory httpClientFactory, IManufacturerRepository manufacturerRepository)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
            urlApi = _config.GetSection("UrlApiAdmin").Value;

            _manufacturer = manufacturerRepository;
        }

        public async Task<IActionResult> GetList()
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            var client = _httpClientFactory.CreateClient();
            string result = await client.GetStringAsync($"{urlApi}/api/Manufacturer");
            return Content(result, "application/json");
        }

        public async Task<IActionResult> GetListViewBag()
        {

          return  ViewBag.ListManufacturer = new Microsoft.AspNetCore.Mvc.Rendering.SelectList((System.Collections.IEnumerable)_manufacturer.GetAll(),"Id","Name");

           
        }

    }
}
