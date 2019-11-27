using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_Services.Exceptions;
using System.Linq;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class IncomeRepository : IBaseRepository<Income>
    {
        private YOBAContext _context;
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

        public Income GetById(int id)
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

        public void Save()
        {
            _context.SaveChanges();
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
    }
}
