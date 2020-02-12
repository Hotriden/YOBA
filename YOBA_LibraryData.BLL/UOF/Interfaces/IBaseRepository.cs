using System.Collections.Generic;

namespace YOBA_LibraryData.BLL.Interfaces
{
    public interface IBaseRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        T GetById(string id);
        void Delete(T item);
        void Change(T item);
    }
}
