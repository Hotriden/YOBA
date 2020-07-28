using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly YOBAContext _context;
        public SupplierRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(string userId, Supplier item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, Supplier item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Supplier> GetAll(string userId)
        {
            return _context.Supplier;
        }

        public Supplier GetById(string userId, int id)
        {
            return _context.Supplier.First(supplier => supplier.Id == id);
        }

        public async Task Change(string userId, Supplier item)
        {
            _context.Supplier.Update(item);
            await _context.SaveChangesAsync();
        }

        public Supplier GetByNumber(string userId, string identity)
        {
            throw new System.NotImplementedException();
        }

        public Supplier Get(string userId, Supplier item)
        {
            throw new System.NotImplementedException();
        }
    }
}
