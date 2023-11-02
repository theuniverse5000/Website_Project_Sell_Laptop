using Shop_Models.Dto;

namespace Shop_API.Service.IService
{
    public interface IBillService
    {
        Task<ResponseDto> CreateBill(string username, string maVoucher);//Khách hàng chỉ được phép tạo hóa đơn
                                                                        //   Task<ResponseDto> CreateBillDetail(string invoiceCode, string serialNumber);// Do nhân viên tạo, truyền vào mã hóa đơn và số serial
        Task<ResponseDto> PGetBillByInvoiceCode(string invoiceCode);
        Task<ResponseDto> GetBillDetailByInvoiceCode(string invoiceCode);
        // Task<ResponseDto> GetAllBill();
        Task<ResponseDto> GetAllBill(string? phoneNumber);
    }
}
