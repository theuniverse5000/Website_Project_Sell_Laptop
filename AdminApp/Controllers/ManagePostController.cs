using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Entities;

namespace AdminApp.Controllers
{
    public class ManagePostController : Controller
    {
        private readonly ILogger<ManagePostController> _logger;
        private readonly IConfiguration _config;
        IHttpClientFactory _httpClientFactory;
        public ManagePostController(IHttpClientFactory httpClientFactory, ILogger<ManagePostController> logger, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult _75584461()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var reponse = await client.GetAsync($"/api/ManagePost/GetAllReturnReposon");
                if (reponse.IsSuccessStatusCode)
                {
                    var result = await reponse.Content.ReadAsStringAsync();
                    return Content(result, "application/json");
                }
                return Json(null);
            }
        }


        public async Task<IActionResult> Create(ManagePost obj)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var reponse = await client.PostAsJsonAsync($"/api/ManagePost/CreateMP", obj);
                if (reponse.IsSuccessStatusCode)
                {
                    //var result = reponse.Content.ReadAsStringAsync();
                    return View("Index");
                }
                return Json(null);
            }
        }

        public  IActionResult CreateWithOtherPage()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateWithOtherPage(ManagePost obj)
        {

            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var reponse = await client.PostAsJsonAsync($"/api/ManagePost/CreateMP", obj);
                if (reponse.IsSuccessStatusCode)
                {
                    return View("Index");
                }else return View();
              

            }

        }

        [HttpGet]
        public async Task<IActionResult> DetailsManagePost(Guid id)
        {
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var reponse = await client.GetAsync($"/api/ManagePost/GetByIdManagePost?Id={id}");
                if (reponse.IsSuccessStatusCode)
                {
                    var data = await reponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ManagePost>(data);
                return View(result);
                }return View("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DetailsManagePost(ManagePost obj)
        {
            
            //ManagePost updateObj = new ManagePost();
            //updateObj.Code = obj.Code;
            //updateObj.LinkImage = obj.LinkImage;
            //updateObj.CreateDate = obj.CreateDate;
            //updateObj.Description = obj.Description;
            //updateObj.Status = obj.Status;
            using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
            {
                var reponse = await client.PutAsJsonAsync($"/api/ManagePost", obj);
                if (reponse.IsSuccessStatusCode)
                {
                    
                    return Json(new { success = true });
                }
                else {
                    
                    return Json(null);
                }
            }

        }
    }
}
