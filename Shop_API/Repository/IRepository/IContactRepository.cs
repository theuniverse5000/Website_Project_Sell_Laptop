using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IContactRepository
    {
        Task<bool> Create( Contact contact );
        Task<bool> Update(Contact contact);
        Task<bool> Delete(Guid Id);
        Task<List<Contact>> GetAllContacts();
    }
}
