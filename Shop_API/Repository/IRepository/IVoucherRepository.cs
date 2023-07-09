using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IVoucherRepository
    {
        Task<bool> Create(Voucher obj);
        Task<bool> Update(Voucher obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Voucher>> GetAllVouchers();
    }
}
