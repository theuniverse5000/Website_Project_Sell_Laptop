using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public class ImeiRepository : IImeiRepository
    {
        public Task<bool> Create(Imei obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Screen>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Imei obj)
        {
            throw new NotImplementedException();
        }
    }
}
