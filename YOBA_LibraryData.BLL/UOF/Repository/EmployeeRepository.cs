using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;
using YOBA_LibraryData.DAL.UOF;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly YOBAContext _context;
        public EmployeeRepository(YOBAContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> GetAll(string userId)
        {
            return _context.Employees;
        }

        public async Task Add(string userId, Employee item)
        {
            if (item != null)
            {
                item.OnAdd(userId);
                _context.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        public Employee Get(string userId, Employee item)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(string userId, Employee item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Change(string userId, Employee item)
        {
            _context.Employees.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
