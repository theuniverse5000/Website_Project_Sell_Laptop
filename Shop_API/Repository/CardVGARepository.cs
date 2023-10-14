using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class CardVGARepository : ICardVGARepository
    {
        private readonly ApplicationDbContext _context;
        public CardVGARepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CardVGA obj)
        {
            var checkMa = await _context.CardVGAs.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.CardVGAs.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(Guid id)
        {
            var cardVGA = await _context.CardVGAs.FindAsync(id);
            if (cardVGA == null)
            {
                return false;
            }
            try
            {
                cardVGA.TrangThai = 0;
                _context.CardVGAs.Update(cardVGA);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<CardVGA>> GetAllCardVGA()
        {
            var list = await _context.CardVGAs.ToListAsync();
            var listCardVGA = list.Where(x => x.TrangThai != 0).ToList();
            return listCardVGA;
        }

        public async Task<CardVGA> GetById(Guid id)
        {
            var result = await _context.CardVGAs.FindAsync(id);
            return result;
        }

        public async Task<bool> Update(CardVGA obj)
        {
            var cardVGA = await _context.CardVGAs.FindAsync(obj.Id);
            if (cardVGA == null)
            {
                return false;
            }
            try
            {
                cardVGA.Ten = obj.Ten;
                cardVGA.ThongSo = obj.ThongSo;
                //cardVGA.TrangThai = obj.TrangThai;
                _context.CardVGAs.Update(cardVGA);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
