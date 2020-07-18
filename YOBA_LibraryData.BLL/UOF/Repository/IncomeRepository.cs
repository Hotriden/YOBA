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
        public async Task Add(Income item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Income item)
        {
           _context.Remove(item);
           await _context.SaveChangesAsync();
        }

        public IQueryable<Income> GetAll(string userId)
        {
            return _context.Incomes;
        }

        public Income GetById(int id)
        {
            return _context.Incomes.First(income => income.Id == id);
        }

        public async Task Change(Income item)
        {
           _context.Update(item);
           await _context.SaveChangesAsync();
        }

        public Income GetByName(string name)
        {
            throw new System.NotImplementedException();
        }
        public Task Add(string userId, Income item)
        {
            throw new System.NotImplementedException();
        }

        public Income GetById(string userId, int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string userId, Income item)
        {
            throw new System.NotImplementedException();
        }

        public Task Change(string userId, Income item)
        {
            throw new System.NotImplementedException();
        }
    }
}
