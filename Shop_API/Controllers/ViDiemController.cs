using Azure;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository;
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
        private ResponseDto _reponseDto;
        private readonly IConfiguration _config;
        public ViDiemController(IViDiemRepository viDiemRepository, IConfiguration config)
        {
            _viDiemRepository = viDiemRepository;
            _reponseDto = new ResponseDto();
            _config = config;
        }
        [HttpGet]
        public async Task<ResponseDto> GetAllViDiem()
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
        public async Task<ResponseDto> CreateViDiem(ViDiem obj)
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
        public async Task<ResponseDto> UpdateViDiem(ViDiem obj)
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
        public async Task<ResponseDto> DeleteViDiem(Guid id)
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


        [HttpGet("GetPointWallet")]
        public async Task<IActionResult> GetPointWallet(string? search, double? from, double? to, string? sortBy, int page)
        {
            //string apiKey = _config.GetSection("ApiKey").Value;
            //if (apiKey == null)
            //{
            //    return Unauthorized();
            //}

            //var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
            //if (keyDomain != apiKey.ToLower())
            //{
            //    return Unauthorized();
            //}
            //_response.Result = _iPagingRepository.GetProductDtos(search, from, to, sortBy, page);
            //_response.Count = _iPagingRepository.GetProductDtos(search, from, to, sortBy, page).Count;

            _reponseDto.Result = await _viDiemRepository.GetAllPointWallet(search, from, to, sortBy, page);
            _reponseDto.Count = _viDiemRepository.GetAllPointWallet(search, from, to, sortBy, page).Result.Count();
            return Ok(_reponseDto);
        }
    }
}
