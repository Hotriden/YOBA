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
        public async Task Add(Expence item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Expence item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Expence> GetAll()
        {
            return _context.Expences;
        }

        public Expence GetById(int id)
        {
            return _context.Expences.First(expence => expence.Id == id);
        }

        public async Task Change(Expence item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public Expence GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Expence> GetAll(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task Add(string userId, Expence item)
        {
            throw new System.NotImplementedException();
        }

        public Expence GetById(string userId, int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string userId, Expence item)
        {
            throw new System.NotImplementedException();
        }

        public Task Change(string userId, Expence item)
        {
            throw new System.NotImplementedException();
        }
    }
}
