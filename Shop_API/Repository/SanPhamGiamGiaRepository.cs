using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class SanPhamGiamGiaRepository : ISanPhamGiamGiaRepository
    {
        private readonly ApplicationDbContext _context;
        public SanPhamGiamGiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDto> Create(SanPhamGiamGia obj)
        {
            var checkMa = await _context.SanPhamGiamGias.AnyAsync(x => x.ProductDetailId == obj.ProductDetailId);
            if (obj == null || checkMa == true)
            {

                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Trùng Mã",

                };
            }
            if (obj.ProductDetailId == Guid.Empty && obj.GiamGiaId == Guid.Empty)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Bắt buộc phải chọn mã giảm giá và mã sản phẩm",

                };
            }
           

            try
            {

                await _context.SanPhamGiamGias.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = obj,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",

                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi hệ thống",

                };
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var sanPhamGiamGia = await _context.SanPhamGiamGias.FindAsync(id);
            if (sanPhamGiamGia == null)
            {
                return false;
            }
            try
            {
                _context.SanPhamGiamGias.Remove(sanPhamGiamGia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<SanPhamGiamGia>> GetAllSanPhamGiamGias()
        {
            return await _context.SanPhamGiamGias.ToListAsync();
        }

        public async Task<bool> Update(SanPhamGiamGia obj)
        {
            var sanPhamGiamGia = await _context.SanPhamGiamGias.FindAsync(obj.Id);

            if (sanPhamGiamGia == null)
            {
                return false;
            }
            try
            {
                //sanPhamGiamGia.DonGia = obj.DonGia;
                //sanPhamGiamGia.SoTienConLai = obj.SoTienConLai;
                ////sanPhamGiamGia.TrangThai = obj.TrangThai;
                sanPhamGiamGia.ProductDetailId = obj.ProductDetailId;
                sanPhamGiamGia.GiamGiaId = obj.GiamGiaId;

                _context.SanPhamGiamGias.Update(sanPhamGiamGia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<SanPhamGiamGia> GetById(Guid guid)
        {
            return await _context.SanPhamGiamGias.FindAsync(guid);
        }
    }
}
