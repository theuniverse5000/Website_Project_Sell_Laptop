using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
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
            //var listMP = list.Where(x => x.Status != false).ToList();
            return list;
        }

        public Task<bool> GetByCode(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ManagePost> GetById(Guid Id)
        {
            var getById = await _dbContext.ManagePosts.FindAsync(Id);
            return getById;
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

        public async Task<IEnumerable<ManagePost>> GetManagePostDtos(string? search, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var query = _dbContext.ManagePosts
                .AsNoTracking()
                //.Where(a => a.Status!=false)
                .Select(a => new ManagePost
                {
                    Id = a.Id,
                    Code = a.Code,
                    CreateDate = a.CreateDate,
                    LinkImage = a.LinkImage,
                    Description = a.Description,
                    Status = a.Status,                  

                });

            #region Filltering
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Code.Contains(search));
            }

            if (from.HasValue)
            {
                query = query.Where(x => x.CreateDate >= from);
            }
            if (to.HasValue)
            {
                query = query.Where(x => x.CreateDate <= to);
            }
            #endregion

            #region Sorting
            //if (!string.IsNullOrEmpty(sortBy))
            //{
            //    switch (sortBy)
            //    {
            //        case "nameproduct_desc":
            //            query = query.OrderByDescending(x => x.Code);
            //            break;
            //        case "price_asc":
            //            query = query.OrderBy(x => x.Price);
            //            break;
            //        case "price_desc":
            //            query = query.OrderByDescending(x => x.Price);
            //            break;
            //        default:
            //            break;
            //    }
            //}
            #endregion

            #region Paging
            var pageSize = page;
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > totalPages)
            {
                page = totalPages;
            }

            //query = query.Skip((page - 1) * pageSize).Take(pageSize);
            #endregion
            var result = await query.ToListAsync();
            // Lấy danh sách mã sản phẩm để thực hiện một lần duy nhất truy vấn cơ sở dữ liệu.
            var productCodes = result.Select(p => p.Code).ToList();
            // Gán AvailableQuantity cho từng sản phẩm trong danh sách.
            //foreach (var productDetail in result)
            //{
            //    productDetail.AvailableQuantity = GetCountProductDetail(productDetail.Code);
            //}

            return result;
        }

    }
}
