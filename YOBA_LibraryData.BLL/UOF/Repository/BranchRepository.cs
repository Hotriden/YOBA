using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private YOBAContext _context;
        public BranchRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Branch item)
        {
            if (_context.Branches.Find(item.BranchName) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.BranchName);
            }
        }

        public void Delete(Branch item)
        {
            if (_context.Branches.First(branch => branch.BranchId == item.BranchId) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.BranchId);
            }
        }

        public IEnumerable<Branch> GetAll()
        {
            if (_context.Branches != null)
            {
                return _context.Branches;
            }
            else
            {
                throw new EmptyDataException(typeof(Product).ToString());
            }
        }

        public Branch GetById(int id)
        {
            var result = _context.Branches.First(branch => branch.BranchId == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Product).ToString());
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Change(Branch item)
        {
            if (_context.Branches.First(x=>x.BranchId==item.BranchId) != null)
            {
                _context.Branches.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.BranchId);
            }
        }
    }
}
