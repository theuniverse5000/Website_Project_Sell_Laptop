using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class BillRepository : IBillRepository
    {
        public Task<bool> Create(Bill obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Bill id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bill>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartItemDto>> GetBillItem(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetByByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Bill obj)
        {
            throw new NotImplementedException();
        }
    }
}
