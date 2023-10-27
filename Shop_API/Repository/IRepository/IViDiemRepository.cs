using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IViDiemRepository
    {
        Task<bool> Create(ViDiem obj);
        Task<bool> Update(ViDiem obj);
        Task<bool> Delete(Guid Id);
        Task<List<ViDiem>> GetAllViDiems();
        Task<ViDiem> GetViDiemById(Guid? id);
        Task<List<PointWalletDto>> GetAllPointWallet(string? search, double? from, double? to, string? sortBy, int page);
    }
}
