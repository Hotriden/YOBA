using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class ExpenceRepository :IExpenceRepository
    {
        private readonly YOBAContext _context;
        public ExpenceRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(string userId, Expence item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, Expence item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Expence> GetAll(string userId)
        {
            return _context.Expences;
        }

        public Expence GetById(string userId, int id)
        {
            return _context.Expences.First(expence => expence.Id == id);
        }

        public async Task Change(string userId, Expence item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public Expence Get(string userId, Expence item)
        {
            throw new System.NotImplementedException();
        }
    }
}
