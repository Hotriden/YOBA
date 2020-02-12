using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Supply;

namespace YOBA_BLL.Catalogue
{
    public interface ICatalogue<T>
    {
        void Create(T item, string UserId);
        void Update(T item, string UserId);
        void Delete(T item, string UserId);
        IEnumerable<T> GetAll();
    }
}
