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
        private readonly ReponseDto _reponseDto;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _reponseDto = new ReponseDto();
        }
        [HttpGet]
        public async Task<ReponseDto> GetAllUser()
        {
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
        public async Task<ReponseDto> CreateUser(User obj)
        {
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
        public async Task<ReponseDto> UpdateUser(User obj)
        {
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
        public async Task<ReponseDto> DeleteUser(Guid id)
        {
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
