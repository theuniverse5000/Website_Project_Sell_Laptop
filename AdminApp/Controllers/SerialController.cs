using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    public class SerialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> ImportProductsFromExcel(IFormFile excelFile)
        //{
        //    try
        //    {
        //        using (var package = new ExcelPackage(excelFile.OpenReadStream()))
        //        {
        //            var worksheet = package.Workbook.Worksheets[0];
        //            var rowCount = worksheet.Dimension.Rows;
        //            var products = new List<ProductDetail>();
        //            var client = _httpClientFactory.CreateClient("PhuongThaoHttpAdmin");
        //            //for (int row = 2; row <= rowCount; row++)
        //            //{
        //            //    var product = new ProductDetail
        //            //    {
        //            //        Id = Guid.NewGuid(),
        //            //        Code = worksheet.Cells[row, 1].Text,
        //            //        ImportPrice = float.Parse(worksheet.Cells[row, 2].Text),
        //            //        Price = float.Parse(worksheet.Cells[row, 3].Text),
        //            //        Upgrade = worksheet.Cells[row, 4].Text,
        //            //        Description = worksheet.Cells[row, 5].Text,
        //            //        Status = int.Parse(worksheet.Cells[row, 6].Text),
        //            //        ProductId = Guid.Parse("D29B5AF1-AF18-4A6C-A4C6-82468DDA11B3"),
        //            //        ColorId = null,
        //            //        RamId = null,
        //            //        CpuId = null,
        //            //        HardDriveId = null,
        //            //        ScreenId = null,
        //            //        CardVGAId = null,
        //            //    };
        //            //    products.Add(product);
        //            var result = await client.PostAsJsonAsync($"/api/ProductDetail/CreateMany", products);
        //            //if (result.IsSuccessStatusCode)
        //            //{
        //            //    return RedirectToAction("Index", "ProductDetail");
        //            //}

        //            //return BadRequest(result);
        //            //  }
        //            return RedirectToAction("Index", "ProductDetail");


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error: " + ex.Message);
        //    }
        //}
    }
}
