using Shop_Models.Dto;
using System.Threading.Tasks;

namespace Shop_API.Service.IService
{
    public interface ITichDiemServices
    {
        double TinhDiemTichLuy(double tongTienHoaDon); // Phương thức để tính điểm tích lũy
        double DoiDiemSangTien(double? soDiemMuonDoi);// Phương thức để đổi điểm sang tiền VND
        Task<ResponseDto> TichDiemChoLanDauMuaHangAsync(string invoiceCode, double TongTienThanhToan, double? SoDiemMuonDung);
        Task<ResponseDto> TichDiemMuaHangAsync(string invoiceCode, double TongTienThanhToan);
        Task<ResponseDto> TieuDiemMuaHangAsync(string invoiceCode, double TongTienThanhToan,double? SoDiemMuonDung);

        bool TichDiemChoLanDauMuaHang(Guid guid, double TongTienThanhToan); // Phương thức tích điểm cho lần đầu mua hàng
        bool TichDiemChoNhungLanMuaSau(Guid IdBill, double TongTienThanhToan, double SoDiemMuonDung); // Phương thức tích điểm tính từ lần mua hàng thứ 2
        Task<ResponseDto> TichDiemChoNhungLanMuaSauAsync(string phoneNumber, double TongTienThanhToan, double SoDiemMuonDung);
    }
}
