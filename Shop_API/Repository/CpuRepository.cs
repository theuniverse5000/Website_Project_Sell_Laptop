using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class CpuRepository : ICpuRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CpuRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Cpu obj)
        {
            var checkMa = await _dbContext.Cpus.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _dbContext.Cpus.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<ResponseDto> CreateReturnDto(Cpu obj)
        {
            obj.Ma = string.Join("", obj.Ma.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            obj.Ten = string.Join("", obj.Ten.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

            var checkMa = await _dbContext.Cpus.AnyAsync(x => x.Ma == obj.Ma);
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
                await _dbContext.Cpus.AddAsync(obj);
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
        public async Task<bool> Delete(Guid id)
        {
            var ram = await _dbContext.Cpus.FindAsync(id);
            if (ram == null)
            {
                return false;
            }
            try
            {
                ram.TrangThai = 0;
                _dbContext.Cpus.Update(ram);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Cpu>> GetAllCpus()
        {
            var list = await _dbContext.Cpus.ToListAsync();// lấy tất cả ram
            var listCpu = list.Where(x => x.TrangThai != 0).ToList();// lấy tất cả ram với điều kiện trạng thái khác 0
            return listCpu;
        }

        public async Task<Cpu> GetById(Guid id)
        {
            var result = await _dbContext.Cpus.FindAsync(id);
            return result;
        }

        public async Task<bool> Update(Cpu obj)
        {
            var cpu = await _dbContext.Cpus.FindAsync(obj.Id);
            if (cpu == null)
            {
                return false;
            }
            try
            {
                cpu.Ten = obj.Ten;
                //cpu.TrangThai = obj.TrangThai;
                _dbContext.Cpus.Update(cpu);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

