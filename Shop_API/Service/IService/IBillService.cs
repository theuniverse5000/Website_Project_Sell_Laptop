using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Service.IService
{
    public interface IBillService
    {
        Task<ResponseDto> CreateBill(string username, string maVoucher);//Khách hàng chỉ được phép tạo hóa đơn
        Task<ResponseDto> CreateBillDetail(string invoiceCode, string serialNumber);// Do nhân viên tạo, truyền vào mã hóa đơn và số serial
        Task<ReponseBillDto> GetBillByPhoneNumber(string phoneNumber);
        // Task<ResponseDto> GetAllBill();
        Task<Bill> GetAllBill(string phoneNumber);
    }
}
