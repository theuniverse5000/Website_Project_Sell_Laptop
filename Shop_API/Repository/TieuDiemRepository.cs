using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class TieuDiemRepository : ITieuDiemRepository
    {
        private readonly ApplicationDbContext _context;
        public TieuDiemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hàm tính số điểm tích lũy dựa vào tổng tiền hóa đơn
        public int TinhDiemTichLuy(double tongTienHoaDon)
        {
            const double tiLeDiemTichLuy = 0.00625; // tiLeDiemTichLuy = 1000 VND /1.600.000 VND = 0.000625.
            const double diemToVND = 10000; // 1 điểm bằng 1000 VND ().

            double diemTichLuy = tongTienHoaDon * tiLeDiemTichLuy / diemToVND;
            return (int)diemTichLuy;
        }

        // hàm tính đổi điểm sang tiền khi sử dụng điểm 
        public double DoiDiemSangTien(int soDiemMuonDoi)
        {

            const double diemToVND = 10000;

            double soTienSauKhiDoi = soDiemMuonDoi * 10000;
            return (double)soTienSauKhiDoi;
        }


        // thêm lịch sử tiêu điểm trong lần đầu khách hàng mua hàng
        public async Task<bool> LanDauMuaHang(Guid IdBill, double TongTienThanhToan)
        {
            var _bill = _context.Bills.Find(IdBill);

            var user = _context.Users.Find(_bill.UserId);
            if (user == null)
            {
                return false; // không thực hiện thanh toán nếu không có người dùng
            }

            var soDiemDuocCong = TinhDiemTichLuy(TongTienThanhToan);
            //var tienTieuDiem = DoiDiemSangTien(TongTienThanhToan);

            var viDiem = _context.ViDiems.FirstOrDefault(x => x.UserId == user.Id);




            // thêm lịch sử quyDoiDiem 
            QuyDoiDiem objQuyDoiDiem = new QuyDoiDiem();
            objQuyDoiDiem.Id = Guid.NewGuid();
            objQuyDoiDiem.TienTieuDiem = 0;// lần đầu mua không có tiền tiêu điểm
            objQuyDoiDiem.TienTichDiem = soDiemDuocCong * 10000; // số tiền tích điểm = số điểm tích được * 10000.
            objQuyDoiDiem.TrangThai = 1;

            _context.QuyDoiDiems.Add(objQuyDoiDiem);
            _context.SaveChanges();

            // thêm lịch sử tiêu điểm 
            LichSuTieuDiem objLSTieuDiem = new LichSuTieuDiem();
            objLSTieuDiem.Id = Guid.NewGuid();
            objLSTieuDiem.SoDiemDaDung = 0; // lần đầu mua hàng không sử dụng điểm
            objLSTieuDiem.SoDiemCong = soDiemDuocCong;
            objLSTieuDiem.NgaySD = DateTime.Now;
            objLSTieuDiem.TrangThai = 1;
            objLSTieuDiem.QuyDoiDiemId = objQuyDoiDiem.Id;
            objLSTieuDiem.ViDiemId = viDiem.UserId;


            _context.LichSuTieuDiems.Add(objLSTieuDiem);
            _context.SaveChanges();

            // vi diem
            viDiem.TongDiem += soDiemDuocCong; // số điểm hiện có sẽ được cộng thêm số điểm được cộng
            viDiem.SoDiemDaCong += soDiemDuocCong;// tổng toàn bộ đã được cộng từ trước đến thời điểm hiện tại
            viDiem.SoDiemDaDung += 0; // lần đầu mua chưa cộng
            viDiem.TrangThai = 2; // trạng thái 2 sẽ là đánh dấu người người đã mua lần đầu
            _context.ViDiems.Update(viDiem);
            _context.SaveChanges();

            return true;

        }



        // thêm lịch sử tiêu điểm khi khách hàng đã mua hàng lần đầu
        public async Task<bool> TichDiemTieuDiem(Guid IdBill, double TongTienThanhToan, int SoDiemMuonDung)
        {
            var _bill = _context.Bills.Find(IdBill);

            var user = _context.Users.Find(_bill.UserId);
            if (user == null)
            {
                return false; // không thực hiện thanh toán nếu không có người dùng
            }

            var soDiemDuocCong = TinhDiemTichLuy(TongTienThanhToan);
            var tienTieuDiem = DoiDiemSangTien(SoDiemMuonDung);

            var viDiem = _context.ViDiems.FirstOrDefault(x => x.UserId == user.Id);




            // thêm lịch sử quyDoiDiem 
            QuyDoiDiem objQuyDoiDiem = new QuyDoiDiem();
            objQuyDoiDiem.Id = Guid.NewGuid();
            objQuyDoiDiem.TienTieuDiem = tienTieuDiem;
            objQuyDoiDiem.TienTichDiem = soDiemDuocCong * 10000; // số tiền tích điểm = số điểm tích được * 10000.
            objQuyDoiDiem.TrangThai = 1;

            _context.QuyDoiDiems.Add(objQuyDoiDiem);
            _context.SaveChanges();

            // thêm lịch sử tiêu điểm 
            LichSuTieuDiem objLSTieuDiem = new LichSuTieuDiem();
            objLSTieuDiem.Id = Guid.NewGuid();
            objLSTieuDiem.SoDiemDaDung = 0; // lần đầu mua hàng không sử dụng điểm
            objLSTieuDiem.SoDiemCong = soDiemDuocCong;
            objLSTieuDiem.NgaySD = DateTime.Now;
            objLSTieuDiem.TrangThai = 1;
            objLSTieuDiem.QuyDoiDiemId = objQuyDoiDiem.Id;
            objLSTieuDiem.ViDiemId = viDiem.UserId;

            _context.LichSuTieuDiems.Add(objLSTieuDiem);
            _context.SaveChanges();

            // vi diem
            viDiem.TongDiem += soDiemDuocCong; // số điểm hiện có sẽ được cộng thêm số điểm được cộng
            viDiem.SoDiemDaCong += soDiemDuocCong;// tổng toàn bộ đã được cộng từ trước đến thời điểm hiện tại
            viDiem.SoDiemDaDung += tienTieuDiem;
            viDiem.TrangThai = 2; // trạng thái 2 sẽ là đánh dấu người người đã mua lần đầu
            _context.ViDiems.Update(viDiem);
            _context.SaveChanges();

            return true;


        }
    }
}
