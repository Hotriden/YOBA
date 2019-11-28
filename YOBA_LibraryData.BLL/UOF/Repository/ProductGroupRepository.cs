using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private YOBAContext _context;
        public ProductGroupRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(ProductGroup item)
        {
            if (_context.ProductGroups.Find(item.GroupName) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.GroupName);
            }
        }

        public void Delete(ProductGroup item)
        {
            if (_context.ProductGroups.First(productGroup => productGroup.GroupId == item.GroupId) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.GroupId);
            }
        }

        public IEnumerable<ProductGroup> GetAll()
        {
            if (_context.ProductGroups != null)
            {
                return _context.ProductGroups;
            }
            else
            {
                throw new EmptyDataException(typeof(ProductGroup).ToString());
            }
        }

        public ProductGroup GetById(int id)
        {
            var result = _context.ProductGroups.First(productGroup => productGroup.GroupId == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(ProductGroup).ToString());
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Change(ProductGroup item)
        {
            if (_context.ProductGroups.First(productGroup => productGroup.GroupId == item.GroupId) != null)
            {
                _context.ProductGroups.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.GroupId);
            }
        }
    }
}
