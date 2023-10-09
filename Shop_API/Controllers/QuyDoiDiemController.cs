using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuyDoiDiemController : ControllerBase
    {
        private readonly IQuyDoiDiemRepository _quyDoiDiemRepository;
        private readonly ResponseDto _reponseDto;
        public QuyDoiDiemController(IQuyDoiDiemRepository quyDoiDiemRepository)
        {
            _quyDoiDiemRepository = quyDoiDiemRepository;
            _reponseDto = new ResponseDto();
        }
        [HttpGet]
        public async Task<ResponseDto> GetAllQuyDoi()
        {
            var list = await _quyDoiDiemRepository.GetAllQuyDoiDiems();
            if (list != null)
            {
                _reponseDto.Result = list;
                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Rỗng";
                _reponseDto.Code = 404;
                return _reponseDto;
            }
        }

        [HttpPost]
        public async Task<ResponseDto> CreateQuyDoiDiem(QuyDoiDiem obj)
        {
            obj.Id = Guid.NewGuid();
            if (await _quyDoiDiemRepository.Create(obj))
            {
                _reponseDto.Result = obj;
                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Lỗi";
                _reponseDto.Code = 405;
                return _reponseDto;
            }
        }

        [HttpPut]
        public async Task<ResponseDto> UpdateQuyDoiDiem(QuyDoiDiem obj)
        {
            if (await _quyDoiDiemRepository.Update(obj))
            {
                _reponseDto.Result = obj;
                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Lỗi";
                _reponseDto.Code = 405;
                return _reponseDto;
            }
        }

        [HttpDelete]
        public async Task<ResponseDto> DeleteQuyDoiDiem(Guid id)
        {
            if (await _quyDoiDiemRepository.Delete(id))
            {

                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.Result = null;
                _reponseDto.IsSuccess = false;
                _reponseDto.Message = "Lỗi";
                _reponseDto.Code = 405;
                return _reponseDto;
            }
        }


    }
}
