﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using X.PagedList;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory=httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = "/api/ProductDetail/PGetProductDetail";
            var apiRespone = await httpClient.GetStringAsync(apiUrl);
            var Respone = apiRespone.ToString();
            var responeModel = JsonConvert.DeserializeObject<ResponseDto>(Respone);
            var content = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModel.Result.ToString());
            ViewBag.ProductDetails = content;
            var apiImagesUrl = "/api/Images";
            var apiResponeImage = await httpClient.GetStringAsync(apiImagesUrl);

            // Deserialize trực tiếp thành danh sách ImageDto
            var contentIMage = JsonConvert.DeserializeObject<List<ImageDto>>(apiResponeImage);

            // Gán danh sách ImageDto trực tiếp cho ViewBag.Image
            ViewBag.Image = contentIMage;


            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> ProductDetail(string code)
        {
            var  x = HttpContext.Request;
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = $@"/api/ProductDetail/PGetProductDetail?codeProductDetail={code}";
            var apiRespone = await httpClient.GetStringAsync(apiUrl);
            var content = JsonConvert.DeserializeObject<ResponseDto>(apiRespone);
            var productDetails = JsonConvert.DeserializeObject<List<ProductDetailDto>>(content.Result.ToString());
            return PartialView("_Detail", productDetails.FirstOrDefault());

        }

        public async Task<IActionResult> GetAllManagePost(int? page)
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
                {
                    string getAll = await client.GetStringAsync("/api/ManagePost/GGetManagePostDtosFSP");
                    var responeModel = JsonConvert.DeserializeObject<ResponseDto>(getAll);
                    var managePosts = JsonConvert.DeserializeObject<List<ManagePost>>(responeModel.Result.ToString());

                    // Specify the page number and page size (4 records per page)
                    int pageNumber = page ?? 1;
                    int pageSize = 4;

                    // Create a paged list
                    var pagedList = managePosts.ToPagedList(pageNumber, pageSize);
                    ViewBag.ManagePost = pagedList;
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, display an error page, etc.)
                return View("Error");
            }
        }


        public async Task<IActionResult> Details(Guid guid)
        {
            var x = HttpContext.Request;
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            //var apiUrl = $"/api/ManagePost/GetByIdManagePost?Id={guid}";
            var apiRespone = await httpClient.GetStringAsync($"/api/ManagePost/GetByIdManagePost?Id={guid}");
            //var content = JsonConvert.DeserializeObject<ResponseDto>(apiRespone);
            var productDetails = JsonConvert.DeserializeObject<ManagePost>(apiRespone);
            return View(productDetails);

        }

    }
}
