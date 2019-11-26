using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private YOBAContext _context;
        public ProductRepository(YOBAContext context)
        {
            _context = context;
        }

        public void Add(Product item)
        {
            if (_context.Products.Find(item.ProductName) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.ProductName);
            }
        }

        public void Delete(Product item)
        {
            if (_context.Products.First(product=>product.ProductId==item.ProductId)!=null) {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.ProductId);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            if (_context.Products != null)
            {
                return _context.Products;
            }
            else
            {
                throw new EmptyDataException(typeof(Product).ToString());
            }
        }

        public Product GetById(int id)
        {
            var result = _context.Products.First(product => product.ProductId == id);
            if (result != null) 
            {
                return result; 
            }
            else 
            { 
                throw new EmptyDataException(typeof(Product).ToString()); 
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Change(Product item)
        {
            if (_context.Products.First(x=>x.ProductId==item.ProductId) != null)
            {
                _context.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.ProductId);
            }
        }
    }
}
