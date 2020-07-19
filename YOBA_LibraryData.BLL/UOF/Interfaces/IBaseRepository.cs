using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.Interfaces
{
    public interface IBaseRepository<T> where T:class
    {
        IQueryable<T> GetAll(string userId);
        Task Add(string userId, T item);
        T Get(string userId, T item);
        Task Delete(string userId, T item);
        Task Change(string userId, T item);
    }
}
