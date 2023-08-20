using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViDiemController : ControllerBase
    {
        private readonly IViDiemRepository _viDiemRepository;
        private ReponseDto _reponseDto;
        private readonly IConfiguration _config;
        public ViDiemController(IViDiemRepository viDiemRepository, IConfiguration config)
        {
            _viDiemRepository = viDiemRepository;
            _reponseDto = new ReponseDto();
            _config = config;
        }
        [HttpGet]
        public async Task<ReponseDto> GetAllViDiem()
        {

            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 401;
                _reponseDto.Message = "Không có quyền";
                return _reponseDto;
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 401;
                _reponseDto.Message = "Không có quyền";
                return _reponseDto;
            }
            var list = await _viDiemRepository.GetAllViDiems();
            if (list != null)
            {
                _reponseDto.Result = list;
                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 404;
                _reponseDto.Message = "Lỗi";
                return _reponseDto;
            }
        }

        [HttpPost]
        public async Task<ReponseDto> CreateViDiem(ViDiem obj)
        {
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 401;
                _reponseDto.Message = "Không có quyền";
                return _reponseDto;
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 401;
                _reponseDto.Message = "Không có quyền";
                return _reponseDto;
            }
            if (await _viDiemRepository.Create(obj))
            {
                _reponseDto.Result = obj;
                _reponseDto.Code = 201;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 404;
                _reponseDto.Message = "Lỗi";
                return _reponseDto;
            }

        }
        [HttpPut]
        public async Task<ReponseDto> UpdateViDiem(ViDiem obj)
        {
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 401;
                _reponseDto.Message = "Không có quyền";
                return _reponseDto;
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 401;
                _reponseDto.Message = "Không có quyền";
                return _reponseDto;
            }
            if (await _viDiemRepository.Update(obj))
            {
                _reponseDto.Result = obj;
                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 404;
                _reponseDto.Message = "Lỗi";
                return _reponseDto;
            }

        }

        [HttpDelete]
        public async Task<ReponseDto> DeleteViDiem(Guid id)
        {
            string apiKey = _config.GetSection("ApiKey").Value;
            if (apiKey == null)
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 401;
                _reponseDto.Message = "Không có quyền";
                return _reponseDto;
            }

            var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            if (keyDomain != apiKey.ToLower())
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 401;
                _reponseDto.Message = "Không có quyền";
                return _reponseDto;
            }
            if (await _viDiemRepository.Delete(id))
            {
                _reponseDto.Result = null;
                _reponseDto.Code = 204;
                _reponseDto.Message = "Xóa Thành Công";
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 404;
                _reponseDto.Message = "Thất bại";
                return _reponseDto;
            }
        }
    }
}
