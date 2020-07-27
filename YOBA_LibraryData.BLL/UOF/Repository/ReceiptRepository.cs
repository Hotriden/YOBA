using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.DAL.UOF.Interfaces;

namespace YOBA_LibraryData.DAL.UOF.Repository
{
    public class ReceiptRepository: IReceiptRepository
    {
        private readonly YOBAContext _context;
        public ReceiptRepository(YOBAContext context)
        {
            _context = context;
        }

        public async Task Add(string userId, Receipt item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, Receipt item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Receipt> GetAll(string userId)
        {
            return _context.Receipt;
        }

        public Receipt GetById(string userId, int id)
        {
            return _context.Receipt.First(receipt => receipt.Id == id);
        }

        public async Task Change(string userId, Receipt item)
        {
            _context.Receipt.Update(item);
            await _context.SaveChangesAsync();
        }

        public Receipt Get(string userId, Receipt item)
        {
            throw new System.NotImplementedException();
        }
    }
}
