using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly YOBAContext _context;
        public PaymentRepository(YOBAContext context)
        {
            _context = context;
        }

        public async Task Add(Payment item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Payment item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Payment> GetAll()
        {
            return _context.Payments;
        }

        public Payment GetById(int id)
        {
            return _context.Payments.First(payment => payment.Id == id);
        }

        public Payment GetByIdentity(string id)
        {
            return _context.Payments.First(payment => payment.IdentialPayNumber == id);
        }

        public async Task Change(Payment item)
        {
            _context.Payments.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
