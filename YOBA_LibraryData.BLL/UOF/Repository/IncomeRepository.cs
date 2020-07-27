using System.Collections.Generic;
using YOBA_LibraryData.BLL.Entities.Finance;
using System.Linq;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly YOBAContext _context;
        public IncomeRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(string userId, Income item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, Income item)
        {
           _context.Remove(item);
           await _context.SaveChangesAsync();
        }

        public IQueryable<Income> GetAll(string userId)
        {
            return _context.Income;
        }

        public Income GetById(string userId, int id)
        {
            return _context.Income.First(income => income.Id == id);
        }

        public async Task Change(string userId, Income item)
        {
           _context.Update(item);
           await _context.SaveChangesAsync();
        }
        public Income Get(string userId, Income item)
        {
            throw new System.NotImplementedException();
        }
    }
}
