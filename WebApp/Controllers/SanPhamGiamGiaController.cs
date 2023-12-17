using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using System.Net.Http;

namespace WebApp.Controllers
{
    public class SanPhamGiamGiaController : Controller
    {
        private readonly ILogger<SanPhamGiamGiaController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private string? urlApi;
        public SanPhamGiamGiaController(ILogger<SanPhamGiamGiaController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
            urlApi = _config.GetSection("UrlApiAdmin").Value;
        }

        [HttpGet("sản-phẩm-giảm-giá/{code}")]
        public async Task<IActionResult> Detail(string code, string name)
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl2 = $"/api/SanPhamGiamGia/GetSPGGPG?codeProductDetail={code}";
            var apiRespone2 = await httpClient.GetStringAsync(apiUrl2);
            var Respone2 = apiRespone2.ToString();
            var responeModel2 = JsonConvert.DeserializeObject<ResponseDto>(Respone2);
            var content2 = JsonConvert.DeserializeObject<List<SanPhamGiamGiaDto>>(responeModel2.Result.ToString());
            ViewBag.SanPhamGiamGia = content2;
            var apiUrl1 = $"/api/SanPhamGiamGia/GetSPGGPG?search={name}";
            var apiRespone1 = await httpClient.GetStringAsync(apiUrl1);
            var Respone1 = apiRespone1.ToString();
            var responeModel1 = JsonConvert.DeserializeObject<ResponseDto>(Respone1);
            var content1 = JsonConvert.DeserializeObject<List<SanPhamGiamGiaDto>>(responeModel1.Result.ToString());
            ViewBag.SanPhamGiamGiaSame = content1;

            return View();
            
        }
    }
}

