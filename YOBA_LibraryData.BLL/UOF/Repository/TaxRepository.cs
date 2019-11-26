using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class TaxRepository : IBaseRepository<Tax>
    {
        private YOBAContext _context;
        public TaxRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Tax item)
        {
            if (_context.Taxes.Find(item.Name) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.Name);
            }
        }

        public void Delete(Tax item)
        {
            if (_context.Taxes.First(tax => tax.Id == item.Id) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }

        public IEnumerable<Tax> GetAll()
        {
            if (_context.Taxes != null)
            {
                return _context.Taxes;
            }
            else
            {
                throw new EmptyDataException(typeof(Tax).ToString());
            }
        }

        public Tax GetById(int id)
        {
            var result = _context.Taxes.First(tax => tax.Id == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Tax).ToString());
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Change(Tax item)
        {
            if (_context.Taxes.First(x => x.Id == item.Id) != null)
            {
                _context.Taxes.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }
    }
}
