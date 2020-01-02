using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly YOBAContext _context;
        public PaymentRepository(YOBAContext context)
        {
            _context = context;
        }

        public void Add(Payment item)
        {
            if (_context.Payments.Find(item.IdentialPayNumber) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.IdentialPayNumber);
            }
        }

        public void Delete(Payment item)
        {
            if (_context.Payments.First(payment => payment.IdentialPayNumber == item.IdentialPayNumber) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.IdentialPayNumber);
            }
        }

        public IEnumerable<Payment> GetAll()
        {
            if (_context.Payments != null)
            {
                return _context.Payments;
            }
            else
            {
                throw new EmptyDataException(typeof(Payment).ToString());
            }
        }

        public Payment GetById(int id)
        {
            var result = _context.Payments.First(payment => payment.Id == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Payment).ToString());
            }
        }

        public Payment GetByIdentity(string id)
        {
            var result = _context.Payments.First(payment => payment.IdentialPayNumber == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Payment).ToString());
            }
        }

        public void Change(Payment item)
        {
            if (_context.Payments.First(payment => payment.Id == item.Id) != null)
            {
                _context.Payments.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }
    }
}
