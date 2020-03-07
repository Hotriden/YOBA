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

        public async Task Add(Receipt item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Receipt item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Receipt> GetAll()
        {
            return _context.Receipts;
        }

        public Receipt GetById(int id)
        {
            return _context.Receipts.First(receipt => receipt.ReceiptId == id);
        }

        public async Task Change(Receipt item)
        {
            _context.Receipts.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
