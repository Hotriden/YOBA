using System.Collections.Generic;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_Services.Exceptions;
using System.Linq;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly YOBAContext _context;
        public IncomeRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Income item)
        {
            if (_context.Incomes.Find(item.Name) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.Name);
            }
        }

        public void Delete(Income item)
        {
            if (_context.Incomes.First(income => income.Id == item.Id) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }

        public IEnumerable<Income> GetAll()
        {
            if (_context.Incomes != null)
            {
                return _context.Incomes;
            }
            else
            {
                throw new EmptyDataException(typeof(Income).ToString());
            }
        }

        public Income GetById(string id)
        {
            var result = _context.Incomes.First(income => income.Id == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Income).ToString());
            }
        }

        public void Change(Income item)
        {
            if (_context.Incomes.First(x => x.Id == item.Id) != null)
            {
                _context.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }

        public Income GetByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
