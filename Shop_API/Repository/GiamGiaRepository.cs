using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class GiamGiaRepository : IGiamGiaRepository
    {
        private readonly ApplicationDbContext _context;
        public GiamGiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<GiamGia> GetGiamGiaByPromotionType(string promotionType)
        {
            try
            {
                return await _context.GiamGias
                    .Where(g => g.LoaiGiamGia == promotionType && g.TrangThai != 0)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseDto> CreateReturnDto(GiamGia obj)
        {
            obj.Ma = string.Join("", obj.Ma.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            var checkMa = await _context.GiamGias.AnyAsync(x => x.Ma == obj.Ma);
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
            try
            {
                await _context.GiamGias.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = obj,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thêm thành công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<bool> Create(GiamGia obj)
        {
            var checkMa = await _context.GiamGias.AnyAsync(x => x.Ma == obj.Ma);// tìm mã, trả về true nếu đã có, false nếu chưa có
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.GiamGias.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var giamGia = await _context.GiamGias.FindAsync(id);
            if (giamGia == null)
            {
                return false;
            }
            try
            {
                _context.GiamGias.Remove(giamGia);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<GiamGia>> GetAllGiamGias()
        {
            var allList = await _context.GiamGias.ToListAsync();
            var list = allList.Where(x => x.TrangThai != 0).ToList();
            return list;
        }

        public async Task<bool> Update(GiamGia obj)
        {
            var giamGia = await _context.GiamGias.FindAsync(obj.Id);

            if (giamGia == null)
            {
                return false;
            }
            try
            {
                giamGia.Ten = obj.Ten;
                giamGia.NgayBatDau = obj.NgayBatDau;
                giamGia.NgayKetThuc = obj.NgayKetThuc;
                giamGia.MucGiamGiaPhanTram = obj.MucGiamGiaPhanTram;
                giamGia.MucGiamGiaTienMat = obj.MucGiamGiaTienMat;
                giamGia.LoaiGiamGia = obj.LoaiGiamGia;
                giamGia.TrangThai = obj.TrangThai;
                _context.GiamGias.Update(giamGia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
