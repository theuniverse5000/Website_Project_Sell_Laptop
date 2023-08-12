using Shop_Models.Dto;

namespace Shop_API.Service.IService
{
    public interface IBillService
    {
        Task<ReponseDto> CreateBill(string username, string maVoucher);//Khách hàng chỉ được phép tạo hóa đơn
        Task<ReponseDto> CreateBillDetail(string invoiceCode, string serialNumber);// Do nhân viên tạo, truyền vào mã hóa đơn và số serial
    }
}
