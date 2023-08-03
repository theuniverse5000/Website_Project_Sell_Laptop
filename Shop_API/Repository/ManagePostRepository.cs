using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ManagePostRepository : IManagePostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ManagePostRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> Create(ManagePost managePost)
        {
            if (managePost == null)
            {
                return false;
            }
            try
            {
                await _dbContext.ManagePosts.AddAsync(managePost);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            var obj = await _dbContext.ManagePosts.FindAsync(Id);
            if (obj == null)
            {
                return false;
            }
            try
            {
                obj.Status = false;
                _dbContext.ManagePosts.Update(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ManagePost>> GetAllManagePosts()
        {
            var list = await _dbContext.ManagePosts.ToListAsync();
            var listMP = list.Where(x => x.Status != false).ToList();
            return listMP;
        }

        public async Task<bool> Update(ManagePost managePost)
        {
            var obj = await _dbContext.ManagePosts.FindAsync(managePost.Id);
            if (obj == null)
            {
                return false;
            }
            try
            {
                obj.Code = managePost.Code;
                obj.CreateDate = managePost.CreateDate;
                obj.LinkImage = managePost.LinkImage;
                obj.Status = managePost.Status;
                obj.Description = managePost.Description;
                _dbContext.ManagePosts.Update(obj);
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
