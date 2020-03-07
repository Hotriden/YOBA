using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly YOBAContext _context;
        public EmployeeRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(Employee item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Employee item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }

        public Employee GetById(int id)
        {
            return _context.Employees.First(emp => emp.EmployeeId == id);
        }

        public async Task Change(Employee item)
        {
            _context.Employees.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
