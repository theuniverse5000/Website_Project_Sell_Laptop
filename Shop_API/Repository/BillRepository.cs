using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDbContext _context;
        public BillRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Bill obj)
        {
            try
            {
                await _context.Bills.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(Bill id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return false;
            }
            try
            {
                bill.Status = 0;
                _context.Bills.Update(bill);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Bill>> GetAll()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task<IEnumerable<BillDetailDto>> GetBillByUsername(string username)
        {
            // Truyền vào tên tào khoản của người dùng
            try
            {
                var listUser = await _context.Users.ToListAsync();// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                Guid idUser = listUser.FirstOrDefault(x => x.Username == username).Id;// Lấy ra ìd người dùng
                // Dùng BillDetailDto để hiển thị kết quả
                List<BillDetailDto> BillDetail = new List<BillDetailDto>();// Khởi tao 1 list
                BillDetail = (
                           // Join các bảng lại để lấy dữ liệu
                           from o in await _context.Users.ToListAsync()
                           join x in await _context.Bills.ToListAsync() on o.Id equals x.UserId
                           join y in await _context.BillDetails.ToListAsync() on x.Id equals y.BillId
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
                           select new BillDetailDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               IdBill = x.Id,
                               SDTKhachHang = x.SDTKhachHang,
                               HoTenKhachHang = x.HoTenKhachHang,
                               DiaChiKhachHang = x.DiaChiKhachHang,
                               UserId = o.Id,
                               //        Quantity = y.Quantity,
                               IdProductDetails = a.Id,
                               MaProductDetail = a.Ma,
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
                return BillDetail.Where(x => x.UserId == idUser);// Trả về list với điểu kiện 
            }
            catch (Exception)
            {
                // Nếu idUser bị null tức là không tìm thấy, sẽ xảy ra Exception
                // Sau đó trả về null
                return null;
            }

        }

        public Task<IEnumerable<BillDetailDto>> GetBillDetail(string username)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Update(Bill obj)
        {
            var bill = await _context.Bills.FindAsync(obj.Id);
            if (bill == null)
            {
                return false;
            }
            try
            {
                bill.SDTKhachHang = obj.SDTKhachHang;
                bill.HoTenKhachHang = obj.HoTenKhachHang;
                bill.DiaChiKhachHang = obj.DiaChiKhachHang;
                bill.Status = obj.Status;
                _context.Bills.Update(bill);
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
