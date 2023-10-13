using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IBillRepository
    {
        Task<bool> Create(Bill obj);
        Task<bool> Update(Bill obj);
        Task<bool> Delete(Bill id);
        Task<IEnumerable<Bill>> GetAll();
        Task<IEnumerable<BillDetailDto>> GetBillDetailByPhoneNumber(string phoneNumber);
        Task<Bill> GetBillByPhoneNumber(string phoneNumber);
        Task<IEnumerable<BillDetailDto>> GetBillDetail(string username);
    }
}
