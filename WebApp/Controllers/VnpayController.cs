using Microsoft.AspNetCore.Mvc;
using Shop_Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class VnpayController : Controller
    {
        private readonly IVnPayService _vnPayService;

        public VnpayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }
    }
}
