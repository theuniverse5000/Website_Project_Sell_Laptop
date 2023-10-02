using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Cart obj)
        {
            var checkId = await _context.Carts.AnyAsync(x => x.UserId == obj.UserId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.Carts.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(Guid id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Cart>> GetAll()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> GetCartByUsername(string username)
        {
            try
            {
                var listUser = await _context.Users.ToListAsync();
                Guid idUser = listUser.FirstOrDefault(x => x.UserName == username).Id;
                var cart = await _context.Carts.FindAsync(idUser);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            // Truyền vào tên tào khoản của người dùng
            try
            {
                var listUser = await _context.Users.ToListAsync();// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                Guid idUser = listUser.FirstOrDefault(x => x.UserName == username).Id;// Lấy ra ìd người dùng
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();// Khởi tao 1 list
                cartItem = (
                           // Join các bảng lại để lấy dữ liệu
                           from x in await _context.Carts.ToListAsync()
                           join y in await _context.CartDetails.ToListAsync() on x.UserId equals y.CartId
                           join a in await _context.ProductDetails.ToListAsync() on y.ProductDetailId equals a.Id
                           join b in await _context.Rams.ToListAsync() on a.RamId equals b.Id
                           join c in await _context.Cpus.ToListAsync() on a.CpuId equals c.Id
                           join d in await _context.HardDrives.ToListAsync() on a.HardDriveId equals d.Id
                           join e in await _context.Colors.ToListAsync() on a.ColorId equals e.Id
                           join f in await _context.CardVGAs.ToListAsync() on a.CardVGAId equals f.Id
                           join g in await _context.Screens.ToListAsync() on a.ScreenId equals g.Id
                           // join h in await _context.Images.ToListAsync() on a.Id equals h.ProductDetailId
                           join i in await _context.Products.ToListAsync() on a.ProductId equals i.Id
                           join k in await _context.Manufacturers.ToListAsync() on i.ManufacturerId equals k.Id
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = a.Id,
                               MaProductDetail = a.Code,
                               Price = a.Price,
                               Description = a.Description,
                               ThongSoRam = b.ThongSo,
                               MaRam = b.Ma,
                               TenCpu = c.Ten,
                               MaCpu = c.Ma,
                               ThongSoHardDrive = d.ThongSo,
                               MaHardDrive = d.Ma,
                               NameColor = e.Name,
                               MaColor = e.Ma,
                               MaCardVGA = f.Ma,
                               TenCardVGA = f.Ten,
                               ThongSoCardVGA = f.ThongSo,
                               MaManHinh = g.Ma,
                               KichCoManHinh = g.KichCo,
                               TanSoManHinh = g.TanSo,
                               ChatLieuManHinh = g.ChatLieu,
                               NameProduct = i.Name,
                               NameManufacturer = k.Name
                               //  LinkImage = h.LinkImage
                           }
                    ).ToList();
                return cartItem.Where(x => x.UserId == idUser);// Trả về list với điểu kiện 
            }
            catch (Exception)
            {
                // Nếu idUser bị null tức là không tìm thấy, sẽ xảy ra Exception
                // Sau đó trả về null
                return null;
            }

        }
        //public async Task<IEnumerable<CartItemDto>> GetAllCarts()
        //{

        //    // Dùng CartItemDto để hiển thị kết quả
        //    List<CartItemDto> cartItem = new List<CartItemDto>();// Khởi tao 1 list
        //    cartItem = (
        //               // Join các bảng lại để lấy dữ liệu
        //               from x in await _context.Carts.ToListAsync()
        //               join y in await _context.CartDetails.ToListAsync() on x.UserId equals y.CartId
        //               join a in await _context.ProductDetails.ToListAsync() on y.ProductDetailId equals a.Id
        //               join b in await _context.Rams.ToListAsync() on a.RamId equals b.Id
        //               join c in await _context.Cpus.ToListAsync() on a.CpuId equals c.Id
        //               join d in await _context.HardDrives.ToListAsync() on a.HardDriveId equals d.Id
        //               join e in await _context.Colors.ToListAsync() on a.ColorId equals e.Id
        //               join f in await _context.CardVGAs.ToListAsync() on a.CardVGAId equals f.Id
        //               join g in await _context.Screens.ToListAsync() on a.ScreenId equals g.Id
        //               // join h in await _context.Images.ToListAsync() on a.Id equals h.ProductDetailId
        //               join i in await _context.Products.ToListAsync() on a.ProductId equals i.Id
        //               join k in await _context.Manufacturers.ToListAsync() on i.ManufacturerId equals k.Id
        //               select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
        //               {
        //                   Id = y.Id,
        //                   UserId = x.UserId,
        //                   Quantity = y.Quantity,
        //                   IdProductDetails = a.Id,
        //                   MaProductDetail = a.Ma,
        //                   Price = a.Price,
        //                   Description = a.Description,
        //                   ThongSoRam = b.ThongSo,
        //                   MaRam = b.Ma,
        //                   TenCpu = c.Ten,
        //                   MaCpu = c.Ma,
        //                   ThongSoHardDrive = d.ThongSo,
        //                   MaHardDrive = d.Ma,
        //                   NameColor = e.Name,
        //                   MaColor = e.Ma,
        //                   MaCardVGA = f.Ma,
        //                   TenCardVGA = f.Ten,
        //                   ThongSoCardVGA = f.ThongSo,
        //                   MaManHinh = g.Ma,
        //                   KichCoManHinh = g.KichCo,
        //                   TanSoManHinh = g.TanSo,
        //                   ChatLieuManHinh = g.ChatLieu,
        //                   NameProduct = i.Name,
        //                   NameManufacturer = k.Name
        //                   //  LinkImage = h.LinkImage
        //               }
        //        ).ToList();
        //    return cartItem;

        //}

        public async Task<bool> Update(Cart obj)
        {
            var cart = await _context.Carts.FindAsync(obj.UserId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.Carts.Update(cart);
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
