using Microsoft.AspNetCore.Mvc;
using Shop_API.Service.IService;
using Shop_Models.Dto;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ReponseDto _reponse;
        public UtilityController(IEmailService emailService)
        {
            _emailService = emailService;
            _reponse = new ReponseDto();
        }
        [HttpPost]
        public ReponseDto SendEmail(EmailDto request)
        {
            if (_emailService.SendEmail(request))
            {
                _reponse.Result = request;
                _reponse.Code = 200;
                return _reponse;
            }
            _reponse.Result = null;
            _reponse.IsSuccess = false;
            _reponse.Code = 404;
            _reponse.Message = "Xảy ra lỗi khi gửi email mời bạn kiểm tra lại";
            return _reponse;
        }
    }
}
