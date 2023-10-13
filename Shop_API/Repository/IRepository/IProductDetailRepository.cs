using Shop_API.RequestModel;
using Shop_Models.Dto;
using Shop_Models.Entities;
using Shop_Models.HelpperModel;
namespace Shop_API.Repository.IRepository
{
    public interface IProductDetailRepository
    {
        Task<bool> Create(ProductDetail obj);
        Task<bool> Update(ProductDetail obj);
        Task<bool> Delete(Guid id);
        Task<ProductDetail> GetByCode(string code);
        Task<Pagination<ProductDetail>> GetAll(ProductDetailQueryModel productDetailQueryModel);
        int GetCountProductDetail(string codeProductDetail);// Truy vấn ra số lượng sản phẩm là số lượng serial
        Task<IEnumerable<ProductDetailDto>> GetProductDetail(string? search, double? from, double? to, string? sortBy, int page = 1);
        Task<IEnumerable<ProductDetailDto>> GetProductDetailPubic(string? search, string? productType, double? from, double? to, string? sortBy, int page = 1);
        Task<IEnumerable<ProductDetailDto>> GetProductDetailsByPromotionType(string promotionType);
        Task<ProductDetailDto> GetProductDetail();
        Task<bool> UpdateSoLuong(Guid id, int soLuong);
        Task<bool> Update(ProductDetailDto productDetail);
    }
}
