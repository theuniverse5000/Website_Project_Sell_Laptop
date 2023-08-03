using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> Create(Contact contact)
        {
            if (contact == null)
            {
                return false;
            }
            try
            {
                await _dbContext.Contacts.AddAsync(contact);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            var obj = await _dbContext.Contacts.FindAsync(Id);
            if (obj == null)
            {
                return false;
            }
            try
            {
                obj.Status = false;
                _dbContext.Contacts.Update(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            var list = await _dbContext.Contacts.ToListAsync();
            var listContact = list.Where(x => x.Status != false).ToList();
            return listContact;
        }

        public async Task<bool> Update(Contact contact)
        {
            var obj = await _dbContext.Contacts.FindAsync(contact.Id);
            if (obj == null)
            {
                return false;
            }
            try
            {
                obj.Email = contact.Email;
                obj.Name = contact.Name;
                obj.Message = contact.Message;
                obj.CreateDate = contact.CreateDate;
                obj.CodeManagePost = contact.CodeManagePost;
                obj.Status = contact.Status;
                obj.Website = contact.Website;
                _dbContext.Contacts.Update(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
