using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IScreenRepository
    {
        Task<bool> Create(Screen obj);
        Task<bool> Update(Screen obj);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Screen>> GetAll();
        Task<Screen> GetById(Guid id);
    }
}
