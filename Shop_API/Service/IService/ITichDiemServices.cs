using Shop_Models.Dto;

namespace Shop_API.Service.IService
{
    public interface ITichDiemServices
    {
        double TinhDiemTichLuy(double tongTienHoaDon); // Phương thức để tính điểm tích lũy
        double DoiDiemSangTien(double soDiemMuonDoi);// Phương thức để đổi điểm sang tiền VND
        bool TichDiemChoLanDauMuaHang(Guid guid, double TongTienThanhToan); // Phương thức tích điểm cho lần đầu mua hàng
        bool TichDiemChoNhungLanMuaSau(Guid IdBill, double TongTienThanhToan, double SoDiemMuonDung); // Phương thức tích điểm tính từ lần mua hàng thứ 2

    }
}
