using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class TaxRepository : ITaxRepository
    {
        private readonly YOBAContext _context;
        public TaxRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(string userId, Tax item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, Tax item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Tax> GetAll(string userId)
        {
            return _context.Tax.Where(c => c.UserId == userId);
        }

        public Tax GetById(string userId, int id)
        {
            return _context.Tax.First(tax => tax.Id == id);
        }

        public async Task Change(string userId, Tax item)
        {
            _context.Tax.Update(item);
            await _context.SaveChangesAsync();
        }
        public Tax Get(string userId, Tax item)
        {
            throw new System.NotImplementedException();
        }
    }
}
