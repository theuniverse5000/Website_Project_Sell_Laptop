using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class HardDriveRepository : IHardDriveRepository
    {
        private readonly ApplicationDbContext _context;

        public HardDriveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(HardDrive obj)
        {
            var checkMa = await _context.HardDrives.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa)
            {
                return false;
            }
            try
            {
                await _context.HardDrives.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseDto> CreateReturnDto(HardDrive obj)
        {
            obj.Ma = string.Join("", obj.Ma.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            obj.ThongSo = string.Join("", obj.ThongSo.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

            var checkMa = await _context.HardDrives.AnyAsync(x => x.Ma == obj.Ma);
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
                await _context.HardDrives.AddAsync(obj);
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

        public async Task<bool> Delete(Guid id)
        {
            var hardDrive = await _context.HardDrives.FindAsync(id);
            if (hardDrive == null)
            {
                return false;
            }
            try
            {
                hardDrive.TrangThai = 0;
                _context.HardDrives.Update(hardDrive);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<HardDrive>> GetAllHardDrives()
        {
            var list = await _context.HardDrives.ToListAsync();
            var listHardDrives = list.Where(x => x.TrangThai != 0).ToList();
            return listHardDrives;
        }

        public async Task<HardDrive> GetById(Guid id)
        {
            var result = await _context.HardDrives.FindAsync(id);
            return result;
        }

        public async Task<bool> Update(HardDrive obj)
        {
            var hardDrive = await _context.HardDrives.FindAsync(obj.Id);
            if (hardDrive == null)
            {
                return false;
            }
            try
            {
                hardDrive.ThongSo = obj.ThongSo;
                //hardDrive.TrangThai = obj.TrangThai;
                _context.HardDrives.Update(hardDrive);
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
