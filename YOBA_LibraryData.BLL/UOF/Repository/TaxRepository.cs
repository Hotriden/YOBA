using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class TaxRepository : ITaxRepository
    {
        private readonly YOBAContext _context;
        public TaxRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(Tax item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Tax item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Tax> GetAll()
        {
            return _context.Taxes;
        }

        public Tax GetById(int id)
        {
            return _context.Taxes.First(tax => tax.Id == id);
        }

        public async Task Change(Tax item)
        {
            _context.Taxes.Update(item);
            await _context.SaveChangesAsync();
        }

        public Tax GetByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
