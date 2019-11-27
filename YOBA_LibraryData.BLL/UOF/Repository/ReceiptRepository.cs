using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class ReceiptRepository : IBaseRepository<Receipt>
    {
        private YOBAContext _context;
        public ReceiptRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Receipt item)
        {
            if (_context.Receipts.Find(item.DocumentNumber) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.DocumentNumber);
            }
        }

        public void Delete(Receipt item)
        {
            if (_context.Receipts.First(receipt => receipt.ReceiptId == item.ReceiptId) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.ReceiptId);
            }
        }

        public IEnumerable<Receipt> GetAll()
        {
            if (_context.Receipts != null)
            {
                return _context.Receipts;
            }
            else
            {
                throw new EmptyDataException(typeof(Receipt).ToString());
            }
        }

        public Receipt GetById(int id)
        {
            var result = _context.Receipts.First(receipt => receipt.ReceiptId == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Receipt).ToString());
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Change(Receipt item)
        {
            if (_context.Receipts.First(x => x.ReceiptId == item.ReceiptId) != null)
            {
                _context.Receipts.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.ReceiptId);
            }
        }
    }
}
