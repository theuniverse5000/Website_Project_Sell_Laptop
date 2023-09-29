using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IProductDetailRepository
    {
        Task<bool> Create(ProductDetail obj);
        Task<bool> Update(ProductDetail obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<ProductDetail>> GetAll();
        int GetCountProductDetail(string codeProductDetail);// Truy vấn ra số lượng sản phẩm là số lượng serial
        Task<IEnumerable<ProductDetailDto>> GetProductDetail(string? search, double? from, double? to, string? sortBy, int page = 1);
        Task<IEnumerable<ProductDetailDto>> GetProductDetailsByPromotionType(string promotionType);
        Task<bool> UpdateSoLuong(Guid id, int soLuong);
        Task<bool> Update(ProductDetailDto productDetail);
    }
}
