using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shop_API.Repository
{
    public class PagingRepository : IPagingRepository     /* public class PagingRepository<T> : IPagingRepository<T> where T : class*/
    { 
        private readonly ApplicationDbContext _context;
        //public static int PAGE_SIZE { get; set; } = 20;
        public PagingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<PagingDto> GetAll(string search)
        {
            var allList = _context.ProductTypes.Where(pt => pt.Name.Contains(search));

            var result = allList.Select(pt => new PagingDto
            {
                Id = pt.Id,
                Name = pt.Name,
                Status = pt.Status,
            });

            return result.ToList();
        }


        public  List<PagingDto> GetAll(string search, double? from, double? to, string sortBy, int page)
        
        {
            //var allProducts = _context.ProductTypes.Include(pt => pt.Name).AsQueryable();


            var allProducts = _context.ProductTypes.Where(x=>x.Status>0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(pt => pt.Name.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.Status >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.Status <= to);
            }
            #endregion

            #region Sorting
            //Default sort by Name (TenHh)
            allProducts = allProducts.OrderBy(hh => hh.Name);
            if (!string.IsNullOrEmpty(sortBy))
            {
                //switch (sortBy)
                //{
                   /* case "tenhh_desc":*/ allProducts = allProducts.OrderByDescending(hh => hh.Name); /*break;*/
                //}
            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allProducts.Count();


            return allProducts.Select(hh => new PagingDto
            {
                Id = hh.Id,
                Name = hh.Name,
                Status = hh.Status
            }).ToList();
        }

    }
}
