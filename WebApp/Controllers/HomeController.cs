using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Runtime.Intrinsics.Arm;
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
            var apiUrl = "/api/ProductDetail/PGetProductDetail?status=2";
            var apiRespone = await httpClient.GetStringAsync(apiUrl);
            var Respone = apiRespone.ToString();
            var responeModel = JsonConvert.DeserializeObject<ResponseDto>(Respone);
            var content = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModel.Result.ToString());
            ViewBag.LinhKien = content;
            var apiUrl2 = "/api/SanPhamGiamGia/GetSPGGPG";
            var apiRespone2 = await httpClient.GetStringAsync(apiUrl2);
            var Respone2 = apiRespone2.ToString();
            var responeModel2 = JsonConvert.DeserializeObject<ResponseDto>(Respone2);
            var content2 = JsonConvert.DeserializeObject<List<SanPhamGiamGiaDto>>(responeModel2.Result.ToString());
            ViewBag.SanPhamGiamGia = content2;
            var apiUrl3 = "api/GiamGia/GetGiamGiasFSP";
            var apiRespone3 = await httpClient.GetStringAsync(apiUrl3);
            var Respone3 = apiRespone3.ToString();
            var responeModel3 = JsonConvert.DeserializeObject<ResponseDto>(Respone3);
            var content3 = JsonConvert.DeserializeObject<List<GiamGia>>(responeModel3.Result.ToString());
            ViewBag.GiamGia = content3;
            var apiImagesUrl = "/api/Images";
            var apiResponeImage = await httpClient.GetStringAsync(apiImagesUrl);


            string doHoa = "Laptop đồ họa";
            var apiLaptopDoHoaUrl = $"api/ProductDetail/PGetProductDetail?productType={doHoa}&page=1";                             
            var apiResponeLapTopDoHoa = await httpClient.GetStringAsync(apiLaptopDoHoaUrl);
            var ResponeLaptopDoHoa = apiResponeLapTopDoHoa.ToString();
            var responeModelLaptopDoHoa = JsonConvert.DeserializeObject<ResponseDto>(ResponeLaptopDoHoa);
            var contentLapTopDoHoa = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModelLaptopDoHoa.Result.ToString());
            ViewBag.ListLaptopDoHoa = contentLapTopDoHoa;

            string gaming = "Laptop Gaming";
            var apiLaptopGamingUrl = $"api/ProductDetail/PGetProductDetail?productType={gaming}&page=1";
            var apiResponeGaming = await httpClient.GetStringAsync(apiLaptopGamingUrl);
            var ResponeGaming = apiResponeGaming.ToString();
            var responeModelGaming = JsonConvert.DeserializeObject<ResponseDto>(ResponeGaming);
            var contentGaming = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModelGaming.Result.ToString());
            ViewBag.ListGaming = contentGaming;


            string mongNhe = "Laptop Mỏng Nhẹ";
            var apiLaptopmongNheUrl = $"api/ProductDetail/PGetProductDetail?productType={mongNhe}&page=1";
            var apiResponemongNhe = await httpClient.GetStringAsync(apiLaptopmongNheUrl);
            var ResponemongNhe = apiResponemongNhe.ToString();
            var responeModelmongNhe = JsonConvert.DeserializeObject<ResponseDto>(ResponemongNhe);
            var contentmongNhe = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModelmongNhe.Result.ToString());
            ViewBag.ListmongNhe = contentmongNhe;


            string giaRe = "Laptop Giá Rẻ";
            var apiLaptopgiaReUrl = $"api/ProductDetail/PGetProductDetail?productType={giaRe}&page=1";
            var apiResponegiaRe = await httpClient.GetStringAsync(apiLaptopgiaReUrl);
            var ResponegiaRe = apiResponegiaRe.ToString();
            var responeModelgiaRe = JsonConvert.DeserializeObject<ResponseDto>(ResponegiaRe);
            var contentgiaRe = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModelgiaRe.Result.ToString());
            ViewBag.ListgiaRe = contentgiaRe;

            string vanPhong = "Laptop Văn Phòng";
            var apiLaptopvanPhongUrl = $"api/ProductDetail/PGetProductDetail?productType={vanPhong}&page=1";
            var apiResponevanPhong = await httpClient.GetStringAsync(apiLaptopvanPhongUrl);
            var ResponevanPhong = apiResponevanPhong.ToString();
            var responeModelvanPhong = JsonConvert.DeserializeObject<ResponseDto>(ResponevanPhong);
            var contentvanPhong = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModelvanPhong.Result.ToString());
            ViewBag.ListvanPhong = contentvanPhong;


            // Deserialize trực tiếp thành danh sách ImageDto
            var contentIMage = JsonConvert.DeserializeObject<List<ImageDto>>(apiResponeImage);

            // Gán danh sách ImageDto trực tiếp cho ViewBag.Image
            ViewBag.Image = contentIMage;


            return View();
        }

        [Route("deal")]
        public async Task<IActionResult> Deal()
        {
            var httpClient = _httpClientFactory.CreateClient("PhuongThaoHttpWeb");
            var apiUrl = "/api/ProductDetail/PGetProductDetail";
            var apiRespone = await httpClient.GetStringAsync(apiUrl);
            var Respone = apiRespone.ToString();
            var responeModel = JsonConvert.DeserializeObject<ResponseDto>(Respone);
            var content = JsonConvert.DeserializeObject<List<ProductDetailDto>>(responeModel.Result.ToString());
            ViewBag.ProductDetails = content;
            var apiUrl2 = "/api/SanPhamGiamGia/GetSPGGPG";
            var apiRespone2 = await httpClient.GetStringAsync(apiUrl2);
            var Respone2 = apiRespone2.ToString();
            var responeModel2 = JsonConvert.DeserializeObject<ResponseDto>(Respone2);
            var content2 = JsonConvert.DeserializeObject<List<SanPhamGiamGiaDto>>(responeModel2.Result.ToString());
            ViewBag.SanPhamGiamGia = content2;
            var apiUrl3 = "api/GiamGia/GetGiamGiasFSP";
            var apiRespone3 = await httpClient.GetStringAsync(apiUrl3);
            var Respone3 = apiRespone3.ToString();
            var responeModel3 = JsonConvert.DeserializeObject<ResponseDto>(Respone3);
            var content3 = JsonConvert.DeserializeObject<List<GiamGia>>(responeModel3.Result.ToString());
            ViewBag.GiamGia = content3;
            var apiImagesUrl = "/api/Images";
            var apiResponeImage = await httpClient.GetStringAsync(apiImagesUrl);

            // Deserialize trực tiếp thành danh sách ImageDto
            var contentIMage = JsonConvert.DeserializeObject<List<ImageDto>>(apiResponeImage);

            // Gán danh sách ImageDto trực tiếp cho ViewBag.Image
            ViewBag.Image = contentIMage;
            return View();
        }

        [Route("info")]
        public IActionResult Info()
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
        //[Route("tintucsukien")]
        public async Task<IActionResult> GetAllManagePost(int? page,bool status)
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpWeb"))
                {
                    status = true;
                    string getAll = await client.GetStringAsync($"https://localhost:44333/api/ManagePost/GGetManagePostDtosFSP?status={status}");
                

                    var responeModel = JsonConvert.DeserializeObject<ResponseDto>(getAll);
                    var managePosts = JsonConvert.DeserializeObject<List<ManagePost>>(responeModel.Result.ToString());

                    // Specify the page number and page size (4 records per page)
                    int pageNumber = page ?? 1;
                    int pageSize = 6;

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
