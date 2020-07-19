using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.DAL;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly YOBAContext _context;
        public BranchRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(string userId, Branch item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, Branch item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Branch> GetAll(string userId)
        {
            return _context.Branches;
        }

        public Branch GetById(string userId, int id)
        {
            return _context.Branches.First(branch => branch.Id == id);
        }

        public async Task Change(string userId, Branch item)
        {
            _context.Branches.Update(item);
            await _context.SaveChangesAsync();
        }

        public Branch Get(string userId, Branch item)
        {
            throw new System.NotImplementedException();
        }
    }
}
