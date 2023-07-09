using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IBillDetailRepository
    {
        Task<bool> CreateBillDetail(BillDetail obj);
        Task<bool> UpdateBillDetail(BillDetail obj);
        Task<bool> DeleteBillDetail(Guid id);
        Task<List<BillDetail>> GetAllBillDetails();
        Task<BillDetail> GetBillDetailById(Guid id);
    }
}
