using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IVoucherRepository
    {
        //Task<bool> Create(Voucher obj);
        Task<ResponseDto> Create(Voucher obj);
        Task<ResponseDto> Update(Voucher obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Voucher>> GetAllVouchers();
        Task<bool> Duyet(Guid Id);
        Task<bool> HuyDuyet(Guid Id);
    }
}
