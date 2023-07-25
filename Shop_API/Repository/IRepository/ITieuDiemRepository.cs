namespace Shop_API.Repository.IRepository
{
    public interface ITieuDiemRepository
    {
        public int TinhDiemTichLuy(double tongTienHoaDon);
        public double DoiDiemSangTien(int soDiemMuonDoi);
        public  Task<bool> LanDauMuaHang(Guid IdBill, double TongTienThanhToan);
        public Task<bool> TichDiemTieuDiem(Guid IdBill, double TongTienThanhToan, int SoDiemMuonDung);

    }
}
