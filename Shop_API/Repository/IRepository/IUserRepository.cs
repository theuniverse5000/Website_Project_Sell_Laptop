using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> Create(User obj);
        Task<bool> Update(User obj);
        Task<bool> Delete(Guid Id);
        Task<List<User>> GetAllUsers();
    }
}
