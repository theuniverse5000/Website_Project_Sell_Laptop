using Microsoft.AspNetCore.Mvc;
using Shop_Models.Entities;

namespace AdminApp.Controllers
{
	public class ColorController : Controller
	{
		private readonly ILogger<ColorController> _logger;
		private readonly IConfiguration _config;
		HttpClient client = new HttpClient();
		private readonly IHttpClientFactory _httpClientFactory;
        int Check = 1;

		public ColorController(ILogger<ColorController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_config = config;
            _httpClientFactory = httpClientFactory;
        }
		public IActionResult Index()
		{

			return View();
		}

		public async Task<IActionResult> GetColor()
        {


			using (var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
			{
				HttpResponseMessage response = await client.GetAsync($"/api/Color/GetColorFSP");

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

		public async Task<JsonResult> CreateColor(Color p)
		{
            string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
            string? urlApi = _config.GetSection("UrlApiAdmin").Value;
            using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
			{
				//    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
				HttpResponseMessage response = await client.PostAsJsonAsync($"/api/Color/CreateColor", p);
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

		public async Task<JsonResult> UpdateColor(Color p)
		{
			try
			{
				string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
				string? urlApi = _config.GetSection("UrlApiAdmin").Value;

				using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
				{
					// Gửi yêu cầu PUT dưới dạng JSON
					HttpResponseMessage response = await client.PutAsJsonAsync($"https://localhost:44333/api/Color", p);

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


		public async Task<JsonResult> DeleteColor(Guid id)
		{
			string? apiKey = _config.GetSection("TokenGetApiAdmin").Value;
			string? urlApi = _config.GetSection("UrlApiAdmin").Value;
			using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
			{
				//    client.DefaultRequestHeaders.Add("Key-Domain", apiKey);
				HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44333/api/Color/id?id={id}");
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


		public async Task<JsonResult> DeleteColor2(Guid id)
		{
			try
			{
				string apiKey = _config.GetSection("TokenGetApiAdmin").Value;
				string urlApi = _config.GetSection("UrlApiAdmin").Value;

				using (HttpClient client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin"))
				{
					// Gửi yêu cầu DELETE với id trong URL
					//HttpResponseMessage response = await client.DeleteAsync($"{urlApi}/api/Color/{id}");
					HttpResponseMessage response = await client.DeleteAsync($"/api/Color/id?id={id}");

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
