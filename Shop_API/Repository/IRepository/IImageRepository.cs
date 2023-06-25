using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IImageRepository
    {
        Task<bool> Create(Image obj);
        Task<bool> Update(Image obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Image>> GetAllImage();
    }
}
