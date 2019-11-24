using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;

namespace YOBA_LibraryData.BLL.UOF
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        void Add(Product item);
        Product GetById(int id);
        void Delete(Product item);
        void Update(Product item);
        void Save();
    }
}
