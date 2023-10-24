using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IBillRepository
    {
        Task<bool> Create(Bill obj);
        Task<bool> Update(Bill obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Bill>> GetAll();
        Task<IEnumerable<BillDetailDto>> GetBillDetailByInvoiceCode(string invoiceCode);
        Task<Bill> GetBillByInvoiceCode(string invoiceCode);
        Task<IEnumerable<BillDetailDto>> GetBillDetail(string username);
    }
}
