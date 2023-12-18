using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IManagePostRepository
    {
        Task<bool> Create(ManagePost managePost);
        Task<bool> Update(ManagePost managePost);
        Task<bool> Delete(Guid Id);
        Task<ResponseDto> Duyet(Guid Id);
        Task<ResponseDto> HuyDuyet(Guid Id);
        Task<ManagePost> GetById(Guid Id);
        Task<bool> GetByCode(Guid Id);
        Task<List<ManagePost>> GetAllManagePosts();
        Task<IEnumerable<ManagePost>> GetManagePostDtos(string? search, DateTime? from, DateTime? to, string? sortBy, bool? status, int page = 1);
    }
}
