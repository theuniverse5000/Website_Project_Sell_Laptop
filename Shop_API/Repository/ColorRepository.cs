using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ColorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Color obj)
        {
            var checkMa = await _dbContext.Colors.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa == true||obj.Ma==null||obj.Name==null)
            {
                return false;
            }
            try
            {
                await _dbContext.Colors.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var color = await _dbContext.Colors.FindAsync(id);
            if (color == null)
            {
                return false;
            }
            try
            {
                color.TrangThai = 0;
                _dbContext.Colors.Update(color);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Color>> GetAllColors()
        {
            var list = await _dbContext.Colors.ToListAsync();
            var listColor = list.Where(x => x.TrangThai != 0).ToList();
            return listColor;
        }

        public  async Task<Color> GetById(Guid id)
        {
            var result = await _dbContext.Colors.FindAsync(id);
            return result;
        }

        public async Task<bool> Update(Color obj)
        {
            var color = await _dbContext.Colors.FindAsync(obj.Id);
            if (color == null)
            {
                return false;
            }
            try
            {
                color.Name = obj.Name;
                color.Ma = obj.Ma;
                //color.TrangThai = obj.TrangThai;
                _dbContext.Colors.Update(color);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseDto> CreateReturnDto(Color obj)
        {
            obj.Ma = string.Join("", obj.Ma.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            obj.Name = string.Join("", obj.Name.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

            var checkMa = await _dbContext.Colors.AnyAsync(x => x.Ma == obj.Ma);
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
                await _dbContext.Colors.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
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

        public async Task<ResponseDto> UpdateReturnDto(Color obj)
        {
            //obj.Ma = string.Join("", obj.Ma.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            obj.Name = string.Join("", obj.Name.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));        

            var color = await _dbContext.Colors.FindAsync(obj.Id);
            if (color == null)
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
                color.Name = obj.Name;
                //color.TrangThai = obj.TrangThai;
                _dbContext.Colors.Update(color);
                await _dbContext.SaveChangesAsync();
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

    }
}
