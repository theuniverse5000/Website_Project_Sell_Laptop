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
        Task<IEnumerable<BillDetailDto>> GetBillByUsername(string username);
        Task<IEnumerable<BillDetailDto>> GetBillDetail(string username);
    }
}
