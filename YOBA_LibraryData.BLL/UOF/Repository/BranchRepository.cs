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
        public async Task Add(Branch item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Branch item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Branch> GetAll()
        {
            return _context.Branches;
        }

        public Branch GetById(int id)
        {
            return _context.Branches.First(branch => branch.BranchId == id);
        }

        public async Task Change(Branch item)
        {
            _context.Branches.Update(item);
            await _context.SaveChangesAsync();
        }

        public Branch GetByName(string name)
        {
            return _context.Branches.First(branch => branch.BranchName == name);
        }
    }
}
