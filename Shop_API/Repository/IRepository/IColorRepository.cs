using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IColorRepository
    {
        Task<bool> Create(Color obj);
        Task<bool> Update(Color obj);
        Task<bool> Delete(Guid id);
        Task<List<Color>> GetAllColors();
        Task<Color> GetById(Guid id);
        Task<ResponseDto> CreateReturnDto(Color obj);
        Task<ResponseDto> UpdateReturnDto(Color obj);
    }
}
