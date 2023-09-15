using Shop_API.AppDbContext;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_API.Service
{
    public class GiamGiaHangLoatServices : IGiamGiaHangLoatServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ISanPhamGiamGiaRepository _sanPhamGiamGiaRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IGiamGiaRepository _giamGiaRepository;
        private readonly ReponseDto _reponseDto;

        public GiamGiaHangLoatServices(
            ApplicationDbContext context,
            SanPhamGiamGiaRepository sanPhamGiamGiaRepository,
            ProductDetailRepository productDetailRepository,
            GiamGiaRepository giamGiaRepository)
        {
            _context = context;
            _sanPhamGiamGiaRepository = sanPhamGiamGiaRepository;
            _productDetailRepository = productDetailRepository;
            _giamGiaRepository = giamGiaRepository;
            _reponseDto = new ReponseDto();
        }

        public async Task<ReponseDto> ApplyDiscountByPromotionType(string promotionType, double discountAmount)
        {
            try
            {
                // Lấy thông tin giảm giá từ repository theo loại khuyến mãi
                var giamGia = await _giamGiaRepository.GetGiamGiaByPromotionType(promotionType);

                if (giamGia == null)
                {
                    _reponseDto.IsSuccess = false;
                    _reponseDto.Code = 404; // Not Found
                    _reponseDto.Message = "Không tìm thấy thông tin giảm giá cho loại khuyến mãi này.";
                    return _reponseDto;
                }

                // Lấy danh sách sản phẩm cần áp dụng giảm giá từ repository
                var productDetails = await _productDetailRepository.GetProductDetailsByPromotionType(promotionType);

                if (productDetails == null || !productDetails.Any())
                {
                    _reponseDto.IsSuccess = false;
                    _reponseDto.Code = 404; // Not Found
                    _reponseDto.Message = "Không tìm thấy sản phẩm nào để áp dụng giảm giá cho loại khuyến mãi này.";
                    return _reponseDto;
                }

                DateTime currentDate = DateTime.Now;

                foreach (var productDetail in productDetails)
                {
                    // Kiểm tra xem giảm giá còn hiệu lực
                    if (currentDate >= giamGia.NgayBatDau && currentDate <= giamGia.NgayKetThuc)
                    {
                        // Áp dụng giảm giá cho sản phẩm
                        double newPrice = productDetail.Price - discountAmount;
                        if (newPrice < 0)
                        {
                            newPrice = 0;
                        }

                        productDetail.Price = newPrice;

                        // Lưu thay đổi vào repository
                        await _productDetailRepository.Update(productDetail);
                    }
                }

                _reponseDto.Message = "Áp dụng giảm giá thành công cho các sản phẩm theo loại khuyến mãi.";
                return _reponseDto;
            }
            catch (Exception ex)
            {
                _reponseDto.IsSuccess = false;
                _reponseDto.Code = 500; // Internal Server Error
                _reponseDto.Message = "Đã xảy ra lỗi trong quá trình xử lý: " + ex.Message;
                return _reponseDto;
            }
        }
    }
}
