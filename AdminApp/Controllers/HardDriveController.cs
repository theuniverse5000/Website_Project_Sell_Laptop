﻿using Microsoft.AspNetCore.Mvc;
using Shop_API.AppDbContext;
using Shop_Models.Entities;
using System.Net.Http;

namespace AdminApp.Controllers
{
    public class HardDriveController : Controller
    {
        private readonly ILogger<HardDriveController> _logger;
        private readonly IConfiguration _config;
        HttpClient client = new HttpClient();
        ApplicationDbContext context;
        private readonly IHttpClientFactory _httpClientFactory;
        int Check = 1;

        public HardDriveController(ILogger<HardDriveController> logger, IConfiguration config, ApplicationDbContext ctext, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            context = ctext;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> GetHardDrive()
        {


            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                HttpResponseMessage response = await client.GetAsync($"/api/HardDrive/GetHardDriveFSP");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var count = result.Count();
                    ViewBag.Count = count;


                    return Content(result, "application/json");
                }
                else
                {
                    return Json(null);
                }
            }
        }

        public async Task<JsonResult> CreateHardDrive(HardDrive p)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = await client.PostAsJsonAsync($"/api/HardDrive/CreateHardDrive", p);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    ViewBag.CartItem = result;
                    Check = 1;
                    return Json(new { status = "success" });
                }
                else
                {
                    Check = 0;
                    return Json(new { status = "error" });
                }

            }
        }

        public async Task<JsonResult> UpdateHardDrive(HardDrive p)
        {
            try
            {
                string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
                string? urlApi = _config.GetSection("UrlApiAdmin").Value;

                using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
                {
                    // Gửi yêu cầu PUT dưới dạng JSON
                    HttpResponseMessage response = await client.PutAsJsonAsync($"https://localhost:44333/api/HardDrive", p);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        ViewBag.CartItem = result;
                        return Json(result);
                    }
                    else
                    {
                        return Json(null);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception ở đây và trả về một phản hồi JSON cho biết lý do thất bại
                return Json(new { status = "Error", message = ex.Message });
            }
        }


        public async Task<JsonResult> DeleteHardDrive(Guid id)
        {
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                //    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44333/api/HardDrive/id?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    ViewBag.CartItem = result;
                    return Json(result);
                }
                else
                {
                    return Json(null);
                }

            }
        }


        public async Task<JsonResult> DeleteHardDrive2(Guid id)
        {
            try
            {
                string apiKey = _config.GetSection("TokenGetApiAdmin").Value;
                string urlApi = _config.GetSection("UrlApiAdmin").Value;

                using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
                {
                    // Gửi yêu cầu DELETE với id trong URL
                    //HttpResponseMessage response = await client.DeleteAsync($"{urlApi}/api/HardDrive/{id}");
                    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44333/api/HardDrive/id?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        ViewBag.CartItem = result;
                        return Json(result);
                    }
                    else
                    {
                        return Json(null);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception ở đây và trả về một phản hồi JSON cho biết lý do thất bại
                return Json(new { status = "Error", message = ex.Message });
            }
        }



    }
}