using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ResponseDto _reponseDto;
        private readonly IConfiguration _config;
        public UserController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _reponseDto = new ResponseDto();
            _config = config;
        }
        [HttpGet]
        public async Task<ResponseDto> GetAllUser()
        {

            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Bạn không có quyền";
                _reponseDto.Code = 401;
                return _reponseDto;
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Bạn không có quyền";
                _reponseDto.Code = 401;
                return _reponseDto;
            }
            var list = await _userRepository.GetAllUsers();
            if (list != null)
            {
                _reponseDto.Result = list;
                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Lỗi";
                _reponseDto.Code = 404;
                return _reponseDto;
            }
        }

        [HttpPost]
        public async Task<ResponseDto> CreateUser(User obj)
        {
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Bạn không có quyền";
                _reponseDto.Code = 401;
                return _reponseDto;
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Bạn không có quyền";
                _reponseDto.Code = 401;
                return _reponseDto;
            }
            obj.Id = Guid.NewGuid();
            if (await _userRepository.Create(obj))
            {
                _reponseDto.Result = obj;
                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Lỗi";
                _reponseDto.Code = 405;
                return _reponseDto;
            }
        }
        [HttpPut]
        public async Task<ResponseDto> UpdateUser(User obj)
        {
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Bạn không có quyền";
                _reponseDto.Code = 401;
                return _reponseDto;
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Bạn không có quyền";
                _reponseDto.Code = 401;
                return _reponseDto;
            }
            if (await _userRepository.Update(obj))
            {
                _reponseDto.Result = obj;
                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Lỗi";
                _reponseDto.Code = 405;
                return _reponseDto;
            }

        }

        [HttpDelete]
        public async Task<ResponseDto> DeleteUser(Guid id)
        {
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Bạn không có quyền";
                _reponseDto.Code = 401;
                return _reponseDto;
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Bạn không có quyền";
                _reponseDto.Code = 401;
                return _reponseDto;
            }
            if (await _userRepository.Delete(id))
            {

                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Lỗi";
                _reponseDto.Code = 405;
                return _reponseDto;
            }
        }
    }
}
