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

        public async Task Add(string userId, Payment item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, Payment item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Payment> GetAll(string userId)
        {
            return _context.Payments;
        }

        public Payment GetById(string userId, int id)
        {
            return _context.Payments.First(payment => payment.Id == id);
        }

        public Payment GetByIdentity(string userId, string id)
        {
            return _context.Payments.First(payment => payment.IdentialPayNumber == id);
        }

        public async Task Change(string userId, Payment item)
        {
            _context.Payments.Update(item);
            await _context.SaveChangesAsync();
        }
        public Payment Get(string userId, Payment item)
        {
            throw new System.NotImplementedException();
        }
    }
}
