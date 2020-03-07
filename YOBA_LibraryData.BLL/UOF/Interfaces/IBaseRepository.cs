using System.Collections.Generic;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.Interfaces
{
    public interface IBaseRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        Task Add(T item);
        T GetById(int id);
        Task Delete(T item);
        Task Change(T item);
    }
}
