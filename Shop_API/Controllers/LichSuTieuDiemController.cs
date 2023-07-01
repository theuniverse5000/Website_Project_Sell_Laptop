﻿using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuTieuDiemController : ControllerBase
    {
        private readonly ILichSuTieuDiemRepository _lichSuTieuDiem;
        private readonly ReponseDto _reponseDto;
        public LichSuTieuDiemController(ILichSuTieuDiemRepository lichSuTieuDiem)
        {
            _lichSuTieuDiem = lichSuTieuDiem;
            _reponseDto = new ReponseDto();
        }
        [HttpGet]
        public async Task<ReponseDto> GetAllLichSuTieuDiem()
        {
            var list = await _lichSuTieuDiem.GetAllLichSuTieuDiems();
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
        public async Task<ReponseDto> CreateLichSuTieuDiem(LichSuTieuDiem obj)
        {
            if (await _lichSuTieuDiem.Create(obj))
            {
                _reponseDto.Result = obj;
                _reponseDto.Code = 201;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 405;
                _reponseDto.Message = "Lỗi";
                return _reponseDto;
            }

        }
        [HttpPut]
        public async Task<ReponseDto> UpdateLichSuTieuDiem(LichSuTieuDiem obj)
        {
            if (await _lichSuTieuDiem.Update(obj))
            {
                _reponseDto.Result = obj;
                _reponseDto.Code = 200;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 405;
                _reponseDto.Message = "Lỗi";
                return _reponseDto;
            }

        }

        [HttpDelete]
        public async Task<ReponseDto> DeleteLichSuTieuDiem(Guid id)
        {
            if (await _lichSuTieuDiem.Delete(id))
            {
                _reponseDto.Result = null;
                _reponseDto.Code = 204;
                return _reponseDto;
            }
            else
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 405;
                _reponseDto.Message = "Lỗi";
                return _reponseDto;
            }
        }
    }
}
