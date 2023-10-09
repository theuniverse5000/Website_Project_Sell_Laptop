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
        private readonly ResponseDto _reponse;
        private readonly IConfiguration _config;
        public UtilityController(IEmailService emailService, IConfiguration config)
        {
            _emailService = emailService;
            _reponse = new ResponseDto();
            _config = config;
        }
        [HttpPost]
        public ResponseDto SendEmail(EmailDto request)
        {

            //string apiKey = _config.GetSection("ApiKey").Value;
            //if (apiKey == null)
            //{
            //    _reponse.Result = null;
            //    _reponse.IsSuccess = false;
            //    _reponse.Code = 404;
            //    _reponse.Message = "Không có quyền";
            //    return _reponse;
            //}

            //var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            //if (keyDomain != apiKey.ToLower())
            //{
            //    _reponse.Result = null;
            //    _reponse.IsSuccess = false;
            //    _reponse.Code = 404;
            //    _reponse.Message = "Không có quyền";
            //    return _reponse;
            //}
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
