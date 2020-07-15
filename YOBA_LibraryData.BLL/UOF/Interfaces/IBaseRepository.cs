using System.Collections.Generic;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.Interfaces
{
    public interface IBaseRepository<T> where T:class
    {
        IEnumerable<T> GetAll(string userId);
        Task Add(string userId, T item);
        T GetById(string userId, int id);
        Task Delete(string userId, T item);
        Task Change(string userId, T item);
    }
}
