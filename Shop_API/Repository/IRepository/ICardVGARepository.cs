using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface ICardVGARepository
    {
        Task<bool> Create(CardVGA obj);
        Task<bool> Update(CardVGA obj);
        Task<bool> Delete(Guid id);
        Task<List<CardVGA>> GetAllCardVGA();
        Task<CardVGA> GetById(Guid id);
    }
}
