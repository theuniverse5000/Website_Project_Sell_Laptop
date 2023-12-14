using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IHardDriveRepository
    {
        Task<bool> Create(HardDrive obj);
        Task<bool> Update(HardDrive obj);
        Task<bool> Delete(Guid id);
        Task<List<HardDrive>> GetAllHardDrives();
        Task<HardDrive> GetById(Guid id);
        Task<ResponseDto> CreateReturnDto(HardDrive obj);
    }
}
