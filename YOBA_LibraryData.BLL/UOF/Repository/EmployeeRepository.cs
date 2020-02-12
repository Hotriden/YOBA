using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_Services.Exceptions;
using YOBA_LibraryData.DAL;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly YOBAContext _context;
        public EmployeeRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Employee item)
        {
            if (_context.Employees.Find(item.EmployeeId) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.Name);
            }
        }

        public void Delete(Employee item)
        {
            if (_context.Employees.First(emp => emp.EmployeeId == emp.EmployeeId) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.EmployeeId);
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            if (_context.Employees != null)
            {
                return _context.Employees;
            }
            else
            {
                throw new EmptyDataException(typeof(Employee).ToString());
            }
        }

        public Employee GetById(string id)
        {
            var result = _context.Employees.First(emp => emp.EmployeeId == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Employee).ToString());
            }
        }

        public void Change(Employee item)
        {
            if (_context.Employees.First(x => x.EmployeeId == item.EmployeeId) != null)
            {
                _context.Employees.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.EmployeeId);
            }
        }
    }
}
