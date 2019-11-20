using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_LibraryData.BLL.Interfaces
{
    public interface IBaseRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        T GetById(int id);
        void Delete(T item);
        void Update(T item);
        void Save();
    }
}
