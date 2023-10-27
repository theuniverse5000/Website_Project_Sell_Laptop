using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
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


        public List<PagingDto> GetAll(string search, double? from, double? to, string sortBy, int page)

        {
            //var allProducts = _context.ProductTypes.Include(pt => pt.Name).AsQueryable();


            var allProducts = _context.ProductTypes.Where(x => x.Status > 0).AsQueryable();


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
                /* case "tenhh_desc":*/
                allProducts = allProducts.OrderByDescending(hh => hh.Name); /*break;*/
                //}
            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            //int totalCount = allProducts.Count();


            return allProducts.Select(hh => new PagingDto
            {
                Id = hh.Id,
                Name = hh.Name,
                Status = hh.Status
            }).ToList();
        }

        public List<PagingDto> GetAllColor(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allColors = _context.Colors.Where(x => x.TrangThai > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allColors = allColors.Where(pt => pt.Name.Contains(search));
            }
            if (from.HasValue)
            {
                allColors = allColors.Where(hh => hh.TrangThai >= from);
            }
            if (to.HasValue)
            {
                allColors = allColors.Where(hh => hh.TrangThai <= to);
            }
            #endregion

            #region Sorting
            //Default sort by Name (TenHh)
            allColors = allColors.OrderBy(hh => hh.Name);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allColors = allColors.OrderByDescending(hh => hh.Name); /*break;*/

            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allColors.Count();


            return allColors.Select(hh => new PagingDto
            {
                Id = hh.Id,
                Ma = hh.Ma,
                Name = hh.Name,
            }).ToList();
        }


        public List<PagingDto> GetAllScreen(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allScreens = _context.Screens.Where(x => x.TrangThai > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allScreens = allScreens.Where(pt => pt.Ma.Contains(search));
                //allScreens = allScreens.Where(pt => pt.ChatLieu.Contains(search));
            }

            if (from.HasValue)
            {
                allScreens = allScreens.Where(hh => hh.TrangThai >= from);
            }
            if (to.HasValue)
            {
                allScreens = allScreens.Where(hh => hh.TrangThai <= to);
            }
            #endregion

            #region Sorting
            //Default sort by Name (TenHh)
            allScreens = allScreens.OrderBy(hh => hh.Ma);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allScreens = allScreens.OrderByDescending(hh => hh.Ma); /*break;*/

            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allScreens.Count();


            return allScreens.Select(hh => new PagingDto
            {
                Id = hh.Id,
                Ma = hh.Ma,
                KichCo = hh.KichCo,
                TanSo = hh.TanSo,
                ChatLieu = hh.ChatLieu,
            }).ToList();
        }

        public List<PagingDto> GetAllRam(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allRams = _context.Rams.Where(x => x.TrangThai > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allRams = allRams.Where(pt => pt.ThongSo.Contains(search));
            }
            if (from.HasValue)
            {
                allRams = allRams.Where(hh => hh.TrangThai >= from);
            }
            if (to.HasValue)
            {
                allRams = allRams.Where(hh => hh.TrangThai <= to);
            }
            #endregion

            #region Sorting
            //Default sort by ThongSo 
            allRams = allRams.OrderBy(hh => hh.ThongSo);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allRams = allRams.OrderByDescending(hh => hh.ThongSo); /*break;*/

            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allRams.Count();


            return allRams.Select(hh => new PagingDto
            {
                Id = hh.Id,
                Ma = hh.Ma,
                ThongSo = hh.ThongSo,
            }).ToList();
        }
        public List<PagingDto> GetAllHardDrive(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allRams = _context.HardDrives.Where(x => x.TrangThai > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allRams = allRams.Where(pt => pt.ThongSo.Contains(search));
            }
            if (from.HasValue)
            {
                allRams = allRams.Where(hh => hh.TrangThai >= from);
            }
            if (to.HasValue)
            {
                allRams = allRams.Where(hh => hh.TrangThai <= to);
            }
            #endregion

            #region Sorting
            //Default sort by ThongSo 
            allRams = allRams.OrderBy(hh => hh.ThongSo);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allRams = allRams.OrderByDescending(hh => hh.ThongSo); /*break;*/

            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allRams.Count();


            return allRams.Select(hh => new PagingDto
            {
                Id = hh.Id,
                Ma = hh.Ma,
                ThongSo = hh.ThongSo,
            }).ToList();
        }

        public List<PagingDto> GetAllCardVGA(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allRams = _context.CardVGAs.Where(x => x.TrangThai > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allRams = allRams.Where(pt => pt.ThongSo.Contains(search));
            }
            if (from.HasValue)
            {
                allRams = allRams.Where(hh => hh.TrangThai >= from);
            }
            if (to.HasValue)
            {
                allRams = allRams.Where(hh => hh.TrangThai <= to);
            }
            #endregion

            #region Sorting
            //Default sort by ThongSo 
            allRams = allRams.OrderBy(hh => hh.ThongSo);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allRams = allRams.OrderByDescending(hh => hh.ThongSo); /*break;*/

            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allRams.Count();


            return allRams.Select(hh => new PagingDto
            {
                Id = hh.Id,
                Ma = hh.Ma,
                Ten = hh.Ten,
                ThongSo = hh.ThongSo,
            }).ToList();
        }
        public List<PagingDto> GetAllManufacturer(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allManufacturer = _context.Manufacturers.Where(x => x.Status > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allManufacturer = allManufacturer.Where(pt => pt.Name.Contains(search));
            }
            //if (from.HasValue)
            //{
            //    allRams = allRams.Where(hh => hh.Status >= from);
            //}
            //if (to.HasValue)
            //{
            //    allRams = allRams.Where(hh => hh.Status <= to);
            //}
            #endregion

            #region Sorting
            //Default sort by Name 
            allManufacturer = allManufacturer.OrderBy(hh => hh.Name);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allManufacturer = allManufacturer.OrderByDescending(hh => hh.Name); /*break;*/

            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allManufacturer.Count();


            return allManufacturer.Select(hh => new PagingDto
            {
                Id = hh.Id,
                Name = hh.Name,
                LinkImage = hh.LinkImage,
            }).ToList();
        }

        public List<PagingDto> GetAllCpu(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allCpu = _context.Cpus.Where(x => x.TrangThai > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allCpu = allCpu.Where(pt => pt.Ten.Contains(search));
            }
            //if (from.HasValue)
            //{
            //    allRams = allRams.Where(hh => hh.Status >= from);
            //}
            //if (to.HasValue)
            //{
            //    allRams = allRams.Where(hh => hh.Status <= to);
            //}
            #endregion

            #region Sorting
            //Default sort by Name 
            allCpu = allCpu.OrderBy(hh => hh.Ten);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allCpu = allCpu.OrderByDescending(hh => hh.Ten); /*break;*/

            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allCpu.Count();


            return allCpu.Select(hh => new PagingDto
            {
                Id = hh.Id,
                Ten = hh.Ten,
                Ma = hh.Ma,
                Status = hh.TrangThai,
            }).ToList();
        }

        public List<ProductDto> GetAllProduct(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allProduct = _context.Products.Where(x => x.Status > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProduct = allProduct.Where(pt => pt.Name.Contains(search));
            }
            //if (from.HasValue)
            //{
            //    allRams = allRams.Where(hh => hh.Status >= from);
            //}
            //if (to.HasValue)
            //{
            //    allRams = allRams.Where(hh => hh.Status <= to);
            //}
            #endregion

            #region Sorting
            //Default sort by Name 
            allProduct = allProduct.OrderBy(hh => hh.Name);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allProduct = allProduct.OrderByDescending(hh => hh.Name); /*break;*/

            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allProduct.Count();


            return allProduct.Select(hh => new ProductDto
            {
                Id = hh.Id,
                Name = hh.Name,
                ManufacturerId = hh.ManufacturerId,
                ProductTypeId = hh.ProductTypeId,
                ManuName = hh.Manufacturer.Name,
                Status = hh.Status,
            }).ToList();
        }
     

    }
}
