using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_Services.Exceptions;
using YOBA_LibraryData.DAL;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly YOBAContext _context;
        public SupplierRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Supplier item)
        {
            if (_context.Suppliers.Find(item.SupplierName) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.SupplierName);
            }
        }

        public void Delete(Supplier item)
        {
            if (_context.Suppliers.First(supplier => supplier.SupplierId == item.SupplierId) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.SupplierId);
            }
        }

        public IEnumerable<Supplier> GetAll()
        {
            if (_context.Suppliers != null)
            {
                return _context.Suppliers;
            }
            else
            {
                throw new EmptyDataException(typeof(Supplier).ToString());
            }
        }

        public Supplier GetById(string id)
        {
            var result = _context.Suppliers.First(supplier => supplier.SupplierId == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Supplier).ToString());
            }
        }

        public void Change(Supplier item)
        {
            if (_context.Suppliers.First(x => x.SupplierId == item.SupplierId) != null)
            {
                _context.Suppliers.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.SupplierId);
            }
        }

        public Supplier GetByNumber(string identity)
        {
            throw new System.NotImplementedException();
        }
    }
}
