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
        public List<UserDto> GetAllUser(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allUser = _context.Users.Where(x => x.Status > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allUser = allUser.Where(pt => pt.FullName.Contains(search));
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
            allUser = allUser.OrderBy(hh => hh.FullName);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allUser = allUser.OrderByDescending(hh => hh.FullName); /*break;*/

            }
            #endregion

            //var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProducts, page, PAGE_SIZE);

            //var result = allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            int totalCount = allUser.Count();


            return allUser.Select(hh => new UserDto
            {
                Id = hh.Id,
                Username = hh.UserName,
                Name = hh.FullName,    
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

        public List<SerialDto> GetAllSerial(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allSerial = _context.Serials.Where(x => x.Status > 0).AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allSerial = allSerial.Where(pt => pt.SerialNumber.Contains(search));
            }

            #endregion

            #region Sorting
            //Default sort by Name 
            allSerial = allSerial.OrderBy(hh => hh.SerialNumber);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allSerial = allSerial.OrderByDescending(hh => hh.SerialNumber); /*break;*/

            }
            #endregion

            int totalCount = allSerial.Count();


            return allSerial.Select(hh => new SerialDto
            {
                Id = hh.Id,
                SerialNumber = hh.SerialNumber,
                ProductDetailId = hh.ProductDetailId,
                BillDetailId = hh.BillDetailId,
                ProductDetailCode = hh.ProductDetail.Code,
                BillDetailCode = hh.BillDetail.Code,
            }).ToList();
        }


        public List<Voucher> GetAllVoucherPG(string? search, double? from, double? to, string? sortBy, int? page)
        {
            var allSerial = _context.Vouchers.AsQueryable();


            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allSerial = allSerial.Where(pt => pt.MaVoucher.Contains(search));
            }

            #endregion

            #region Sorting
            //Default sort by Name 
            allSerial = allSerial.OrderBy(hh => hh.TenVoucher);
            if (!string.IsNullOrEmpty(sortBy))
            {

                allSerial = allSerial.OrderByDescending(hh => hh.GiaTri); /*break;*/

            }
            #endregion

            int totalCount = allSerial.Count();


            return allSerial.Select(hh => new Voucher
            {
                Id = hh.Id,
                MaVoucher = hh.MaVoucher,
                TenVoucher = hh.TenVoucher,
                StarDay = hh.StarDay,
                EndDay = hh.EndDay,
                GiaTri = hh.GiaTri,
                SoLuong = hh.SoLuong,
                Status = hh.Status,

            }).ToList();
        }

        public List<SanPhamGiamGiaDto> GetAllSPGGPGs(string? codeProductDetail, string? search, double? from, double? to, string? sortBy, int? page)
        {
            var allSPGG = _context.SanPhamGiamGias.Where(x => x.TrangThai > 0 && (codeProductDetail != null ? x.ProductDetail.Code == codeProductDetail : true)).AsQueryable();

            int totalCount = allSPGG.Count();

            return allSPGG.Select(hh => new SanPhamGiamGiaDto
            {
                Id = hh.Id,
                ProductDetailId = hh.ProductDetailId,
                GiamGiaId = hh.GiamGiaId,
                DonGia = hh.ProductDetail.Price,
                SoTienConLai = hh.ProductDetail.Price - (hh.ProductDetail.Price * Convert.ToInt64(hh.GiamGia.MucGiamGiaPhanTram) / 100),
                TrangThai = hh.TrangThai,
                ProductDetailCode = hh.ProductDetail.Code,
                GiamGiaPhanTram = hh.GiamGia.MucGiamGiaPhanTram,
                GiamGiaCode = hh.GiamGia.Ten,
                ProductDetailName = hh.ProductDetail.Product.Name,
                TenSanPham = hh.ProductDetail.Product.Name + " " + hh.ProductDetail.Code + " " +
                hh.ProductDetail.Cpu.Ten + " " + hh.ProductDetail.Ram.ThongSo + " " +
                hh.ProductDetail.HardDrive.ThongSo + " " + hh.ProductDetail.CardVGA.Ten + " " +
                hh.ProductDetail.CardVGA.ThongSo + " " + hh.ProductDetail.Screen.KichCo + " " +
                hh.ProductDetail.Screen.TanSo + " " + hh.ProductDetail.Screen.ChatLieu,
                TenCpu = hh.ProductDetail.Cpu.Ten,
                ThongSoRam = hh.ProductDetail.Ram.ThongSo,
                ThongSoHardDrive = hh.ProductDetail.HardDrive.ThongSo,
                TenCardVGA = hh.ProductDetail.CardVGA.Ten,
                ThongSoCardVGA = hh.ProductDetail.CardVGA.ThongSo,
                KichCoManHinh = hh.ProductDetail.Screen.KichCo,
                TanSoManHinh = hh.ProductDetail.Screen.TanSo,
                ChatLieuManHinh = hh.ProductDetail.Screen.ChatLieu,
                NameProductType = hh.ProductDetail.Product.Name,
                NameManufacturer = hh.ProductDetail.Product.Manufacturer.Name,
                NameColor = hh.ProductDetail.Color.Name,
                Description = hh.ProductDetail.Description,
                LinkImage = _context.Images
                    .Where(image => image.ProductDetailId == hh.ProductDetailId && image.Ma == "Anh1")
                    .Select(image => image.LinkImage)
                    .FirstOrDefault(),
            }).ToList();
        }

        public List<GiamGia> GetAllGiamGia(string? search, double? from, double? to, string? sortBy, int page)
        {
            var allGiamGia = _context.GiamGias.Where(x => x.TrangThai > 0).AsQueryable();
            int totalCount = allGiamGia.Count();
            return allGiamGia.Select(hh => new GiamGia
            {
                Id = hh.Id,
                Ma = hh.Ma,
                Ten = hh.Ten,
                NgayBatDau = hh.NgayBatDau,
                NgayKetThuc = hh.NgayKetThuc,
                MucGiamGiaPhanTram = hh.MucGiamGiaPhanTram,
                MucGiamGiaTienMat = hh.MucGiamGiaTienMat,
                LoaiGiamGia = hh.LoaiGiamGia,
                TrangThai = hh.TrangThai

            }).ToList();
        }


    }
}
