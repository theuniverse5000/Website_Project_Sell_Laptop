using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IGiamGiaRepository
    {
        Task<bool> Create(GiamGia obj);
        Task<bool> Update(GiamGia obj);
        Task<bool> Delete(Guid id);
        Task<List<GiamGia>> GetAllGiamGias();
        Task<GiamGia> GetGiamGiaByPromotionType(string promotionType);
        Task<ResponseDto> CreateReturnDto(GiamGia obj);


    }
}
