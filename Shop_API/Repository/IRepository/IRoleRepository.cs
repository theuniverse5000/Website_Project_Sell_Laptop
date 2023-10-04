using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IRoleRepository
    {
        public const string Role_NoteFound = "Role_NotFound";
        Task<bool> Create(Role obj);
        Task<bool> Update(Role obj);
        Task<bool> Delete(Guid id);
        Task<List<Role>> GetAllRoles();
        Task<Role> GetById(Guid id);
    }
}
