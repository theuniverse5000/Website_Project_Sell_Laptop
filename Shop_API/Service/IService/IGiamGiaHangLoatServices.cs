using Shop_Models.Dto;

namespace Shop_API.Service.IService
{
    public interface IGiamGiaHangLoatServices
    {
        Task<ReponseDto> ApplyDiscountByPromotionType(string promotionType, double discountAmount);
    }
}
