using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ColorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ColorRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public async Task<bool> Create(Color obj)
        {
            var checkMa = await _dbContext.Colors.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa == true)
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
                color.TrangThai = obj.TrangThai;
                _dbContext.Colors.Update(color);
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
