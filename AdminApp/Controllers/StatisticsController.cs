
using Microsoft.AspNetCore.Mvc;
using Shop_Models.Entities;

namespace AdminApp.Controllers
{
    
    public class StatisticsController : Controller
    {
        private readonly ILogger<StatisticsController> _logger;

        HttpClient http;

        public StatisticsController(ILogger<StatisticsController> logger)
        {
            _logger = logger;

            http = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBills()
        {
            string apiKey = "c825f522ca3f4a5f935f2d2ac3e05e25";

            http.DefaultRequestHeaders.Add("api-key", apiKey);

            var api = "https://localhost:7286/api/Bill";
            //var res = await http.GetFromJsonAsync<List<Bill>>(api);

            List<Bill> res = new List<Bill>
            {
                new Bill { Id = Guid.NewGuid(), InvoiceCode = "HD01", CreateDate = DateTime.Now.AddDays(-4), PhoneNumber = "0356889008", FullName = "Phạm Quang Vinh", Address = "Hưng Yên", Status = 1, UserId = Guid.NewGuid(), VoucherId = Guid.NewGuid(), },
                new Bill { Id = Guid.NewGuid(), InvoiceCode = "HD02", CreateDate = DateTime.Now.AddDays(-2), PhoneNumber = "0356889008", FullName = "Phạm Quang Vinh", Address = "Hưng Yên", Status = 1, UserId = Guid.NewGuid(), },
                new Bill { Id = Guid.NewGuid(), InvoiceCode = "HD03", CreateDate = DateTime.Now.AddDays(-2), PhoneNumber = "0356889008", FullName = "Phạm Quang Vinh", Address = "Hưng Yên", Status = 1, UserId = Guid.NewGuid(), VoucherId = Guid.NewGuid(), },
                new Bill { Id = Guid.NewGuid(), InvoiceCode = "HD04", CreateDate = DateTime.Now, PhoneNumber = "0356889008", FullName = "Phạm Quang Vinh", Address = "Hưng Yên", Status = 1, UserId = Guid.NewGuid(), VoucherId = Guid.NewGuid(), },
                new Bill { Id = Guid.NewGuid(), InvoiceCode = "HD05", CreateDate = DateTime.Now, PhoneNumber = "0356889008", FullName = "Phạm Quang Vinh", Address = "Hưng Yên", Status = 0, UserId = Guid.NewGuid(), VoucherId = Guid.NewGuid(), },

            };

            List<int> arr = new List<int> { 2, 3, 4 };
            ViewBag.Message = arr;
            return View(res);
        }
    }
}