using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IQuyDoiDiemRepository
    {
        Task<bool> Create(QuyDoiDiem obj);
        Task<bool> Update(QuyDoiDiem obj);
        Task<bool> Delete(Guid id);
        Task<List<QuyDoiDiem>> GetAllQuyDoiDiems();
    }
}
