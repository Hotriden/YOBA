using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Interfaces;

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
            if (_context.Products.Find(item.ProductId) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Object not find");
            }
        }

        public void Delete(Product item)
        {
            if (_context.Products.Find(item) != null) {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Object not find");
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Product item)
        {
            if (_context.Products.Find(item.ProductId) == null)
            {
                _context.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Object not find");
            }
        }
    }
}
