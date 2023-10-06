using Shop_API.AppDbContext;
using Shop_API.Repository;
using Shop_API.Repository.IRepository;
using Shop_API.Service.IService;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Service
{
    public class TichDiemServices : ITichDiemServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuyDoiDiemRepository _quyDoiDiemRepository;
        private readonly ILichSuTieuDiemRepository _lichSuTieuDiemRepository;
        private readonly IViDiemRepository _viDiemRepository;
        private readonly ReponseDto _reponseDto;
        public TichDiemServices(ApplicationDbContext context, IQuyDoiDiemRepository quyDoiDiemRepository, ILichSuTieuDiemRepository lichSuTieuDiemRepository, IViDiemRepository viDiemRepository)
        {
            _context = context;
            _quyDoiDiemRepository = quyDoiDiemRepository;
            _lichSuTieuDiemRepository = lichSuTieuDiemRepository;
            _viDiemRepository = viDiemRepository;
            _reponseDto = new ReponseDto();


        }

        // Hàm tính số điểm tích lũy dựa vào tổng tiền hóa đơn
        public double TinhDiemTichLuy(double tongTienHoaDon)
        {
            const double tiLeDiemTichLuy = 0.000000625; // tiLeDiemTichLuy = 1 VND /1.600.000 VND = 0.000000625


            if (tongTienHoaDon > 0)
            {
                double diemTichLuy = tongTienHoaDon * tiLeDiemTichLuy ;

                return diemTichLuy;
            }
            else
            {
                double diemTichLuy = 0;

                return diemTichLuy;
            }
        }

        // hàm tính đổi điểm sang tiền khi sử dụng điểm 
        public double DoiDiemSangTien(double soDiemMuonDoi)
        {
            const double diemToVND = 10000; // 1 điểm bằng 10000 VND           

            if (soDiemMuonDoi > 0)
            {

                double soTienSauKhiDoi = soDiemMuonDoi * 10000;



                return soTienSauKhiDoi;
            }
            else
            {
                double soTienSauKhiDoi = 0;

                return soTienSauKhiDoi;
            }

        }

        public bool TichDiemChoLanDauMuaHang(Guid IdBill, double TongTienThanhToan)
        {

            var _bill = _context.Bills.Find(IdBill);
            var user = _context.Users.Find(_bill.UserId);

            if (_bill == null && TongTienThanhToan <= 0)
            {
                return false;// không thực hiện tích điểm nếu không tìm thấy hóa đơn or tổng tiền thanh toán < 0 VND
            }
            else if (_bill.Status != 1)
            {
                return false;// Không thực hiện tích điểm nếu hóa đơn có trạng thái khác 1.(chưa thanh toán xong) 
            }
            else if (user == null)
            {
                return false;// không thực hiện tích điểm nếu không có người dùng
            }
            else
            {
                var soDiemDuocCong = TinhDiemTichLuy(TongTienThanhToan);
                var soTienTichDuoc = DoiDiemSangTien(soDiemDuocCong);

                var viDiem = _context.ViDiems.FirstOrDefault(x => x.UserId == user.Id);

                // thêm lịch sử quyDoiDiem 
                QuyDoiDiem objQuyDoiDiem = new QuyDoiDiem();
                objQuyDoiDiem.Id = Guid.NewGuid();
                objQuyDoiDiem.TienTieuDiem = 0;// lần đầu mua không có tiền tiêu điểm
                objQuyDoiDiem.TienTichDiem = soTienTichDuoc; // số tiền được đổi từ điểm tích được trong lần mua hàng hiện tại
                objQuyDoiDiem.TrangThai = 1;
                _quyDoiDiemRepository.Create(objQuyDoiDiem);

                // thêm lịch sử tiêu điểm 
                LichSuTieuDiem objLSTieuDiem = new LichSuTieuDiem();
                objLSTieuDiem.Id = Guid.NewGuid();
                objLSTieuDiem.SoDiemDaDung = 0; // lần đầu mua hàng không sử dụng điểm
                objLSTieuDiem.SoDiemCong = soDiemDuocCong;
                objLSTieuDiem.NgaySD = DateTime.Now;
                objLSTieuDiem.TrangThai = 1;
                objLSTieuDiem.QuyDoiDiemId = objQuyDoiDiem.Id;
                objLSTieuDiem.ViDiemId = viDiem.UserId;
                _lichSuTieuDiemRepository.Create(objLSTieuDiem);

                // vi diem
                viDiem.TongDiem += soDiemDuocCong; // số điểm hiện có sẽ được cộng thêm số điểm được cộng
                viDiem.SoDiemDaCong += soDiemDuocCong;// tổng toàn bộ đã được cộng từ trước đến thời điểm hiện tại
                viDiem.SoDiemDaDung += 0; // lần đầu mua không dùng điểm
                viDiem.TrangThai = 2; // trạng thái 2 sẽ là đánh dấu người người đã mua lần đầu
                _viDiemRepository.Update(viDiem);

                return true;
            }

        }

        public bool TichDiemChoNhungLanMuaSau(Guid IdBill, double TongTienThanhToan, double SoDiemMuonDung)
        {

            var _bill = _context.Bills.Find(IdBill);
            var user = _context.Users.Find(_bill.UserId);

            if (_bill == null && TongTienThanhToan <= 0)
            {
                return false;// không thực hiện tích điểm nếu không tìm thấy hóa đơn or tổng tiền thanh toán < 0 VND
            }
            else if (_bill.Status != 1)
            {
                return false;// Không thực hiện tích điểm nếu hóa đơn có trạng thái khác 1.(chưa thanh toán xong) 
            }
            else if (user == null)
            {
                return false;// không thực hiện tích điểm nếu không có người dùng
            }
            else if (SoDiemMuonDung < 0)
            {
                return false;// không thực hiện tiếp nếu số điểm muốn dùng là âm
            }
            else
            {
                var SoDiemDuocCong = TinhDiemTichLuy(TongTienThanhToan);
                var tienTieuDiem = DoiDiemSangTien(SoDiemMuonDung);
                var tienTichDiem = DoiDiemSangTien(SoDiemDuocCong);

                var viDiem = _context.ViDiems.FirstOrDefault(x => x.UserId == user.Id);


                // thêm lịch sử quyDoiDiem 
                QuyDoiDiem objQuyDoiDiem = new QuyDoiDiem();
                objQuyDoiDiem.Id = Guid.NewGuid();
                objQuyDoiDiem.TienTieuDiem = tienTieuDiem;
                objQuyDoiDiem.TienTichDiem = tienTichDiem; // số tiền tích điểm = số điểm tích được * 10000.
                objQuyDoiDiem.TrangThai = 1;
                _quyDoiDiemRepository.Create(objQuyDoiDiem);

                // thêm lịch sử tiêu điểm 
                LichSuTieuDiem objLSTieuDiem = new LichSuTieuDiem();
                objLSTieuDiem.Id = Guid.NewGuid();
                objLSTieuDiem.SoDiemDaDung = SoDiemMuonDung;
                objLSTieuDiem.SoDiemCong = SoDiemDuocCong;
                objLSTieuDiem.NgaySD = DateTime.Now;
                objLSTieuDiem.TrangThai = 1;
                objLSTieuDiem.QuyDoiDiemId = objQuyDoiDiem.Id;
                objLSTieuDiem.ViDiemId = viDiem.UserId;
                _lichSuTieuDiemRepository.Create(objLSTieuDiem);

                // vi diem
                double tongDiem = viDiem.TongDiem - SoDiemMuonDung + SoDiemDuocCong;
                viDiem.TongDiem = tongDiem; // tổng điểm hiện có sau khi trừ số điểm muốn dùng và cộng số điểm được cộng 
                viDiem.SoDiemDaCong += SoDiemDuocCong;// tổng toàn bộ điểm đã được cộng từ trước đến thời điểm hiện tại
                viDiem.SoDiemDaDung += tienTieuDiem;
                viDiem.TrangThai = 2; // trạng thái 2 sẽ là đánh dấu người người đã mua lần đầu
                _viDiemRepository.Update(viDiem);
                return true;
            }
        }
    }
}
