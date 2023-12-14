using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class CardVGARepository : ICardVGARepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ResponseDto _responseDto;
        public CardVGARepository(ApplicationDbContext context)
        {
            _context = context;
            _responseDto = new ResponseDto();
        }
        public async Task<bool> Create(CardVGA obj)
        {
            var checkMa = await _context.CardVGAs.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.CardVGAs.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseDto> CreateReturnDto(CardVGA obj)
        {
            obj.Ma = string.Join("", obj.Ma.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            obj.Ten = string.Join("", obj.Ten.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            obj.ThongSo = string.Join("", obj.ThongSo.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

            var checkMa = await _context.CardVGAs.AnyAsync(x => x.Ma == obj.Ma);
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
                await _context.CardVGAs.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = obj,
                    IsSuccess = false,
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

        public async Task<ResponseDto> UpdateReturnDto(CardVGA obj)
        {
            obj.Ma = string.Join("", obj.Ma.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            obj.Ten = string.Join("", obj.Ten.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            obj.ThongSo = string.Join("", obj.ThongSo.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

            var cardVGA = await _context.CardVGAs.FindAsync(obj.Id);
            if (cardVGA == null)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 405,
                    Message = "Không tìm thấy hoặc đã bị xóa",
                };
            }
            try
            {
                cardVGA.Ten = obj.Ten;
                cardVGA.ThongSo = obj.ThongSo;
                //cardVGA.TrangThai = obj.TrangThai;
                _context.CardVGAs.Update(cardVGA);
                await _context.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = obj,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Cập nhật thành công",
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


        public async Task<bool> Delete(Guid id)
        {
            var cardVGA = await _context.CardVGAs.FindAsync(id);
            if (cardVGA == null)
            {
                return false;
            }
            try
            {
                cardVGA.TrangThai = 0;
                _context.CardVGAs.Update(cardVGA);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<CardVGA>> GetAllCardVGA()
        {
            var list = await _context.CardVGAs.ToListAsync();
            var listCardVGA = list.Where(x => x.TrangThai != 0).ToList();
            return listCardVGA;
        }

        public async Task<CardVGA> GetById(Guid id)
        {
            var result = await _context.CardVGAs.FindAsync(id);
            return result;
        }

        public async Task<bool> Update(CardVGA obj)
        {
            var cardVGA = await _context.CardVGAs.FindAsync(obj.Id);
            if (cardVGA == null)
            {
                return false;
            }
            try
            {
                cardVGA.Ten = obj.Ten;
                cardVGA.ThongSo = obj.ThongSo;
                //cardVGA.TrangThai = obj.TrangThai;
                _context.CardVGAs.Update(cardVGA);
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
