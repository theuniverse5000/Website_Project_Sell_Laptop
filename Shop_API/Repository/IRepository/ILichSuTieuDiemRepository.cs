using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface ILichSuTieuDiemRepository
    {
        Task<bool> Create(LichSuTieuDiem obj);
        Task<bool> Update(LichSuTieuDiem obj);
        Task<bool> Delete(Guid id);
        Task<List<LichSuTieuDiem>> GetAllLichSuTieuDiems();
    }
}
