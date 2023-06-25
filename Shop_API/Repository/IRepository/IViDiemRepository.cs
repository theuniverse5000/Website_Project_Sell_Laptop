using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IViDiemRepository
    {
        Task<bool> Create(ViDiem obj);
        Task<bool> Update(ViDiem obj);
        Task<bool> Delete(Guid Id);
        Task<List<ViDiem>> GetAllViDiems();
    }
}
